using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the UI TextMeshPro element
    public float timeLimit; // 2 minutes in seconds

    private float timeRemaining;

    void Start()
    {
        timeRemaining = timeLimit; // Initialize the timer
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // Decrease time remaining
            UpdateTimerDisplay();
        }
        else
        {
            timeRemaining = 0;
            //Debug.Log("Timer Stops now");
            UpdateTimerDisplay();
            // Add any additional logic for when the timer reaches 0 (e.g., end the game)
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
