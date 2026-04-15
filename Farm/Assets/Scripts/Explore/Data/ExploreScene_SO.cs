using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExploreScene_SO", menuName = "Explore/ExploreScene_SO")]
public class ExploreScene_SO : ScriptableObject
{
    public List<EntranceDetail> entranceDetailList;

    public EntranceDetail GetEntranceDetail(string sceneName)
    {
        foreach (var Details in entranceDetailList)
        {
            if (Details.sceneName == sceneName)
            {
                return Details;
            }
        }
        return null;
    }
}

[System.Serializable]
public class EntranceDetail
{
    [SceneName] public string sceneName;
    public List<InventoryItem> requiredItemList;
    public bool isActivated;//是否激活入口
}