using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI textMeshProCoin;
    public GameObject gameOverUI;

    [Header("Game Settings")]
    public int coin = 0;
    public bool isGameOver = false;

    [Header("Player Reference")]
    public Player player; // 인스펙터에서 수동 연결하거나 자동 찾기

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
        else
            Debug.LogWarning("⚠️ gameOverUI가 연결되지 않았습니다.");

        if (textMeshProCoin == null)
            Debug.LogWarning("⚠️ textMeshProCoin이 연결되지 않았습니다.");

        if (player == null)
            player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f; // 시간 정지 해제
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
        }
    }

    public void ShowCoinCount()
    {
        coin++;

        if (textMeshProCoin != null)
            textMeshProCoin.SetText(coin.ToString());

        if (coin % 2 == 0 && player != null)
        {
            player.MissileUp();
        }
    }

    public void OnPlayerDead()
    {
        isGameOver = true;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }
}
