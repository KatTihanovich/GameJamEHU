using UnityEngine;
using UnityEngine.UI;

public class WallpaperManager : MonoBehaviour
{
    [SerializeField] private Image backgroundImage; // UI Image для обоев
    [SerializeField] private Sprite[] wallpapers; // Массив обоев

    private void Start()
    {
        UpdateWallpaper();
    }

    public void UpdateWallpaper()
    {
        if (wallpapers.Length == 0 || backgroundImage == null)
        {
            Debug.LogError("Wallpapers array is empty or backgroundImage is not assigned!");
            return;
        }

        float currentHeat = HeatBar.GetCurrentHeat();
        int index = 0;

        if (currentHeat < 2f)
            index = 0;
        else if (currentHeat >= 2f && currentHeat < 4f)
            index = 1;
        else if (currentHeat >= 4f && currentHeat < 6f)
            index = 2;
        else if (currentHeat >= 6f && currentHeat < 8f)
            index = 3;
        else if (currentHeat >= 8f)
            index = 4;

        index = Mathf.Clamp(index, 0, wallpapers.Length - 1);
        backgroundImage.sprite = wallpapers[index];
    }
}