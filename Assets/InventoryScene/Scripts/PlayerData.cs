
using System;
using System.Collections.Generic;
using System.Text;
[System.Serializable]
public class PlayerData 
{
    public List<PlayerDataOneUnit> playerDataList;
    public PlayerData()
    {
        playerDataList = new List<PlayerDataOneUnit>();
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        if(playerDataList!=null)
        {
            for(int i=0;i<playerDataList.Count;i++)
            {
                sb.Append(playerDataList[i].ToString()+"\n");
            }
        }
        return sb.ToString();
    }
}
[System.Serializable]
public class PlayerDataOneUnit
{
    public string ItemID;
    public int ItemValue;
    public string ItemName;
    public override string ToString()
    {
        return $"ItemID {ItemID}, ItemValue {ItemValue}, ItemName {ItemName}";
    }
}
