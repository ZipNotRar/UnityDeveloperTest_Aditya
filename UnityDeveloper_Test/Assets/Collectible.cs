using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player collects the cube
        {
            // Call the GameController method to update score
            GameController gameController = FindObjectOfType<GameController>();
            if (gameController != null)
            {
                gameController.CollectCube();
            }

            // Destroy the cube after collection
            Destroy(gameObject);
        }
    }
}
