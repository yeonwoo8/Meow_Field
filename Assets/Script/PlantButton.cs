using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlantButton : MonoBehaviour
{
    public InventoryController inventoryController;
    public GrowController growController;

    public Text messageText;  // 심은 메시지를 보여줄 UI 텍스트
    public List<Button> groundButtons;  // 여러 개의 땅 (버튼)
    public Sprite holeSprite;  // 초기 땅 이미지
    public Sprite afterplantSprite;  // 심어진 후의 땅 이미지

    public List<Item> seeds;  // 여러 종류의 씨앗
    private int selectedGroundIndex = -1;  // 선택된 땅 인덱스

    void Start()
    {
        // 각 땅 버튼에 클릭 이벤트 추가
        for (int i = 0; i < groundButtons.Count; i++)
        {
            int index = i;  // 람다 캡처 문제 방지
            groundButtons[i].onClick.AddListener(() => SelectGround(index));
        }
    }

    // 클릭된 땅을 선택
    public void SelectGround(int index)
    {
        selectedGroundIndex = index;
        ShowMessage("땅 " + (index + 1) + " 선택됨");
    }

    // 씨앗을 심는 함수
    public void PlantSeed(string seedName)
    {

        if (selectedGroundIndex == -1)
        {
            ShowMessage("땅을 선택하세요!");
            return;
        }

        Item seed = seeds.Find(s => s.itemName == seedName);

        if (seed != null && seed.quantity > 0)
        {

             seed.quantity--;  // 씨앗 개수 감소
            groundButtons[selectedGroundIndex].image.sprite = afterplantSprite;  // 선택한 땅의 이미지 변경

            ShowMessage(seedName + "이(가) 심어졌습니다.");

            growController.Growing(selectedGroundIndex,seedName);

            inventoryController.RemoveItem(seed);

        }
        else
        {
            ShowMessage(seedName + "이(가) 부족합니다.");
        }

        selectedGroundIndex = -1;  // 선택 초기화
    }

    // 메시지 출력 함수
    void ShowMessage(string message)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        Invoke("HideMessage", 2f);  // 2초 후 메시지 숨기기
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}
