using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public int gridWidth = 20;
    public int gridHeight = 20;
    public TextMeshProUGUI scoreText;
    public  TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public GameObject foodParticlePrefab;
    public AudioClip foodSound;
    private int score = 0;
    private float timeRemaining = 60f;
    private bool timerIsRunning = true;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SpawnFood();
        UpdateScore(0);
        gameOverPanel.SetActive(false);
    }

    private static void UpdateScore()
    {
        UpdateScore();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining>0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimer(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameOVer();
            }
        }
    }

    public  void GameOVer()
    {
     gameOverPanel.SetActive(true);
        Time.timeScale=0f;
    }
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes=Mathf.FloorToInt(currentTime/60);
        float seconds=Mathf.FloorToInt(currentTime%60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void  SpawnFood()
    {
        int x = Random.Range(-gridWidth / 2, gridWidth / 2) * 1;
        int y = Random.Range(-gridHeight / 2, gridHeight / 2) * 1;
        Instantiate(foodPrefab,new Vector2(x,y),Quaternion.identity);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void PlayFoodEffect(Vector2 position)
    {
        if (foodParticlePrefab!=null)
        {
            Instantiate(foodParticlePrefab, position, Quaternion.identity);
        }
        if (foodSound != null)
        {
            audioSource.PlayOneShot(foodSound);
        }
        else
        {
            Debug.Log("foodSound is not assigned in the GameManager.");
        }
    }
    
}
