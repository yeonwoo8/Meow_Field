using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController instance;

    public GameObject inventoryPanel;
    public Text messageText;
    public List<GameObject> slots;

    private bool isInventoryOpen = false;


    /// <summary>
    /// 아이템 추가 (GameManager가 런타임 인스턴스 관리)
    /// </summary>
    public void AddItem(Item newItem)
    {
        GameManager.instance.AddItem(newItem);
        UpdateInventoryUI();
    }

    public void RemoveItem(Item newItem)
    {
        GameManager.instance.RemoveItem(newItem);
        UpdateInventoryUI();
    }

    /// <summary>
    /// 인벤토리 UI 갱신
    /// </summary>
    public void UpdateInventoryUI()
    {
        var itemData = GameManager.instance.itemData;
        var inventoryItems = GameManager.instance.inventoryItems;

        int index = 0;
        foreach (var kvp in itemData)
        {
            if (index >= slots.Count) break;

            var slot = slots[index];
            Image itemImage = slot.transform.GetChild(0).GetComponent<Image>();
            Text quantityText = slot.transform.GetChild(1).GetComponent<Text>();

            Item foundItem = inventoryItems.Find(item => item.itemName == kvp.Key);
            if (foundItem != null)
            {
                itemImage.sprite = foundItem.itemIcon;
                itemImage.color = Color.white;
                quantityText.text = kvp.Value.ToString();
                quantityText.gameObject.SetActive(true);
            }
            else
            {
                // 아이템 못 찾으면 빈 슬롯 처리
                itemImage.sprite = null;
                itemImage.color = new Color(1, 1, 1, 0);
                quantityText.text = "";
                quantityText.gameObject.SetActive(false);
            }
            index++;
        }

        // 남은 슬롯은 비우기
        for (; index < slots.Count; index++)
        {
            var slot = slots[index];
            Image itemImage = slot.transform.GetChild(0).GetComponent<Image>();
            Text quantityText = slot.transform.GetChild(1).GetComponent<Text>();
            itemImage.sprite = null;
            itemImage.color = new Color(1, 1, 1, 0);
            quantityText.text = "";
            quantityText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 인벤토리 초기화 (아이템 데이터 비움)
    /// </summary>
    public void ClearInventory()
    {
        GameManager.instance.itemData.Clear();
        GameManager.instance.inventoryItems.Clear();
        Debug.Log("[ClearInventory] itemData와 inventoryItems 초기화 완료");
        UpdateInventoryUI();
    }

    /// <summary>
    /// 인벤토리 열고 닫기
    /// </summary>
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Debug.Log("[ToggleInventory] 인벤토리 열림 → UpdateInventoryUI 호출");
            UpdateInventoryUI();
        }
    }

    /// <summary>
    /// 메시지 출력
    /// </summary>
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

    /// <summary>
    /// 현재 itemData 상태 출력
    /// </summary>
    void PrintItemData()
    {
        Debug.Log("=== [itemData 상태 출력] ===");
        if (GameManager.instance.itemData.Count == 0)
        {
            Debug.Log("itemData가 비어있습니다.");
        }
        else
        {
            foreach (var kvp in GameManager.instance.itemData)
            {
                Debug.Log($"아이템: {kvp.Key}, 수량: {kvp.Value}");
            }
        }
        Debug.Log("=======================");
    }
}
