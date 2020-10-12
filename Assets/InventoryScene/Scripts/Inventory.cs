using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() 
    {
        new PlayerDataManager().SavePlayerData(new PlayerDataOneUnit{
            ItemID = "1",
            ItemValue = 1
        });
    }
}
