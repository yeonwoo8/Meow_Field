using UnityEngine;

public class Seed : MonoBehaviour
{
    private Camera mainCamera;  // 카메라 참조

    private void Start()
    {
        mainCamera = Camera.main;  // 카메라 참조 얻기
    }

    private void Update()
    {

        // 씨앗의 위치가 카메라 화면 내에서만 유지되도록 제한
        KeepSeedInBounds();
    }


    // 씨앗이 카메라 화면 내에서만 이동하도록 제한하는 함수
    private void KeepSeedInBounds()
    {
        // 씨앗의 월드 좌표를 뷰포트 좌표로 변환
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // X와 Y 좌표가 카메라 화면을 벗어나지 않도록 제한
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f); // 화면의 좌우 범위 제한
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f); // 화면의 상하 범위 제한

        // 제한된 뷰포트 좌표를 월드 좌표로 다시 변환
        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
    }
}
