using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageFlipperMail : MonoBehaviour
{
    public List<GameObject> pages;
    public List<Button> pageButtons;

    void Start()
    {
        // Скрыть все письма при запуске
        foreach (var page in pages)
        {
            page.SetActive(false);
        }

        for (int i = 0; i < pageButtons.Count; i++)
        {
            int pageIndex = i;
            pageButtons[i].onClick.AddListener(() => ShowPage(pageIndex));
        }

        // Убираем автоматическое отображение страницы
        // ShowPage(0);
    }

    public void ShowPage(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < pages.Count)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].SetActive(i == pageIndex);
            }
            Debug.Log("Page switched to: " + pageIndex);
        }
        else
        {
            Debug.Log("Invalid page index: " + pageIndex);
        }
    }
}
