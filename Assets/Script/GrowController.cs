using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GrowController : MonoBehaviour
{
    public List<Button> groundButtons;  // 여러 개의 땅 (버튼)
    public List<Image> images;          // 각 버튼에 연결된 이미지
    public List<Sprite> sprites;
    public List<GameObject> alerts;


    public Item tomato;
    public Item pea;
    public Item eggplant;


    public InventoryController inventoryController;
    public Sprite original_hole;

    private string get_ItemName;
    private int get_Index;

    // 선택된 땅에서 성장 과정을 시뮬레이션
    public void Growing(int index, string seedName)
    {
        if (index < 0 || index >= groundButtons.Count) return;

        groundButtons[index].gameObject.SetActive(false);
        StartCoroutine(GrowSequence(index, seedName));
    }

    private System.Collections.IEnumerator GrowSequence(int index, string seedName)
    {

        if (index < 0 || index >= images.Count) yield break;

        images[index].sprite = sprites[0];
        images[index].gameObject.SetActive(true);


        if (seedName == "tomato_seed")
        {
            for (int i = 1; i < 7; i++)
            {
                yield return new WaitForSeconds(2f);
                images[index].sprite = sprites[i];
            }
            alerts[index].gameObject.SetActive(true);
            bringitem("tomato",index);
        }
        if (seedName == "pea_seed")
        {
            for (int i = 7; i < 13; i++)
            {
                yield return new WaitForSeconds(2f);
                images[index].sprite = sprites[i];
            }
            alerts[index].gameObject.SetActive(true);
            bringitem("pea",index);
        }
        if (seedName == "eggplant_seed")
        {
            for (int i = 13; i < 19; i++)
            {
                yield return new WaitForSeconds(2f);
                images[index].sprite = sprites[i];
            }
            alerts[index].gameObject.SetActive(true);
            bringitem("eggplant",index);
        }


    }


    public void bringitem(string itemName,int index)
    {
        get_ItemName = itemName;
        get_Index = index;
    }

    public void ClickHarvest()
    {
        if (get_ItemName == "tomato")
        {
            inventoryController.AddItem(tomato);
            alerts[get_Index].gameObject.SetActive(false);
            images[get_Index].gameObject.SetActive(false);
            groundButtons[get_Index].image.sprite = original_hole;
            groundButtons[get_Index].gameObject.SetActive(true);

        }
        else if (get_ItemName == "pea")
        {
            inventoryController.AddItem(pea);
            alerts[get_Index].gameObject.SetActive(false);
            images[get_Index].gameObject.SetActive(false);
            groundButtons[get_Index].image.sprite = original_hole;
            groundButtons[get_Index].gameObject.SetActive(true);
        }
        else if (get_ItemName == "eggplant")
        {
            inventoryController.AddItem(eggplant);
            alerts[get_Index].gameObject.SetActive(false);
            images[get_Index].gameObject.SetActive(false);
            groundButtons[get_Index].image.sprite = original_hole;
            groundButtons[get_Index].gameObject.SetActive(true);
        }

        
        inventoryController.UpdateInventoryUI();
    }

}
