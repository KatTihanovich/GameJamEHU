using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EmailManager : MonoBehaviour
{
    public Button[] emailButtons; // Кнопки перехода к письмам
    public Button[] deleteButtons; // Кнопки удаления писем
    public Button playVideoButton; // Кнопка для воспроизведения видео
    public Button jumpscareButton; // Кнопка для скримера
    public Button imageButton; // Кнопка для другой картинки
    
    public VideoPlayer videoPlayer;
    public GameObject videoPanel;
    
    public GameObject jumpscareImage;
    public GameObject otherImage;
    public GameObject restartPanel; // Окно с сообщением о рестарте
    
    private float startTime;
    private bool targetEmailDeleted = false;
    
    void Start()
    {
        startTime = Time.time;
        
        foreach (Button btn in deleteButtons)
        {
            btn.onClick.AddListener(() => DeleteEmail(btn));
        }
        
        playVideoButton.onClick.AddListener(PlayVideo);
        jumpscareButton.onClick.AddListener(ShowJumpscare);
        imageButton.onClick.AddListener(ShowOtherImage);
        
        ResetUI(); // Сбрасываем состояние интерфейса
    }
    
    void ResetUI()
    {
        // Включаем все кнопки писем
        foreach (Button btn in emailButtons)
        {
            btn.interactable = true;
            btn.gameObject.SetActive(true);
        }

        // Включаем все кнопки удаления
        foreach (Button btn in deleteButtons)
        {
            btn.interactable = true;
            btn.gameObject.SetActive(true);
        }

        // Скрываем элементы интерфейса
        videoPanel.SetActive(false);
        jumpscareImage.SetActive(false);
        otherImage.SetActive(false);
        restartPanel.SetActive(false);
    }

    void DeleteEmail(Button deleteButton)
    {
        int index = System.Array.IndexOf(deleteButtons, deleteButton);
        if (index >= 0 && index < emailButtons.Length)
        {
            emailButtons[index].interactable = false; // Отключаем кнопку перехода
            emailButtons[index].gameObject.SetActive(false); // Прячем кнопку письма
            deleteButton.interactable = false; // Отключаем кнопку удаления
            deleteButton.gameObject.SetActive(false); // Прячем кнопку удаления
            
            // Логика проверки нужного письма
            if (!targetEmailDeleted && index == 2) // Например, нужное письмо - третье
            {
                targetEmailDeleted = true;
                float timeTaken = Time.time - startTime;
                Debug.Log("Время удаления нужного письма: " + timeTaken + " секунд");
            }
            
            // Переключение на другое письмо
            SwitchToAnotherEmail(index);
        }
    }
    
    void SwitchToAnotherEmail(int deletedIndex)
    {
        for (int i = 0; i < emailButtons.Length; i++)
        {
            if (emailButtons[i].interactable)
            {
                Debug.Log("Переключение на письмо: " + i);
                return;
            }
        }
    }
    
    void PlayVideo()
    {
        videoPanel.SetActive(true);
        videoPlayer.Play();
        StartCoroutine(HideVideoAfterDelay(20f));
    }
    
    IEnumerator HideVideoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        videoPanel.SetActive(false);
    }
    
    void ShowJumpscare()
    {
        jumpscareImage.SetActive(true);
        StartCoroutine(HideJumpscareAfterDelay(5f));
    }
    
    IEnumerator HideJumpscareAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        jumpscareImage.SetActive(false);
    }
    
    void ShowOtherImage()
    {
        
        StartCoroutine(RestartLevelAfterDelay(2f));
    }
    
    IEnumerator RestartLevelAfterDelay(float delay)
    {
        otherImage.SetActive(true);
        yield return new WaitForSeconds(delay);
        otherImage.SetActive(false);
        restartPanel.SetActive(true);
        yield return new WaitForSeconds(10f); 
        ResetUI(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
