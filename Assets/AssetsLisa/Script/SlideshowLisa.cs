using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SlideshowLisa : MonoBehaviour
{
    [SerializeField] private Image displayImage;
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float slideDuration = 2f; // Temps entre chaque image
    [SerializeField] private AudioSource sonJeu;
    [SerializeField] private AudioSource sonFlash;

    private int currentIndex = 0;

    void Start()
    {
        // Rendre l'image invisible au départ
        Color color = displayImage.color;
        color.a = 0f;
        displayImage.color = color;
        displayImage.gameObject.SetActive(false);
    }

    public void Slideshow()
    {
        if (images.Count == 0)
        {
            Debug.LogError("Aucune image dans le diaporama !");
            return;
        }

        // Arrêter SonJeu et démarrer SonFlash
        if (sonJeu.isPlaying) sonJeu.Pause();
        if (!sonFlash.isPlaying) sonFlash.Play();

        displayImage.gameObject.SetActive(true);
        currentIndex = 0;
        displayImage.sprite = images[currentIndex];
        ResizeImageToFullScreen();
        StartCoroutine(FadeImage(0f, 1f, fadeDuration, () => StartCoroutine(AutoNextImage())));
    }

    IEnumerator AutoNextImage()
    {
        while (currentIndex < images.Count - 1)
        {
            yield return new WaitForSeconds(slideDuration);
            NextImage();
        }

        yield return new WaitForSeconds(slideDuration);
        StartCoroutine(FadeImage(1f, 0f, fadeDuration, () =>
        {
            displayImage.gameObject.SetActive(false);
            // Reprendre SonJeu et arrêter SonFlash
            if (!sonJeu.isPlaying) sonJeu.Play();
            if (sonFlash.isPlaying) sonFlash.Stop();
        }));
    }

    void NextImage()
    {
        StartCoroutine(FadeImage(1f, 0f, fadeDuration, () =>
        {
            currentIndex++;
            if (currentIndex >= images.Count)
                return;

            displayImage.sprite = images[currentIndex];
            ResizeImageToFullScreen();
            StartCoroutine(FadeImage(0f, 1f, fadeDuration));
        }));
    }

    void ResizeImageToFullScreen()
    {
        if (displayImage.sprite == null) return;

        // Prendre toute la taille du HUD
        displayImage.rectTransform.anchorMin = Vector2.zero;
        displayImage.rectTransform.anchorMax = Vector2.one;
        displayImage.rectTransform.offsetMin = Vector2.zero;
        displayImage.rectTransform.offsetMax = Vector2.zero;
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
