using UnityEngine;
using Farm.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;

namespace Farm.Explore
{
    public class Entrance : MonoBehaviour
    {
        [SceneName]
        public string sceneToGo;
        public Vector3 positionToGo;
        public bool isActivated;
        private SubmitUI submitUI;
        private EntranceDetail currentEntranceDetail;

        // private void OnEnable()
        // {
        //     EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        // }

        private void Start()
        {
            currentEntranceDetail = ExploreManager.Instance.exploreScene_SO.GetEntranceDetail(sceneToGo);
            isActivated = currentEntranceDetail.isActivated;
            submitUI = ExploreManager.Instance.submitUI;
            submitUI.gameObject.SetActive(false);
        }

        // private void OnDisable()
        // {
        //     EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        // }

        // private void OnAfterSceneLoadedEvent()
        // {
        //     submitUI = SubmitUI.Instance;
        //     submitUI.gameObject.SetActive(false);
        // }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (isActivated)
                {
                    EventHandler.CallTransitionEvent(sceneToGo, positionToGo);
                }
                else
                {
                    //入口未激活，显示提交界面
                    ShowSubmitUI();
                    submitUI.gameObject.SetActive(true);
                }
            }
        }

        private void ShowSubmitUI()
        {
            EventHandler.CallUpdateGameStateEvent(GameState.Pause);
            if (currentEntranceDetail != null)
            {
                InventoryItem requiredItem = currentEntranceDetail.requiredItemList[0];
                ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(requiredItem.itemID);
                //传递数据
                submitUI.SetUpSubmitUI(itemDetails, requiredItem, this);
            }
            else
            {
                Debug.LogError("入口详情为空");
            }
        }


    }
}