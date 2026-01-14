using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NInventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Text messageText;
    public List<GameObject> slots;


    private bool isInventoryOpen = false;

    void Start()
    {
        if (GameManager.instance != null)
        {
            UpdateInventoryUI();             // 시작할 때 UI 갱신
        }
        else
        {
            Debug.LogError("GameManager 인스턴스를 찾을 수 없습니다!");
        }
    }

    public void UpdateInventoryUI()
    {
        // GameManager의 itemData 가져오기
        Dictionary<string, int> itemData = GameManager.instance.itemData;

        Debug.Log($"itemData Count: {GameManager.instance.itemData.Count}");

        foreach (var kvp in GameManager.instance.itemData)
        {
            Debug.Log($"itemData key: {kvp.Key}, value: {kvp.Value}");
        }


        // itemData 내용 출력
        Debug.Log("=== itemData 내용 ===");
        foreach (var kvp in itemData)
        {
            Debug.Log($"ItemName: {kvp.Key}, Quantity: {kvp.Value}");
        }
        Debug.Log("=====================");


        int index = 0;
        foreach (var kvp in itemData)
        {
            if (index >= slots.Count) break;

            string itemName = kvp.Key;
            int quantity = kvp.Value;

            Image itemImage = slots[index].transform.GetChild(0).GetComponent<Image>();
            Text quantityText = slots[index].transform.GetChild(1).GetComponent<Text>();

            // GameManager의 inventoryItems에서 아이콘 찾기
            Item foundItem = GameManager.instance.inventoryItems.Find(item => item.itemName == itemName);
            if (foundItem != null)
            {
                Debug.Log("아이템을 찾음");
                itemImage.sprite = foundItem.itemIcon;
                itemImage.color = Color.white;
                quantityText.text = quantity.ToString();
                quantityText.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("아이템을 찾지못함");
                // 못 찾으면 빈 슬롯 처리
                itemImage.sprite = null;
                itemImage.color = new Color(1, 1, 1, 0);
                quantityText.text = "";
                quantityText.gameObject.SetActive(false);
            }

            index++;
        }

        // 남은 슬롯 비우기
        for (int i = index; i < slots.Count; i++)
        {
            Image itemImage = slots[i].transform.GetChild(0).GetComponent<Image>();
            Text quantityText = slots[i].transform.GetChild(1).GetComponent<Text>();

            itemImage.sprite = null;
            itemImage.color = new Color(1, 1, 1, 0);
            quantityText.text = "";
            quantityText.gameObject.SetActive(false);
        }
    }

    public void AddItem(Item newItem)
    {
        GameManager.instance.AddItem(newItem);
        UpdateInventoryUI();
    }
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            Debug.Log($"inventoryItems Count: {GameManager.instance.inventoryItems.Count}");
            for (int i = 0; i < GameManager.instance.inventoryItems.Count; i++)
            {
                var item = GameManager.instance.inventoryItems[i];
                if (item == null)
                    Debug.Log($"inventoryItems[{i}]가 null입니다.");
                else
                    Debug.Log($"inventoryItems[{i}].itemName: {item.itemName}");
            }

            UpdateInventoryUI();

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