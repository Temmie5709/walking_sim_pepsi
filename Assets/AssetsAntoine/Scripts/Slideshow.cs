using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Slideshow : MonoBehaviour
{
    [SerializeField] private Image displayImage;
    [SerializeField] private List<Sprite> images = new List<Sprite>() ;
    [SerializeField] private List<float> displayTimes = new List<float>() ;
    [SerializeField] private float fadeDuration = 0.5f; // Temps du fondu

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
        while (true)
        {
            // Applique l'image suivante
            displayImage.sprite = images[currentIndex];

            // Démarre le fondu d'apparition
            yield return StartCoroutine(FadeImage(0f, 1f, fadeDuration));

            // Attends le temps d'affichage défini pour l'image
            yield return new WaitForSeconds(displayTimes[currentIndex]);

            // Démarre le fondu de disparition
            yield return StartCoroutine(FadeImage(1f, 0f, fadeDuration));

            // Passe à l'image suivante
            currentIndex = (currentIndex + 1) % images.Count;
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
