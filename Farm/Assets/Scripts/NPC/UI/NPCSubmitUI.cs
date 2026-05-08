using UnityEngine;
using UnityEngine.UI;
using Farm.Inventory;
using System.Collections;
using System.Collections.Generic;

public class NPCSubmitUI : MonoBehaviour
{
    public Image itemIcon;
    public Text amountText;
    public Button submitButton;
    public Button exitButton;
    private int requiredAmount;
    private int playerAmount;
    private InventoryItem currentInventory;
    private GameObject box;

    //玩家背包数据
    [SerializeField] private InventoryBag_SO playerBag;

    private void OnEnable()
    {
        submitButton.onClick.AddListener(SubmitButton_Click);
        exitButton.onClick.AddListener(ExitButton_Click);
    }

    private void OnDisable()
    {
        submitButton.onClick.RemoveListener(SubmitButton_Click);
        exitButton.onClick.RemoveListener(ExitButton_Click);
    }

    public void SetUpSubmitUI(ItemDetails requiredItem, InventoryItem inventoryItem, LucasFunction lucasFunction)
    {
        playerBag = InventoryManager.Instance.playerBag_SO;
        box = lucasFunction.Box;
        itemIcon.sprite = requiredItem.itemIcon;
        this.currentInventory = inventoryItem;
        int requiredAmount = inventoryItem.itemAmount;
        this.requiredAmount = requiredAmount;
        //获取玩家背包中对应的物品数量
        int playerAmount = GetPlayerAmount(requiredItem.itemID);
        this.playerAmount = playerAmount;
        amountText.text = "" + requiredAmount + "/" + playerAmount;
    }

    private int GetPlayerAmount(int itemID)
    {
        if (playerBag != null)
        {
            foreach (var item in playerBag.itemList)
            {
                if (item.itemID == itemID)
                {
                    return item.itemAmount;
                }
            }
        }
        return 0;
    }

    private void SubmitButton_Click()
    {
        if (playerAmount < requiredAmount)
        {
            Debug.Log("物品数量不够，无法提交");
            return;
        }
        else
        {
            InventoryManager.Instance.RemoveItem(currentInventory.itemID, currentInventory.itemAmount);
            EventHandler.CallUpdateGameStateEvent(GameState.GamePlay);
            box.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void ExitButton_Click()
    {
        EventHandler.CallUpdateGameStateEvent(GameState.GamePlay);
        this.gameObject.SetActive(false);
    }
}
