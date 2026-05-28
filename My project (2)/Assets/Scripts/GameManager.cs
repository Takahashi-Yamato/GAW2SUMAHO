using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("スコア")]
    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;

    public GameObject gameOverText;

    [Header("敵生成")]
    public GameObject enemyPrefab;

    public float spawnX = 7f;
    public float spawnY = 4f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();

        gameOverText.SetActive(false);

        SpawnEnemy();
    }

    // =========================
    // スコア加算
    // =========================

    public void AddScore(int amount)
    {
        score += amount;

        UpdateUI();
    }

    // =========================
    // HP更新
    // =========================

    public void UpdateHP(int hp)
    {
        hpText.text = "HP : " + hp;

        if (hp <= 0)
        {
            gameOverText.SetActive(true);
        }
    }

    // =========================
    // UI更新
    // =========================

    void UpdateUI()
    {
        scoreText.text = "Score : " + score;
    }

    // =========================
    // 敵生成
    // =========================

    public void SpawnEnemy()
    {
        float randomX =
            Random.Range(-spawnX, spawnX);

        float randomY =
            Random.Range(-spawnY, spawnY);

        Vector2 spawnPos =
            new Vector2(randomX, randomY);

        Instantiate(enemyPrefab, spawnPos,
            Quaternion.identity);
    }
}