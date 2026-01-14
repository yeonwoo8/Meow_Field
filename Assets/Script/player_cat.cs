using UnityEngine;

public class player_cat : MonoBehaviour
{

    private float moveX = 0f;
    private float moveY = 0f;

    // 화면 경계를 설정할 변수
    private float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        // 카메라의 뷰포트 크기를 이용하여 화면의 경계를 계산
        Camera camera = Camera.main;
        if (camera != null)
        {
            float cameraHeight = 2f * camera.orthographicSize;
            float cameraWidth = cameraHeight * camera.aspect;

            minX = -cameraWidth / 2f;
            maxX = cameraWidth / 2f;
            minY = -cameraHeight / 2f;
            maxY = cameraHeight / 2f;
        }

    }

    // Update is called once per frame
    void Update()
    {

        // Initialize movement values
        moveX = 0f;
        moveY = 0f;

        // Check for input and update movement variables
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;  // Move right
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;  // Move left
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveY += 1f;  // Move up
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveY -= 1f;  // Move down
        }

        // Apply movement
        transform.Translate(new Vector3(moveX, moveY, 0f) * Time.deltaTime * 5f);

        // Clamp the player's position to stay within the screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

    }
}