using UnityEngine;
using UnityEngine.UI;

public class ImageClickChange : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;

    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.sprite = defaultSprite;
    }

    public void ToggleImage()
    {
        if (img.sprite == defaultSprite)
        {
            img.sprite = hoverSprite;
        } else 

        if (img.sprite == hoverSprite)
        {
            img.sprite = defaultSprite;
        }
    }
}
