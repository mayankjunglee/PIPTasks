using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform mContent;
    [SerializeField] GameObject mStoreItem;
    [SerializeField] InvControlller mcontroller;
    private void OnEnable() {
        ShowUI();
    }
    public void ShowUI()
    {
        DeletePreviousItems();
        PlayerData playerData = PlayerDataManager.LoadPlayerData();
        for(int i=0;i<playerData.playerDataList.Count;i++)
        {
            var obj = Instantiate<GameObject>(mStoreItem);
            obj.transform.SetParent(mContent,false);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<InventoryItem>().SetData(playerData.playerDataList[i]);
        }
    }
    void DeletePreviousItems()
    {
        for(int i = mContent.childCount-1;i>=0;i--)
        {
            Destroy(mContent.GetChild(i).gameObject);
        }
    }
    public void ShowStore()
    {
        mcontroller.OnStoreOpenClicked();
    }
}
