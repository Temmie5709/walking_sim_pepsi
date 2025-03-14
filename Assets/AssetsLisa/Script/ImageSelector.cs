using UnityEngine;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{
    public Image targetImage;  // L'Image à changer
    public Sprite[] sprites;  


    public void UpdateImage(int index)
    {
        if (index >= 0 && index < sprites.Length)
            targetImage.sprite = sprites[index];
    }
}
