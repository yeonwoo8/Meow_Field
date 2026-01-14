using UnityEngine;

public class player_cat2 : MonoBehaviour
{
    public Camera mainCamera; // 사용자가 설정할 수 있는 카메라

    private float moveX = 0f;
    private float moveY = 0f;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // 기본적으로 Main Camera를 사용
        }

        if (mainCamera != null)
        {
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            minX = -cameraWidth / 2f;
            maxX = cameraWidth / 2f;
            minY = -cameraHeight / 2f;
            maxY = cameraHeight / 2f;
        }
    }

    void Update()
    {
        moveX = 0f;
        moveY = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveY += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY -= 1f;
        }

        transform.Translate(new Vector3(moveX, moveY, 0f) * Time.deltaTime * 5f);


    }
}
