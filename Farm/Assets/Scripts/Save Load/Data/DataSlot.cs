using UnityEngine;
using Farm.Transition;
using System.Collections.Generic;

namespace Farm.Save
{
    public class DataSlot
    {
        //进度条，String是GUID
        public Dictionary<string, GameSaveData> dataDict = new Dictionary<string, GameSaveData>();

        #region 用来UI显示进度详情
        public string DataTime
        {
            get
            {
                var key = TimeManager.Instance.GUID;

                if (dataDict.ContainsKey(key))
                {
                    var timeData = dataDict[key];
                    return timeData.timeDict["gameYear"] + "年/" + (Season)timeData.timeDict["gameSeason"] + "/" + timeData.timeDict["gameMonth"] + "月/" + timeData.timeDict["gameDay"] + "日/";
                }
                else
                {
                    Debug.Log(key);
                    return string.Empty;
                }
            }
        }

        public string DataScene
        {
            get
            {
                var key = TransitionManager.Instance.GUID;
                if (dataDict.ContainsKey(key))
                {
                    var transitionData = dataDict[key];
                    return transitionData.dataSceneName switch
                    {
                        "00Start" => "海边",
                        "01Field" => "农场",
                        "02Home" => "木屋",
                        "03Stall" => "商贩",
                        _ => string.Empty,
                    };
                }
                else return string.Empty;
            }
        }
        #endregion

    }
}