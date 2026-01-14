using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExchangeShop : MonoBehaviour
{
    private bool isExchangeOpen = false;
    public List<GameObject> ExchangePanels;
    private int page = 0;

    public InventoryController inventoryController;

    public Item tomato_seed;
    public Item pea_seed;
    public Item eggplant_seed;

    int coinCount = GameManager.coin;

    public Text messageText;

    public void ToggleExchange()
    {
        isExchangeOpen = !isExchangeOpen;
        ExchangePanels[page].SetActive(isExchangeOpen);

        Debug.Log($"[ToggleExchange] isExchangeOpen: {isExchangeOpen}");
    }

    public void OnRedButtonClicked()
    {
        ExchangeItem("red_seed", 3, tomato_seed, "3개의 red_seed를 교환하여 tomato_seed를 추가했습니다.");
    }

    public void OnPurpleButtonClicked()
    {
        ExchangeItem("purple_seed", 3, eggplant_seed, "3개의 purple_seed를 교환하여 eggplant_seed를 추가했습니다.");
    }

    public void OnGreenButtonClicked()
    {
        ExchangeItem("grn_seed", 3, pea_seed, "3개의 grn_seed를 교환하여 pea_seed를 추가했습니다.");
    }

    private void ExchangeItem(string seedName, int requiredQuantity, Item newItem, string successMessage)
    {
        var itemData = GameManager.instance.itemData;

        if (itemData.TryGetValue(seedName, out int quantity))
        {
            if (quantity >= requiredQuantity)
            {
                itemData[seedName] -= requiredQuantity;
                inventoryController.AddItem(newItem);
                ShowMessage(successMessage);
                Debug.Log($"[ExchangeItem] {seedName} -{requiredQuantity}, 새 아이템 추가: {newItem.itemName}");
            }
            else
            {
                ShowMessage($"{seedName} 개수가 부족합니다.");
                Debug.Log($"[ExchangeItem] {seedName} 개수 부족");
            }
        }
        else
        {
            ShowMessage($"{seedName}를 가지고 있지 않습니다.");
            Debug.Log($"[ExchangeItem] {seedName} 없음");
        }

    }

    public void ClickButton(string button)
    {
        int p_size = ExchangePanels.Count;

        if (button == "left")
        {
            if (page == 0) return;

            ExchangePanels[page].SetActive(false);
            page--;
            ExchangePanels[page].SetActive(true);
        }
        else
        {
            if (page >= p_size - 1) return;

            ExchangePanels[page].SetActive(false);
            page++;
            ExchangePanels[page].SetActive(true);
        }

        Debug.Log($"[ClickButton] 현재 페이지: {page}");
    }

    public void SELL(Item item)
    {
        if (item.quantity < 1)
        {
            ShowMessage($"{item.itemName}의 개수가 부족합니다.");
            Debug.Log($"[SELL] {item.itemName} 개수 부족");
            return;
        }

        int price = GetSellPrice(item.itemName);
        if (price > 0)
        {
            item.quantity -= 1;
            ShowMessage($"{item.itemName}를 팔았습니다. (+{price}코인)");
            Debug.Log($"[SELL] {item.itemName} 판매 완료, +{price} 코인");

            GameManager.coin += price;
            inventoryController.RemoveItem(item);

        }
        else
        {
            ShowMessage($"{item.itemName}는 팔 수 없는 아이템입니다.");
            Debug.Log($"[SELL] {item.itemName}는 판매 불가");
        }
    }

    private int GetSellPrice(string itemName)
    {
        switch (itemName)
        {
            case "tomato": return 5;
            case "pea": return 3;
            case "egg_plant": return 7;
            default: return 0;
        }
    }

    void ShowMessage(string message)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        Invoke("HideMessage", 2f);
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}
