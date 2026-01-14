using UnityEngine;
using UnityEngine.UI;

public class PlantController : MonoBehaviour
{
    private InventoryController inventory;  // 인벤토리 컨트롤러 참조

    public GameObject PlantPanel;
    private bool isPlantPanelOpen = false;

    public Text tomatoText;
    public Text peaText;
    public Text eggplantText;

    void Awake()
    {
        inventory = FindFirstObjectByType<InventoryController>();
    }

    void Start()
    {
        UpdateItemText();
    }


    void Update()
    {
        UpdateItemText();
    }

    void UpdateItemText()
    {
        var itemData = GameManager.instance.itemData;

        tomatoText.text = "X " + GetQuantity(itemData, "tomato_seed");
        peaText.text = "X " + GetQuantity(itemData, "pea_seed");
        eggplantText.text = "X " + GetQuantity(itemData, "eggplant_seed");
    }

    int GetQuantity(System.Collections.Generic.Dictionary<string, int> itemData, string itemName)
    {
        return itemData.TryGetValue(itemName, out int quantity) ? quantity : 0;
    }

    public void TogglePlant()
    {
        isPlantPanelOpen = !isPlantPanelOpen;
        PlantPanel.SetActive(isPlantPanelOpen);
    }
}
