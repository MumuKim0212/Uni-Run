using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isGameover = false;     // 게임 오버 상태
    public Text scoreText;              // 점수를 출력할 UI 텍스트
    public Text bestScoreText;          // 점수를 출력할 UI 텍스트
    public RectTransform scoreRect;
    public GameObject gameoverUI;       // 게임 오버시 활성화 할 UI 게임 오브젝트
    public GameObject soundOn;
    public GameObject soundOff;

    private int score = 0;              // 게임 점수
    int scoreAdd = 0;
    float texttmp = 0;
    float bestScore;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (isGameover && Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (scoreAdd == 1)
            ScoreAdd();

        if (Music.instance.isSoundOn == true)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            scoreAdd = 1;
            scoreText.text = "Score : " + score;
        }
    }
    void ScoreAdd()
    {
        if (texttmp <= 2.5f)
        {
            scoreRect.position += Vector3.up;
            texttmp += 0.1f;
        }
        else if (texttmp <= 5f)
        {
            scoreRect.position += Vector3.down;
            texttmp += 0.1f;
        }
        else
        {
            texttmp = 0;
            scoreAdd = 0;
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
        isGameover = true;
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }
        bestScoreText.text = "BestScore : " + bestScore;
        gameoverUI.SetActive(true);
    }
    public void ChangeMusic()
    {
        if (!Music.instance.isSoundOn)
        {
            Music.instance.gameObject.SetActive(true);
            Music.instance.isSoundOn = true;
        }
        else
            Music.instance.isSoundOn = false;
    }

}