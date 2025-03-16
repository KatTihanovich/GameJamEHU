using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels; // 0 - первый, 1 - второй, 2 - третий
    private GameObject activePanel = null;

    // Метод вызывается при нажатии на иконку
    public void OpenPanel(int index)
    {
        CloseAllPanels(); // Сначала закроем все панели

        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(true);
            activePanel = panels[index];
        }
    }

    // Метод вызывается на кнопке крестика
    public void CloseActivePanel()
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false);
            activePanel = null;
        }
    }

    // Метод для закрытия всех панелей (опционально)
    private void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        activePanel = null;
    }
}