using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMaster : MonoBehaviour
{
    Stack<PopUPData> mPopUPStack;

    [SerializeField] Transform mPopUPHolder;
    [SerializeField] GameObject mPopUpPrefab;

    [SerializeField] GameObject background;
    [SerializeField] GameObject clearAllButton;
    private void Awake() 
    {
        ActivateBG(false);
        //Just for Dummy Data
        AddDummyDataForPopUps();
        ShowPopUpFromStack();
    }

    void AddDummyDataForPopUps()
    {
        AddDataToPopUp("Heading 1","Some body text");
        AddDataToPopUp("Heading 2","Some body text for popup 2");
        AddDataToPopUp("Heading 1","Some Long body text for scroll.\n..........\n.\n............\n.\n.\n.\n......\n.down\n.\n............\n.\n.\n.\n.\n.\n.\n..........\n.\n.down\n.\n.\n.\n........\n.\n.\n.\n............\n.\n.\n.............\n.\n.\n.\n.\n.\n.........\n.down\n.\n.\n......\n.\n.\n.\n.\n.\n.......\n. See it works");
    }

    void ShowPopUpFromStack()
    {
        if(mPopUPStack!=null && mPopUPStack.Count>0)
        {
            PopUPData data = mPopUPStack.Pop();
            GameObject popup = Instantiate<GameObject>(mPopUpPrefab,Vector3.zero,Quaternion.identity);
            PopUP popUpScript = popup.GetComponent<PopUP>();
            popUpScript.SetData(data,PopUPOkButtonClicked,PopUPCancelButtonClicked);
            popup.transform.SetParent(mPopUPHolder,true);
            popup.transform.localScale = Vector3.one;
            popUpScript.OpenPopUp();
            ActivateBG(true);
        }
        else
        {
            ActivateBG(false);
        }
    }
    void ActivateBG(bool activate)
    {
        background.SetActive(activate);
        clearAllButton.SetActive(activate);
    }
    void PopUPOkButtonClicked()
    {
        ShowPopUpFromStack();
    }
    void PopUPCancelButtonClicked()
    {
        ShowPopUpFromStack();
    }
    public void AddDataToPopUp(string header,string body)
    {
        PopUPData data = new PopUPData{
            HeaderText = header,
            BodyText = body

        };
        if(mPopUPStack==null) mPopUPStack = new Stack<PopUPData>();
        mPopUPStack.Push(data);
    }
    public void ClearAllPopUP()
    {
        for(int i =mPopUPHolder.transform.childCount-1;i>=0;i--)
        {
            Destroy(mPopUPHolder.transform.GetChild(i).gameObject);
        }
        ActivateBG(false);
        if(mPopUPStack!=null) mPopUPStack.Clear();
    }
    public void BackButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
