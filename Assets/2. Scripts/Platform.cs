using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; // 장애물 오브젝트들

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable()
    {
        // 장애물 수만큼 루프
        for (int i = 0; i < obstacles.Length; i++)
        {
            // 현재 순번의 장애물을 1/3의 확률로 활성화
            if (Random.Range(0, 3) == 0)
                obstacles[i].SetActive(true);
            else
                obstacles[i].SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            GameManager.instance.AddScore(1);
        this.gameObject.SetActive(false);
    }
}