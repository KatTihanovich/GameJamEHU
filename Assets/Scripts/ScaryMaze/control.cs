using UnityEngine;
using UnityEngine.UI; // For UI elements
using System.Collections; // For using coroutines

public class Control : MonoBehaviour
{
    public GameObject warningImage; // Panel for warning message
    public GameObject restartPanel; // Panel for restart message
    public GameObject finishObject; // The object that triggers the finish animation
    public AudioClip warningSound; // Sound for warning
    public float cursorRadius = 0.1f; // Radius for collision detection

    public Animator finishAnimator; // Animator for the finish animation

    private bool isShowingRestartPanel = false; // Prevent duplicate restart panels
    private bool isFinishTriggered = false; // Ensure finish animation plays once
    private Vector3 startPosition; // Store the starting position of the cursor

    void Start()
    {
        // Hide system cursor
        Cursor.visible = false;

        // Hide both panels initially
        if (warningImage != null) warningImage.SetActive(false);
        if (restartPanel != null) restartPanel.SetActive(false);

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

        // Move the cursor if no collision
        if (!Physics.CheckSphere(targetPosition, cursorRadius))
        {
            transform.position = targetPosition;
        }
        else if (!isShowingRestartPanel) // If collision with walls, show restart panel
        {
            ShowRestartPanel();
        }

        // Check collision with the finish object
        if (finishObject != null && !isFinishTriggered)
        {
            float distanceToFinish = Vector3.Distance(transform.position, finishObject.transform.position);
            if (distanceToFinish < cursorRadius)
            {
                TriggerFinishAnimation();
            }
        }
    }

    // Coroutine to show a warning periodically after a random time (4-10 seconds)
    IEnumerator ShowWarningPeriodically()
    {
        while (true)
        {
            float randomTime = Random.Range(4f, 10f);
            yield return new WaitForSeconds(randomTime);

            if (warningImage != null) warningImage.SetActive(true);

            if (warningSound != null)
            {
                AudioSource.PlayClipAtPoint(warningSound, Camera.main.transform.position);
            }

            yield return new WaitForSeconds(1f);

            if (warningImage != null) warningImage.SetActive(false);
        }
    }

    // Show the restart panel when a collision occurs
    void ShowRestartPanel()
    {
        isShowingRestartPanel = true;

        if (restartPanel != null) restartPanel.SetActive(true);

        Time.timeScale = 0f; // Pause the game
    }

    // Trigger the finish animation when cursor collides with the finish object
    void TriggerFinishAnimation()
    {
        isFinishTriggered = true;

        if (finishAnimator != null)
        {
            finishAnimator.SetTrigger("PlayFinish");
        }

        // Optionally, you could delay further actions until the animation finishes
        StartCoroutine(EndGameAfterAnimation());
    }

    // Wait for the finish animation to complete before taking further actions
    IEnumerator EndGameAfterAnimation()
    {
        yield return new WaitForSeconds(finishAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Example: Display a win screen or transition
        Debug.Log("Finish Animation Complete!");
    }

    // Method to restart the game (called by a UI button)
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        transform.position = startPosition; // Reset cursor position

        if (restartPanel != null) restartPanel.SetActive(false);
        isShowingRestartPanel = false;
        isFinishTriggered = false;

        // Optionally reset the game state or reload the scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
