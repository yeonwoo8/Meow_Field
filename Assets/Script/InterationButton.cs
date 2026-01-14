using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    public Button interactionButton;  // 버튼 UI 연결

    private void Start()
    {
        interactionButton.gameObject.SetActive(false);  // 버튼 초기 비활성화
        interactionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 2D 트리거 감지
        {
            Debug.Log("Player entered trigger zone");  // 디버그 메시지 추가
            interactionButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 2D 트리거 범위 이탈 감지
        {
            Debug.Log("Player exited trigger zone");  // 디버그 메시지 추가
            interactionButton.gameObject.SetActive(false);
        }
    }

    private void OnButtonClick()
    {
        Debug.Log("Button clicked!");  // 버튼 클릭 확인
    }
}
