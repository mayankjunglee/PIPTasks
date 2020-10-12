using System.Collections.Generic;
using System.Text;
[System.Serializable]
public class StoreData 
{
    public List<StoreOneItem> store_data;
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        if(store_data!=null)
        {
            for(int i =0;i<store_data.Count;i++)
            {
                sb.Append(store_data[i]+"\n");
            }
        }
        return sb.ToString();
    }
}
[System.Serializable]
public class StoreOneItem
{
    public string item_id;
    public int item_value;
    public string item_name;
    public override string ToString()
    {
        return $"item id {item_id}, item value {item_value}, item Name {item_name}";
    }
}