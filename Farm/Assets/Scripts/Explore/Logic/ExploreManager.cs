using System;
using System.Collections;
using System.Collections.Generic;
using Farm.Save;
using UnityEngine;

namespace Farm.Explore
{
    public class ExploreManager : Singleton<ExploreManager>, ISaveable
    {
        public ExploreScene_SO exploreScene_SO;
        public SubmitUI submitUI;

        public string GUID => GetComponent<DataGUID>().guid;

        private void OnEnable()
        {
            EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        }

        private void Start()
        {
            ISaveable saveable = this;
            saveable.RegisterSaveable();
        }

        private void OnDisable()
        {
            EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        }


        private void OnAfterSceneLoadedEvent()
        {
            submitUI = SubmitUI.Instance;
            submitUI.gameObject.SetActive(false);
        }

        public bool ActivateEntrance(Entrance entrance)
        {
            if (entrance == null)
            {
                return false;
            }
            else
            {
                entrance.isActivated = true;
                EntranceDetail entranceDetail = exploreScene_SO.GetEntranceDetail(entrance.sceneToGo);
                entranceDetail.isActivated = true;
                return true;
            }
        }

        public GameSaveData GenerateSaveData()
        {
            GameSaveData saveData = new GameSaveData();
            saveData.entranceDict = new Dictionary<string, EntranceDetail>();

            foreach (var item in exploreScene_SO.entranceDetailList)
            {
                saveData.entranceDict.Add(item.sceneName, item);
            }

            return saveData;
        }

        public void RestoreData(GameSaveData saveData)
        {
            for (int i = 0; i < exploreScene_SO.entranceDetailList.Count; i++)
            {
                EntranceDetail currentEntranceDetail = exploreScene_SO.entranceDetailList[i];
                if (saveData.entranceDict.ContainsKey(currentEntranceDetail.sceneName))
                {
                    EntranceDetail savedDetail = saveData.entranceDict[currentEntranceDetail.sceneName];
                    exploreScene_SO.entranceDetailList[i] = savedDetail;
                }
            }
        }
    }
}