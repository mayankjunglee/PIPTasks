
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
public class PlayerDataManager
{
    public PlayerData LoadPlayerData()
    {
        var pData = new PlayerData();
        XmlSerializer ser = new XmlSerializer(typeof(PlayerData));
        string text = PlayerPrefs.GetString("PLAYER_DATA");
        if(text.Length!=0)
        {
            using (var reader = new System.IO.StringReader(text))
            {
                pData = ser.Deserialize(reader) as PlayerData;
            }
        }
        return pData;
    }
    public void SavePlayerData(PlayerDataOneUnit unit)
    {
        // PlayerPrefs.DeleteAll();
        PlayerData data = LoadPlayerData();
        data.playerDataList.Add(unit);
        XmlSerializer ser = new XmlSerializer(typeof(PlayerData));
        using(StringWriter sw = new StringWriter())
        {
            ser.Serialize(sw,data);
            Debug.Log(sw.ToString());
            PlayerPrefs.SetString("PLAYER_DATA",sw.ToString());
        }
    }
}
