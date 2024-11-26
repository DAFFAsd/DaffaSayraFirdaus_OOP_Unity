using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Tambahkan ini

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 0;
    public int totalEnemies = 0;
    public int points = 0;
    public Player playership;

    private TextMeshProUGUI waveText;
    private TextMeshProUGUI enemiesText;
    private TextMeshProUGUI pointsText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI titleText;
    private TextMeshProUGUI countdownText;


    private void Start()
    {
        waveNumber = 0;
        playership = GameObject.FindObjectOfType<Player>();
        if (GameManager.Instance != null &&
            GameManager.Instance.LevelManager != null)
        {
            Canvas levelCanvas = GameManager.Instance.LevelManager.GetComponentInChildren<Canvas>();

            if (levelCanvas != null)
            {
                waveText = levelCanvas.transform.Find("Wave")?.GetComponent<TextMeshProUGUI>();
                enemiesText = levelCanvas.transform.Find("Enemies")?.GetComponent<TextMeshProUGUI>();
                pointsText = levelCanvas.transform.Find("Points")?.GetComponent<TextMeshProUGUI>();
                healthText = levelCanvas.transform.Find("Health")?.GetComponent<TextMeshProUGUI>();
                titleText = levelCanvas.transform.Find("Title")?.GetComponent<TextMeshProUGUI>();
                countdownText = levelCanvas.transform.Find("Countdown")?.GetComponent<TextMeshProUGUI>();
            }
        }
        UpdateUI();
        Destroy(titleText.gameObject);
    }

    private void Update()
    {
        if (totalEnemies <= 0 || waveNumber == 0)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                StartNewWave();
            }
        }
        UpdateUI();
    }

    private void StartNewWave()
    {
        timer = 0;
        waveNumber++;
        totalEnemies = 0;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.StartSpawning();
        }
    }

    public void OnEnemyKilled()
    {
        totalEnemies--;
    }

    private void UpdateUI()
    {
        if (waveText != null)
            waveText.text = "Wave: " + waveNumber;

        if (enemiesText != null)
            enemiesText.text = "Enemies Left: " + totalEnemies;

        if (pointsText != null)
            pointsText.text = "Points: " + points;

        if (countdownText != null)
        {
            if (timer != 0)
                countdownText.text = "Next Wave Starts In:\n" + (5-timer);
            else
                countdownText.text = null;
        }

        if (healthText != null)
            healthText.text = "Health: " + playership.GetComponent<HealthComponent>().Health;
    }
}