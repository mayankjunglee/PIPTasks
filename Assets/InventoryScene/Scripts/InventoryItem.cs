using UnityEngine;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Text mNameText;
    [SerializeField] private Text mValueText;

    private string mid;
    private string mname;
    private int mvalue;
    private StoreUI mStoreUI;
    public void SetData(StoreOneItem data,StoreUI storeUI)
    {
        mid = data.item_id;
        mname = data.item_name;
        mvalue = data.item_value;
        mNameText.text = mname;
        if(mValueText!=null)
        {
            mValueText.text = mvalue.ToString()+" rs";
        }
        mStoreUI = storeUI;
    }

    public void SetData(PlayerDataOneUnit data)
    {
        mname = data.ItemName;
        mNameText.text = mname;
    }
    public void OnPurchase()
    {
        PlayerDataOneUnit data = new PlayerDataOneUnit{
            ItemID = mid,
            ItemName = mname,
            ItemValue = mvalue
        };
        PlayerDataManager.SavePlayerData(data);
        mStoreUI.ShowUI();
    }
}
