using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public GameObject red_seedPrefab;    // 빨간 씨앗 프리팹
    public GameObject purple_seedPrefab; // 보라색 씨앗 프리팹
    public GameObject green_seedPrefab;  // 초록색 씨앗 프리팹

    public float spawnInterval = 10f;    // 떨어지는 간격 (10초)
    public float spawnRangeX = 8f;       // X축에서 랜덤 위치 범위
    public float spawnHeight = 8f;       // 씨앗이 떨어질 높이

    private Camera mainCamera;           // 카메라 참조

    private void Start()
    {
        mainCamera = Camera.main; // 카메라 참조 얻기
        InvokeRepeating("SpawnSeed", 0f, spawnInterval);  // 10초마다 씨앗을 떨어뜨리도록 반복 호출
    }

    // 랜덤으로 씨앗을 떨어뜨리는 함수
    void SpawnSeed()
    {
        // 랜덤한 X 위치 생성 (카메라의 뷰포트 내에서만 생성)
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);  // 떨어질 위치 설정

        // 씨앗이 화면 밖으로 나가지 않도록 범위 제한
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spawnPosition);
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.1f, 0.9f); // X 위치 범위 제한
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.1f, 0.9f); // Y 위치 범위 제한
        spawnPosition = mainCamera.ViewportToWorldPoint(viewportPosition);  // 월드 좌표로 변환

        // 랜덤하게 씨앗 선택
        int randomSeed = Random.Range(0, 3); // 0, 1, 2 중 하나의 값
        GameObject seedToSpawn = null;

        switch (randomSeed)
        {
            case 0:
                seedToSpawn = red_seedPrefab;
                break;
            case 1:
                seedToSpawn = purple_seedPrefab;
                break;
            case 2:
                seedToSpawn = green_seedPrefab;
                break;
        }

        // 선택된 씨앗 프리팹 인스턴스화
        if (seedToSpawn != null)
        {
            GameObject spawnedSeed = Instantiate(seedToSpawn, spawnPosition, Quaternion.identity);
            Item item = spawnedSeed.AddComponent<Item>();  // Item 컴포넌트를 추가
            item.itemtype = ItemType.Seed;  // ItemType을 Seed로 설정
            spawnedSeed.AddComponent<BoxCollider2D>().isTrigger = true; // Collider 추가 및 Is Trigger 활성화
        }
    }
}
