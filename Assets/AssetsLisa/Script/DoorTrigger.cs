using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public Image fadeImage; // Image blanche pour la transition
    public TextMeshProUGUI messageText; // Texte au centre
    public GameObject player; // Référence au joueur
    public MonoBehaviour playerMovementScript; // Script de mouvement du joueur à désactiver
    public float fadeDuration = 2f;
    public float quitDelay = 10f;
    public Image X;
    private bool isTriggered = false;

    void Start()
    {
        // Assure-toi que l'image et le texte sont invisibles au début
        if (fadeImage != null) fadeImage.color = new Color(1, 1, 1, 0);
        if (messageText != null) messageText.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false; // Désactive le mouvement du joueur
            }
            StartCoroutine(FadeAndQuit());
            X.enabled = false;
        }
    }

    IEnumerator FadeAndQuit()
    {
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;
        Color textColor = messageText.color;

        // Boucle de fade
        while (elapsedTime < fadeDuration)
        {
            float alpha = elapsedTime / fadeDuration;

            // On ajuste l'alpha de l'image de fondu
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            // On ajuste l'alpha du texte et on fait en sorte que le texte devienne de plus en plus noir
            messageText.color = new Color(0f, 0f, 0f, alpha); // Le texte devient de plus en plus noir avec un alpha qui augmente
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assure-toi que l'image et le texte sont complètement visibles
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
        messageText.color = new Color(0f, 0f, 0f, 1); // Le texte est complètement noir et visible

        yield return new WaitForSeconds(quitDelay);

        // Quitte le jeu (ne fonctionne pas dans l'éditeur Unity)
        Application.Quit();
    }


}