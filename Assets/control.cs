using UnityEngine;
using UnityEngine.UI; // For UI elements
using System.Collections; // For using coroutines

public class Control : MonoBehaviour
{
    public GameObject warningImage; // Panel for warning message (optional)
    public GameObject restartPanel; // Panel for restart message (shown on collision)
    public AudioClip warningSound; // Sound for warning
    public float cursorRadius = 0.1f; // Radius for collision detection
    private bool isShowingRestartPanel = false; // To prevent showing multiple restart panels

    private Vector3 startPosition; // To store the starting position of the cursor

    void Start()
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

        // Store the starting position for the cursor
        startPosition = transform.position;

        // Start showing periodic warning after random intervals
        StartCoroutine(ShowWarningPeriodically());
    }

    void Update()
    {
        // Check for collision with walls (use Physics2D.OverlapCircle if you're in 2D)
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;

        // Convert mouse position to world position
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0; // Keep cursor in 2D space

        // Check for collision with walls
        if (!Physics.CheckSphere(targetPosition, cursorRadius))
        {
            // No collision – move the cursor
            transform.position = targetPosition;
        }
        else
        {
            // Collision detected – show restart panel if not already showing
            if (!isShowingRestartPanel)
            {
                ShowRestartPanel();
            }
        }
    }

    // Coroutine to show a warning periodically after a random time (4-10 seconds)
    IEnumerator ShowWarningPeriodically()
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

    // Show the restart panel when a collision occurs
    void ShowRestartPanel()
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
        transform.position = startPosition; // Reset cursor position
        if (restartPanel != null)
        {
            restartPanel.SetActive(false); // Hide the restart panel
        }
        isShowingRestartPanel = false;

        // Optionally reset the game state or reload the scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Uncomment if you want to reload the scene
    }
}
