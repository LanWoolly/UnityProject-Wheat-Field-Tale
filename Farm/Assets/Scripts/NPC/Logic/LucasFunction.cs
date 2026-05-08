using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Farm.Inventory;

public class LucasFunction : MonoBehaviour
{
    private GameObject submitPanel;
    public InventoryItem inventoryItem;
    private ItemDetails itemDetails;
    [SceneName] public string SceneName;
    public GameObject Box;

    private void Start()
    {
        submitPanel = GameObject.FindWithTag("NPCSubmitUI");
        submitPanel.SetActive(false);
        Box.SetActive(false);
    }

    private void Update()
    {
        if (SceneName != SceneManager.GetActiveScene().name)
        {
            Box.SetActive(false);
        }
    }

    public void OpenSubmitUI()
    {
        submitPanel.SetActive(true);
        itemDetails = InventoryManager.Instance.GetItemDetails(inventoryItem.itemID);
        submitPanel.GetComponent<NPCSubmitUI>().SetUpSubmitUI(itemDetails, inventoryItem, this);
        EventHandler.CallUpdateGameStateEvent(GameState.Pause);
    }
}
