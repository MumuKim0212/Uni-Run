using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    public int itemCount = 3; // 생성할 발판의 개수
    public int enemyCount = 3; // 생성할 발판의 개수

    [Header("SpawnTime")]
    public float timeItemSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeItemSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeItemSpawn; // 다음 배치까지의 시간 간격
    public float timeEnemySpawnMin = 1.5f; // 다음 배치까지의 시간 간격 최솟값
    public float timeEnemySpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeEnemySpawn; // 다음 배치까지의 시간 간격

    [Header("SpawnPoint")]
    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    public GameObject[] items = new GameObject[3]; // 미리 생성한 발판들
    public GameObject[] enemies = new GameObject[3]; // 미리 생성한 발판들
    private int currentItemIndex = 0; // 사용할 현재 순번의 발판
    private int currentEnemyIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastItemSpawnTime; // 마지막 배치 시점
    private float lastEnemySpawnTime; // 마지막 배치 시점


    void Start()
    {
        for (int i = 0; i < itemCount; i++)
            items[i] = Instantiate(items[i], poolPosition, Quaternion.identity);
        for (int i = 0; i < enemyCount; i++)
            enemies[i] = Instantiate(enemies[i], poolPosition, Quaternion.identity);
        // 마지막 배치 시점 초기화
        lastItemSpawnTime = 0f;
        lastEnemySpawnTime = 0f;
        // 다음번 배치까지의 시간 간격을 0으로 초기화
        timeItemSpawn = timeItemSpawnMax;
        timeEnemySpawn = timeEnemySpawnMax;
    }

    void Update()
    {
        ItemSpawn();
        EnemySpawn();
    }
    void EnemySpawn()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치
        if (GameManager.instance.isGameover)
            return;

        // 마지막 배치 시점에서 timeBetSpawn이상 시간이 흘렀다면
        if (Time.time >= lastEnemySpawnTime)
        {
            // 기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastEnemySpawnTime = Time.time + timeEnemySpawn;

            // 다음 배치까지의 시간 간격을 timeBetSpawnMin, timeBetSpawnMax 사이에서 랜덤 설정
            timeEnemySpawn = Random.Range(timeEnemySpawnMin, timeEnemySpawnMax);

            // 배치할 위치의 높이를 yMin과 yMax 사이에서 랜덤 설정
            float yPos = Random.Range(yMin, yMax);

            // 사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 즉시 다시 활성화
            // 이때 발판의 Platform 컴포넌트의 OnEnable 메서드가 실행됨
            enemies[currentEnemyIndex].SetActive(false);
            enemies[currentEnemyIndex].SetActive(true);

            // 현재 순번의 발판을 화면 오른쪽에 재배치
            enemies[currentEnemyIndex].transform.position = new Vector2(xPos, yPos);

            currentEnemyIndex = (currentEnemyIndex + 1) % 2;
        }

    }
    void ItemSpawn()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치
        if (GameManager.instance.isGameover)
            return;

        // 마지막 배치 시점에서 timeBetSpawn이상 시간이 흘렀다면
        if (Time.time >= lastItemSpawnTime)
        {
            // 기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastItemSpawnTime = Time.time + timeItemSpawn;

            // 다음 배치까지의 시간 간격을 timeBetSpawnMin, timeBetSpawnMax 사이에서 랜덤 설정
            timeItemSpawn = Random.Range(timeItemSpawnMin, timeItemSpawnMax);

            // 배치할 위치의 높이를 yMin과 yMax 사이에서 랜덤 설정
            float yPos = Random.Range(yMin, yMax);

            // 사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 즉시 다시 활성화
            // 이때 발판의 Platform 컴포넌트의 OnEnable 메서드가 실행됨
            items[currentItemIndex].SetActive(false);
            items[currentItemIndex].SetActive(true);

            // 현재 순번의 발판을 화면 오른쪽에 재배치
            items[currentItemIndex].transform.position = new Vector2(xPos, yPos);
            // 순번 넘기기
            currentItemIndex++;

            // 마지막 순번에 도달했다면 순번을 리셋
            if (currentItemIndex >= itemCount)
                currentItemIndex = 0;
        }
    }
}