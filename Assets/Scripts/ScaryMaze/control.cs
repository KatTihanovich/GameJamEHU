using UnityEngine;
using UnityEngine.UI; // For UI elements
using System.Collections; // For using coroutines

public class control : MonoBehaviour
{
    public GameObject warningImage; // Panel for warning message (optional)
    public GameObject restartPanel; // Panel for restart message (shown on collision)
    public AudioClip warningSound; // Sound for warning
    public float cursorRadius = 0.1f; // Radius for collision detection
    private bool isShowingRestartPanel = false; // To prevent showing multiple restart panels
    public GameObject anim;

    private void Start()
    {
        // Hide system cursor
        Cursor.visible = false;

        // Hide both panels initially
        if (warningImage != null)
        {
            warningImage.SetActive(false);
        }

        if (restartPanel != null)
        {
            restartPanel.SetActive(false);
        }

        // Start showing periodic warning after random intervals
        StartCoroutine(ShowWarningPeriodically());
    }

    private void Update()
    {
        // Check for collision with walls (using box collider)
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;

        // Convert mouse position to world position
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0; // Keep cursor in 2D space

        // Move the cursor
        transform.position = targetPosition;
    }

    // Detect when the cursor exits the collider zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger exited with: ");
        // When the cursor leaves the collider zone, show the restart panel
        if (!isShowingRestartPanel)
        {
            ShowRestartPanel();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("finish") && !isShowingRestartPanel)
        {
            ShowRestartPanel();
            anim.SetActive(true);
        }
    }

    // Coroutine to show a warning periodically after a random time (4-10 seconds)
    private IEnumerator ShowWarningPeriodically()
    {
        while (true)
        {
            // Wait for a random time between 4 and 10 seconds
            float randomTime = Random.Range(4f, 10f);
            yield return new WaitForSeconds(randomTime);

            // Show the warning image (if assigned)
            if (warningImage != null)
            {
                warningImage.SetActive(true);
            }

            // Play the warning sound (if assigned)
            if (warningSound != null)
            {
                AudioSource.PlayClipAtPoint(warningSound, Camera.main.transform.position);
            }

            // Wait for 1 second before hiding the warning
            yield return new WaitForSeconds(1f);

            // Hide the warning image after the duration
            if (warningImage != null)
            {
                warningImage.SetActive(false);
            }
        }
    }

    // Show the restart panel when a collision occurs or when the cursor leaves the collider zone
    private void ShowRestartPanel()
    {
        isShowingRestartPanel = true;

        // Show the restart panel
        if (restartPanel != null)
        {
            restartPanel.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0f;

        // Optional: You could add a restart button in the restart panel for the player to restart manually
    }

    // Method to restart the game (you can call this on a restart button click)
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        if (restartPanel != null)
        {
            restartPanel.SetActive(false); // Hide the restart panel
        }
        isShowingRestartPanel = false;

        // Optionally reset the game state or reload the scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Uncomment if you want to reload the scene
    }
}
