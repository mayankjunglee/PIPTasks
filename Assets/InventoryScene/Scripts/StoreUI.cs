using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    [SerializeField] TextAsset mStoreJSON;
    [SerializeField] Transform mContent;
    [SerializeField] GameObject mStoreItem;
    [SerializeField] InvControlller mcontroller;
    private void OnEnable() {
        // PlayerPrefs.DeleteAll();
        ShowUI();
    }
    public void ShowMyItems()
    {
        mcontroller.OnInventoryOpenClicked();
    }
    public void ShowUI()
    {
        DeletePreviousItems();
        StoreData storeData = GetUnpurchasedData();
        for(int i=0;i<storeData.store_data.Count;i++)
        {
            var obj = Instantiate<GameObject>(mStoreItem);
            obj.transform.SetParent(mContent,false);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<InventoryItem>().SetData(storeData.store_data[i],this);
        }
    }
    void DeletePreviousItems()
    {
        for(int i = mContent.childCount-1;i>=0;i--)
        {
            Destroy(mContent.GetChild(i).gameObject);
        }
    }
    StoreData GetUnpurchasedData()
    {
        StoreData storeData = JsonUtility.FromJson<StoreData>(mStoreJSON.text);
        PlayerData playerData = PlayerDataManager.LoadPlayerData();

        for(int i = storeData.store_data.Count-1;i>=0;i--)
        {
            for(int j =0;j<playerData.playerDataList.Count;j++)
            {
                if(storeData.store_data[i].item_id.Equals(playerData.playerDataList[j].ItemID))
                {
                    storeData.store_data.RemoveAt(i);
                    break;
                }
            }
        }

        return storeData;
    }
}
