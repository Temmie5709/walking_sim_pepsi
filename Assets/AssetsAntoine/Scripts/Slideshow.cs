using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Slideshow : MonoBehaviour
{
    [SerializeField] private Image displayImage;
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    [SerializeField] private List<float> displayTimes = new List<float>();
    [SerializeField] private float fadeDuration = 0.5f;

    [SerializeField] private Vector2 minSize = new Vector2(200, 200); // Taille min
    [SerializeField] private Vector2 maxSize = new Vector2(800, 600); // Taille max

    private int currentIndex = 0;
    private Coroutine slideshowCoroutine;


    void Start()
    {
        if (images.Count != displayTimes.Count)
        {
            Debug.LogError("Le nombre d'images et de temps d'affichage doit être identique !");
            return;
        }
        slideshowCoroutine = StartCoroutine(PlaySlideshow());
    }

    IEnumerator PlaySlideshow()
    {
        while (currentIndex<images.Count)
        {
            displayImage.sprite = images[currentIndex];

            // Redimensionner l'image pour éviter l'étirement
            ResizeImage();

            yield return StartCoroutine(FadeImage(0f, 1f, fadeDuration));
            yield return new WaitForSeconds(displayTimes[currentIndex]);
            yield return StartCoroutine(FadeImage(1f, 0f, fadeDuration));

            currentIndex++;
        }
        gameObject.SetActive(false);
    }

    void ResizeImage()
    {
        if (displayImage.sprite == null) return;

        // Récupérer la taille originale du sprite
        float originalWidth = displayImage.sprite.texture.width;
        float originalHeight = displayImage.sprite.texture.height;
        float aspectRatio = originalWidth / originalHeight;

        // Taille cible dans les limites définies
        float targetWidth = Mathf.Clamp(originalWidth, minSize.x, maxSize.x);
        float targetHeight = targetWidth / aspectRatio;

        if (targetHeight > maxSize.y)
        {
            targetHeight = maxSize.y;
            targetWidth = targetHeight * aspectRatio;
        }

        // Appliquer la nouvelle taille sans déformer
        displayImage.rectTransform.sizeDelta = new Vector2(targetWidth, targetHeight);

        // Ajuster pour ne pas dépasser la taille du parent (optionnel)
        RectTransform parentRect = displayImage.rectTransform.parent as RectTransform;
        if (parentRect != null)
        {
            float maxParentWidth = parentRect.rect.width;
            float maxParentHeight = parentRect.rect.height;

            if (targetWidth > maxParentWidth || targetHeight > maxParentHeight)
            {
                float scaleFactor = Mathf.Min(maxParentWidth / targetWidth, maxParentHeight / targetHeight);
                displayImage.rectTransform.sizeDelta = new Vector2(targetWidth * scaleFactor, targetHeight * scaleFactor);
            }
        }
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = displayImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = alpha;
            displayImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        displayImage.color = color;
    }
}
