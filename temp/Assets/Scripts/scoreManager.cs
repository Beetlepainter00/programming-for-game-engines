using UnityEngine;
using TMPro; // Required namespace to control TextMeshPro components

public class scoreManager : MonoBehaviour
{
    public static scoreManager Instance { get; private set; }

    [Header("UI Reference")]
    [Tooltip("Drag your ScoreText UI object into this slot in the Inspector")]
    public TextMeshProUGUI scoreText;

    [Header("Score Values")]
    public int currentScore = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Set initial text display when the game starts
        UpdateScoreUI();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    // Handles changing the visual text on screen
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }
}