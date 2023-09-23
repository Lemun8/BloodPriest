using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manager;
    public GameObject deathScreen;
    public GameObject winScreen;
    public int score;
    public TextMeshProUGUI scoreText;
    public HealthController hc;
    [SerializeField] float lifeDrain;

    private void Awake()
    {
        Time.timeScale = 1;
        manager = this;
    }

    public void GameOver()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0;
    }

    void Update()
    {
        if (score == 100)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString() + "/100";
        hc.AddHealth(lifeDrain);
    }
}
