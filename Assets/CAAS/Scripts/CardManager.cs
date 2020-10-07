using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.U2D;
using UnityEngine.UI;
public class CardManager : MonoBehaviour
{
    private CardsJsonData mCardsJsonData;
    private GameObject mCardPrefab;
    private GameObject mGroupObj;
    private SpriteAtlas mCardAtlas; 
    private ResourceReader mReader;

    private List<Card> mCardList;
    private int mTotalGroupNo = 0;
    private Vector3 mMovingCardPreviousPos;
    private float mCardHeight = 1.9f;
    private float mCardWidth = 1.4f;

    private int mCurrentCardIndex = -1;
    private int mCurrentCardGroupNo = -1;
    [SerializeField] private float m_YPosOfCard = 0f;
    [SerializeField] private float m_XGapBetweenCard = .2f;
    [SerializeField] private float m_XGapBetweenGroup = .4f;
    [SerializeField] private int m_MaxGroupNo = 5;
    [SerializeField] private Transform m_CardParent;
    [SerializeField] private Button m_GroupButton;
    [SerializeField] private float m_ShiftInY = .2f;
    [SerializeField] private GameObject mDropObj;

    private void Awake() 
    {
        m_GroupButton.gameObject.SetActive(false);
        mReader = new ResourceReader();
        SetDropObjToCorner();
        GetCardDataFromResource();
        GetCardAtlas();
        GetCardPrefabFromResource();
        GetGroupPrefab();
        InstantiateCards();
    }
    //Only works In case of camera orthographic
    void SetDropObjToCorner()
    {
        float x = Camera.main.orthographicSize*Camera.main.aspect - mCardWidth/2f;

        mDropObj.transform.position = new Vector3(x,m_YPosOfCard,0);
        mDropObj.SetActive(false);
    }
    private void GetCardDataFromResource()
    {
        var tAsset = mReader.ReadFromResource<TextAsset>(Consts.RESOURCES_CARD_JSON_PATH);
        mCardsJsonData = JsonUtility.FromJson<CardsJsonData>(tAsset.text);
        mReader.UnloadResource<TextAsset>(tAsset);
    }
    private void GetGroupPrefab()
    {
        mGroupObj = mReader.ReadFromResource<GameObject>(Consts.RESOURCES_PREFAB_GROUP);
    }
    private void GetCardPrefabFromResource()
    {
        mCardPrefab = mReader.ReadFromResource<GameObject>(Consts.RESOURCES_PREFAB_CARD);
    }
    private void GetCardAtlas()
    {
        mCardAtlas = mReader.ReadFromResource<SpriteAtlas>(Consts.RESOURCES_ATLAS_CARD);
    }


    private void InstantiateCards()
    {
        var deck = mCardsJsonData.data.deck;
        var noOfCardsInDeck = deck.Length;
        if(mCardList==null) mCardList = new List<Card>();
        
        for(int i =0;i<noOfCardsInDeck;i++)
        {
            GameObject card = InstantiateCard(Vector3.zero,deck[i]);
            card.transform.SetParent(m_CardParent,true);
            card.GetComponent<Card>().SetData(1,this,noOfCardsInDeck-i);
            mCardList.Add(card.GetComponent<Card>());
        }
        mTotalGroupNo = 1;
        ShiftCardsInX(null);
    }

