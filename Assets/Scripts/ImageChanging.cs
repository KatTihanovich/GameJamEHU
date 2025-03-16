using UnityEngine;
using UnityEngine.UI;

public class ImageHoverChange : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;

    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.sprite = defaultSprite;
    }

    public void OnPointerEnter()
    {
        img.sprite = hoverSprite;
    }

    public void OnPointerExit()
    {
        img.sprite = defaultSprite;
    }
}
