using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText = null;
    private int totalPoints = 0;
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        scoreText.text = "SCORE: " + totalPoints.ToString();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        

    }
    public void AddPoints(int points)
    {
        totalPoints += points;
        Debug.Log("My Score " + totalPoints.ToString());
        UpdateScore();
    }
    private void UpdateScore()
    {
        scoreText.text = "SCORE: " + totalPoints.ToString();
    }
}
