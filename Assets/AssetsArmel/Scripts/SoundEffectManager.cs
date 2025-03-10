using System.Collections;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public CameraZoom cameraZoomScript;     // Référence vers le script CameraZoom

    public AudioSource BoutonExitSoundEffect;   // Source audio pour le son des boutons
    public AudioSource ExitSoundEffect;     // Source audio pour le son de sortie
    public AudioSource StartSoundEffect;    // Source audio pour le son de début
    public AudioSource BoutonSoundEffect;
    public AudioSource KeySoundEffect;

    private bool canZoom;

    private void Start()
    {
        canZoom = false;
    }

    // Fonction pour jouer le son d'un bouton
    public void PlayBoutonSoundEffect()
    {
        if (BoutonSoundEffect != null)
        {
            BoutonSoundEffect.Play();
        }
        else
        {
            Debug.Log("Aucun son de Bouton Menu");
        }
    }

    // Fonction pour jouer le son d'un bouton Exit
    public void PlayBoutonExitSoundEffect()
    {
        if (BoutonExitSoundEffect != null)
        {
            BoutonExitSoundEffect.Play();
        }
        else
        {
            Debug.Log("Aucun son de Fermer/Retour Menu");
        }
    }

    // Fonction pour jouer le son de sortie et attendre sa fin avant de quitter
    public void PlayExitSoundEffect()
    {
        if (ExitSoundEffect != null)
        {
            StartCoroutine(QuitAfterSound());
        }
        else
        {
            Debug.Log("Aucun son d'Exit");
        }
    }

    // Coroutine pour attendre la fin du son avant de quitter
    private IEnumerator QuitAfterSound()
    {
        ExitSoundEffect.Play();

        // Attendre jusqu'à ce que le son ait fini de jouer
        while (ExitSoundEffect.isPlaying)
        {
            yield return null;  // Attendre la fin de la frame actuelle
        }

        // Une fois le son terminé, quitter l'application
        Application.Quit();
    }

    // Fonction pour jouer le son de début et déclencher le zoom après
    public void PlayStartSoundEffect()
    {
        if (StartSoundEffect != null)
        {
            StartCoroutine(WaitForStartSoundToEnd());
        }
        else
        {
            Debug.Log("Aucun son pour Start");
        }
    }

    public void PlayKeySoundEffect()
    {
        if (KeySoundEffect != null)
        {
            KeySoundEffect.Play();
        }
        else
        {
            Debug.Log("Aucun son de Changement de touches");
        }
    }

    // Coroutine pour attendre la fin du son de début avant de déclencher le zoom
    private IEnumerator WaitForStartSoundToEnd()
    {
        StartSoundEffect.Play();

        // Attendre que le son de début soit terminé
        while (StartSoundEffect.isPlaying)
        {
            yield return null;  // Attendre la fin de la frame actuelle
        }

        canZoom = true;

        // Une fois le son terminé, démarrer le zoom via le script CameraZoom
        if (canZoom == true)
        {
            cameraZoomScript.StartZoom();
        }
        else
        {
            Debug.LogError("canZoom = false");
        }
    }
}