using System.Collections;
using System.Collections.Generic;
using Farm.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "BuiPrintDataList_SO", menuName = "Inventory/BuiPrintDataList_SO")]
public class BuiPrintDataList_SO : ScriptableObject
{
    public List<BluePrintDetails> bluePrintDataList;

    public BluePrintDetails GetBluePrintDetalis(int itemID)
    {
        return bluePrintDataList.Find(b => b.ID == itemID);
    }
}

[System.Serializable]
public class BluePrintDetails
{
    public int ID;
    public InventoryItem[] resourceItem = new InventoryItem[4];
    public GameObject buildPrefab;
}