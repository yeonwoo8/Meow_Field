using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    Seed,   // 씨앗 아이템
    fruit, //과일
    material,
    ETC     // 기타 아이템
}

public class Item : MonoBehaviour  // MonoBehaviour로 수정하여 GameObject에 추가 가능하게 함
{
    public ItemType itemtype;  // 아이템 타입 (Seed, ETC 등)
    public string itemName;    // 아이템 이름
    public Sprite itemIcon;    // 아이템 아이콘
    //public GameObject itemGameObject;  // 아이템의 GameObject 참조
    public bool hasCollided = false; // 충돌 여부 추적 변수
    public int quantity = 1; // 아이템 수량 (기본값은 1)


    // 기본 생성자 (기존 코드에서 사용되는 생성자)
    public Item()
    {
        itemName = "Unknown";
        quantity = 0;
    }

    // 두 개의 인수를 받는 생성자 추가
    public Item(string name, int qty)
    {
        itemName = name;
        quantity = qty;
        // itemIcon에 대한 기본값 설정을 원하면 추가할 수 있습니다
    }

    // 아이템 사용 메소드
    public bool use()
    {
        // 예시로 사용하지 않도록 false를 반환
        return false;
    }
}
