using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage; // L'image qui sera utilisée pour le fondu
    public float fadeDuration = 2f; // La durée du fondu
    public string sceneToLoad = "Jour 2"; // Le nom de la scène à charger

    public GameObject task;  // L'objet "task" à désactiver
    public GameObject interact;  // L'objet "interact" à désactiver

    void Start()
    {
        // S'assurer que l'image de fondu est complètement transparente au départ
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public void StartTransition()
    {
        // Désactiver les objets "task" et "interact"
        if (task != null) task.SetActive(false);
        if (interact != null) interact.SetActive(false);

        // Commencer la coroutine pour le fondu et le changement de scène
        StartCoroutine(FadeOutAndLoadScene());
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        // Fondu au noir (augmentation de l'alpha à 1)
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(elapsedTime / fadeDuration));
            yield return null;
        }

        // Charger la nouvelle scène après le fondu
        SceneManager.LoadScene(sceneToLoad);
    }
}
