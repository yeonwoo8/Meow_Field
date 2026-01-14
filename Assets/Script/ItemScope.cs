using UnityEngine;
using UnityEngine.UI;

public class ItemScope : MonoBehaviour
{

    public InventoryController inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item itemComponent = other.GetComponent<Item>();



        if (itemComponent != null && itemComponent.itemtype == ItemType.Seed)
        {
            // 이미 충돌 처리된 아이템이면 무시
            if (itemComponent.hasCollided) return;

            Debug.Log("Seed 아이템과 충돌: " + other.gameObject.name);

            // 인벤토리에 아이템 추가
            Item item = itemComponent;

            // 충돌 처리 여부 설정
            itemComponent.hasCollided = true;

            Destroy(itemComponent.gameObject);

            inventory.AddItem(item);
        }
    }




    // 디버깅을 위한 범위 시각화 (옵션)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
