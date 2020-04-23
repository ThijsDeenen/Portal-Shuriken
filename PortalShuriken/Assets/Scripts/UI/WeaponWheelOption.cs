using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelOption : MonoBehaviour
{
    public Sprite[] sprites;
    private bool enabled = false;
    Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        if (enabled)
        {
            image.sprite = sprites[0];
        }
        else image.sprite = sprites[1];
    }

    public void Enable()
    {
        enabled = true;
        image.sprite = sprites[0];
    }

    public void Disable()
    {
        enabled = false;
        image.sprite = sprites[1];
    }
}
