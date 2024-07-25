using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the UI TextMeshPro element
    public TextMeshProUGUI statusText; // Reference to the UI TextMeshPro element for game status
    public int totalCubes = 5; // Total number of cubes in the game
    public Transform player; // Reference to the player Transform
    public float fallDuration; // Time duration to consider the player has fallen off

    private int score = 0;
    private bool gameOver = false;
    private bool isGrounded = true;
    private float notGroundedTime = 0f;

    void Start()
    {
        UpdateScoreText();
        statusText.text = ""; // Clear status text at the start
    }

    void Update()
    {
        if (gameOver)
        {
            // Check for the restart input even after the game is over
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            return;
        }

        CheckGroundStatus();

        if (!isGrounded)
        {
            notGroundedTime += Time.deltaTime;
            if (notGroundedTime >= fallDuration)
            {
                EndGame("Game Over!");
            }
        }
        else
        {
            notGroundedTime = 0f; // Reset the timer if the player is grounded
        }
    }

    void CheckGroundStatus()
    {
        // Assuming the player has a CharacterController component
        CharacterController controller = player.GetComponent<CharacterController>();
        isGrounded = controller.isGrounded;
    }

    public void CollectCube()
    {
        score++;
        UpdateScoreText();

        if (score >= totalCubes)
        {
            EndGame("You Win!");
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}/{totalCubes}";
    }

    void EndGame(string message)
    {
        gameOver = true;
        statusText.text = message;
    }

    void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
