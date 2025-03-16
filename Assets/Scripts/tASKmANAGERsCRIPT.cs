using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessManager : MonoBehaviour
{
    [System.Serializable]
    public class Process
    {
        public string name;
        public int score;
        public Button button;
    }

    public List<Process> processes;               // Список всех процессов
    public int correctProcessIndex = 0;           // Индекс правильного процесса
    public GameObject confirmationWindow;         // Окно подтверждения

    public Button yesButton;
    public Button noButton;

    private int selectedProcessIndex = -1;        // Выбранный процесс

    void Start()
    {
        // Привязка кнопок процессов
        for (int i = 0; i < processes.Count; i++)
        {
            int index = i; // Локальная копия для замыкания
            processes[i].button.onClick.AddListener(() => OnProcessClicked(index));
        }

        // Скрываем окно подтверждения
        confirmationWindow.SetActive(false);

        // Назначаем действия кнопок "Да" и "Нет"
        yesButton.onClick.AddListener(OnConfirmYes);
        noButton.onClick.AddListener(OnConfirmNo);
    }

    void OnProcessClicked(int index)
    {
        selectedProcessIndex = index;
        confirmationWindow.SetActive(true);
    }

    void OnConfirmYes()
    {
        if (selectedProcessIndex == correctProcessIndex)
        {
            // Верный процесс: увеличиваем очки только у него
            processes[correctProcessIndex].score += 1;
            Debug.Log($"✅ Верно! +1 очко для {processes[correctProcessIndex].name}");
        }
        else
        {
            // Неверный процесс: увеличиваем очки у всех, кроме правильного
            for (int i = 0; i < processes.Count; i++)
            {
                if (i != correctProcessIndex)
                {
                    processes[i].score += 1;
                    Debug.Log($"⚠️ Ошибка. +1 очко для {processes[i].name}");
                }
            }

            Debug.Log($"❌ Неправильный процесс: индекс {selectedProcessIndex}, имя: {processes[selectedProcessIndex].name}");
        }

        confirmationWindow.SetActive(false);
        selectedProcessIndex = -1;
    }

    void OnConfirmNo()
    {
        // Просто закрыть окно
        confirmationWindow.SetActive(false);
        selectedProcessIndex = -1;
    }
}
