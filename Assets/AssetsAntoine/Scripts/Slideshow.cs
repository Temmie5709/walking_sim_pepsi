using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Slideshow : MonoBehaviour
{
    [SerializeField] private Image displayImage;
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private KeyCode nextImageKey = KeyCode.Space; // Touche configurable depuis l'inspecteur

    [SerializeField] private Vector2 minSize = new Vector2(200, 200); // Taille min
    [SerializeField] private Vector2 maxSize = new Vector2(800, 600); // Taille max

    private int currentIndex = 0;

    void OnEnable()
    {
        RestartSlideshow();
    }

    void Update()
    {
        if (Input.GetKeyDown(nextImageKey)) // Change l'image avec la touche configurable
        {
            NextImage();
        }
    }

    void RestartSlideshow()
    {
        if (images.Count == 0)
        {
            Debug.LogError("Aucune image dans le diaporama !");
            return;
        }

        currentIndex = 0;
        displayImage.sprite = images[currentIndex];
        ResizeImage();
        StartCoroutine(FadeImage(0f, 1f, fadeDuration));
    }

    void NextImage()
    {
        StartCoroutine(FadeImage(1f, 0f, fadeDuration, () =>
        {
            currentIndex++;
            if (currentIndex >= images.Count)
            {
                gameObject.SetActive(false);
                return;
            }

            displayImage.sprite = images[currentIndex];
            ResizeImage();
            StartCoroutine(FadeImage(0f, 1f, fadeDuration));
        }));
    }

    void ResizeImage()
    {
        if (displayImage.sprite == null) return;

        float originalWidth = displayImage.sprite.texture.width;
        float originalHeight = displayImage.sprite.texture.height;
        float aspectRatio = originalWidth / originalHeight;

        float targetWidth = Mathf.Clamp(originalWidth, minSize.x, maxSize.x);
        float targetHeight = targetWidth / aspectRatio;

        if (targetHeight > maxSize.y)
        {
            targetHeight = maxSize.y;
            targetWidth = targetHeight * aspectRatio;
        }

        displayImage.rectTransform.sizeDelta = new Vector2(targetWidth, targetHeight);
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha, float duration, System.Action onComplete = null)
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

        onComplete?.Invoke();
    }
}