    private GameObject InstantiateCard(Vector3 position,string cardName)
    {
        var obj = Instantiate<GameObject>(mCardPrefab,position,Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite = mCardAtlas.GetSprite(cardName);
        obj.name = cardName;
        return obj;
    }
    public void OnCardMouseDown(Card card)
    {
        mMovingCardPreviousPos = card.transform.position;
        mCurrentCardIndex = mCardList.IndexOf(card);
        mCurrentCardGroupNo = card.pGroupNo;
        m_GroupButton.gameObject.SetActive(false);
    }
    public void OnCardMouseDrag(Card card)
    {
        UpdateRenderer(card);
        if(mTotalGroupNo<m_MaxGroupNo && !mDropObj.activeInHierarchy)
        {
            mDropObj.SetActive(true);
        }
    }
    public void OnCardMouseUp(Card card)
    {
        if(!GroupCreated(card))
        {
            if(mCurrentCardIndex!=mCardList.IndexOf(card)||mCurrentCardGroupNo!=card.pGroupNo)
            {
                card.pCardStatus = CardStatus.DOWN;
            }
            ResetTotalGroup();
        }
        ShiftCardsInX(null);
        mDropObj.SetActive(false);
        ShowGroupButton();
    }
    bool GroupCreated(Card card)
    {
        if(mTotalGroupNo<m_MaxGroupNo && IsOverlappingCards(card.transform,mDropObj.transform))
        {
            int cardPreviousIndex = mCardList.IndexOf(card);
            
            card.pGroupNo = ++mTotalGroupNo;

            mCardList.Insert(mCardList.Count,card);
            mCardList.RemoveAt(cardPreviousIndex);
            ResetTotalGroup();
            card.pCardStatus = CardStatus.DOWN;
            return true;
        }
        return false;
    }
    void ResetTotalGroup()
    {
        int initialGroup = 0;
        int currentGroupNo = 0; 
        for(int i =0;i<mCardList.Count;i++)
        {
            if(mCardList[i].pGroupNo-currentGroupNo>0)
            {
                initialGroup++;
                currentGroupNo = mCardList[i].pGroupNo;
            }
            mCardList[i].pGroupNo = initialGroup;
            mCardList[i].pZOrder = mCardList.Count-i;
        }
        mTotalGroupNo = initialGroup;
    }
    void UpdateRenderer(Card card)
    {
        if(CardYPOSInDeckArea(card.transform))
        {
            Tuple<int,int> indexes = GetBeforOrAfterCardsIndexes(card);
            int beforeIndex = indexes.Item1;
            int afterIndex = indexes.Item2;
            if(beforeIndex!=-1 || afterIndex!=-1)
            {
                int desiredIndex = beforeIndex!=-1?beforeIndex+1:afterIndex-1;
                int cardToComapreIndex = beforeIndex!=-1?beforeIndex:afterIndex;
                int cardToComapareGroupNo = mCardList[cardToComapreIndex].pGroupNo;
                if(desiredIndex<0) desiredIndex = 0;
                int currentCardIndex = mCardList.IndexOf(card);
                if(currentCardIndex!=desiredIndex || card.pGroupNo!=cardToComapareGroupNo)
                {
                    mCardList.Insert(desiredIndex,card);
                    if(currentCardIndex>desiredIndex) currentCardIndex++;
                    mCardList.RemoveAt(currentCardIndex);
                    ResetZOrder();
                    ShiftCardsInX(card);
                    card.transform.position = new Vector3(card.transform.position.x,card.transform.position.y,
                    mCardList.Count-card.pZOrder);
                    
                    
                    card.pGroupNo = cardToComapareGroupNo;
                }

            }

        }
    }
    Tuple<int,int> GetBeforOrAfterCardsIndexes(Card movingCard)
    {
        int beforeIndex = -1;
        int afterIndex = -1;

        for(int i =mCardList.Count-1;i>=0;i--)
        {
            
            if(movingCard.gameObject.Equals(mCardList[i].gameObject))continue;
            if(HaveGreaterRenderOrder(movingCard.transform,mCardList[i].transform)) 
            {
                beforeIndex = i;
                break;
            }
        }
        if(beforeIndex==-1)
        {
            for(int i =0;i<mCardList.Count;i++)
            {
                if(movingCard.gameObject.Equals(mCardList[i].gameObject))continue;
                if(HaveGreaterRenderOrder(mCardList[i].transform,movingCard.transform)) 
                {
                    afterIndex = i;
                    break;
                }
            }
        }
        Tuple<int,int> cardsIndexes = new Tuple<int, int>(beforeIndex,afterIndex);
        return cardsIndexes;
    }
    void ResetZOrder()
    {
        for(int i=0;i<mCardList.Count;i++)
        {
            mCardList[i].SetZOrder(mCardList.Count-i);
        }
    }
    private bool HaveGreaterRenderOrder(Transform movingCard,Transform OtherCard)
    {
        return movingCard.position.x>OtherCard.position.x && movingCard.position.x - OtherCard.position.x<mCardWidth;
    }
    private bool CardYPOSInDeckArea(Transform movingCard)
    {
        return Mathf.Abs(movingCard.position.y-m_YPosOfCard)<=mCardHeight;
    }
    private void ShiftCardsInX(Card movingCard)
    {
        for(int i=0;i<mCardList.Count;i++)
        {
            if(movingCard!=null && movingCard.gameObject.Equals(mCardList[i].gameObject)) continue;
            mCardList[i].transform.position = PositionOfCard(mCardList[i]);
        }

    }
    private bool IsOverlappingCards(Transform card1,Transform card2)
    {
        return Mathf.Abs(card1.position.x-card2.position.x)<=mCardWidth &&
        Mathf.Abs(card1.position.y-card2.position.y)<=mCardHeight;
    }
    private Vector3 PositionOfCard(Card card)
    {
        int cardNo = mCardList.Count- card.pZOrder+1;
        int groupNo = card.pGroupNo;
        float initialX = -TotalLengthOfDeck()/2;
        float x = (groupNo-1)*m_XGapBetweenGroup + (cardNo-groupNo)*m_XGapBetweenCard+initialX;
        float y = card.pCardStatus==CardStatus.DOWN?m_YPosOfCard:m_YPosOfCard+m_ShiftInY;
        float z = card.pZOrder;
        return new Vector3(x,y,z);
    }
    private float TotalLengthOfDeck()
    {
        int totalCard = mCardList.Count;
        int totalGroup = mCardList[totalCard-1].pGroupNo;
        return (totalCard-totalGroup)*m_XGapBetweenCard+(totalGroup-1)*m_XGapBetweenGroup;
    }

    void ShowGroupButton()
    {
        int noOfUpCards = 0;

        for(int i =0;i<mCardList.Count;i++)
        {
            if(mCardList[i].pCardStatus==CardStatus.UP) noOfUpCards++;
        }
        if(mTotalGroupNo>=m_MaxGroupNo || noOfUpCards<=1)
        {
            m_GroupButton.gameObject.SetActive(false);

        }
        else
        {
            m_GroupButton.gameObject.SetActive(true);
        }
    }
    public void ClickGroupButton()
    {
        m_GroupButton.gameObject.SetActive(false);
        if(mTotalGroupNo<m_MaxGroupNo)
        {
            mTotalGroupNo++;
            List<int> indexToRemove = new List<int>();
            List<Card> cardsToGroup = new List<Card>(); 
            for(int i=0;i<mCardList.Count;i++)
            {
                if(mCardList[i].pCardStatus==CardStatus.UP) 
                {
                    Card c = mCardList[i];
                    c.pCardStatus = CardStatus.DOWN;
                    c.pGroupNo = mTotalGroupNo;
                    indexToRemove.Add(i);
                    cardsToGroup.Add(c);
                }
                    
            }
            for(int i =0;i<cardsToGroup.Count;i++)
            {
                mCardList.Add(cardsToGroup[i]);
            }
            for(int i =indexToRemove.Count-1;i>=0;i--)
            {
                mCardList.RemoveAt(indexToRemove[i]);
            }
            indexToRemove.Clear();
            cardsToGroup.Clear();
            ResetTotalGroup();
            ShiftCardsInX(null);
        }
    }
}