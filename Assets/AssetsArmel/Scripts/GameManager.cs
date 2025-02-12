using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject settingsPanel;  // Panneau des paramètres
    public Slider volumeSlider;       // Slider pour le volume
    public Dropdown resolutionDropdown; // Dropdown pour choisir la résolution
    public Toggle fullscreenToggle;   // Toggle pour le mode plein écran
    public AudioSource musiqueDeFond;   // Source audio pour gérer le volume

    public GameObject menuPanel;
    public Text gameTitleText;        // Texte du nom du jeu

    private string originalGameTitle = "Nom du Jeu"; // Remplacer par le nom de votre jeu
    private Resolution[] resolutions; // Liste des résolutions disponibles

    void Start()
    {
        menuPanel.SetActive(true);
        
        // Initialiser les résolutions disponibles dans le dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Lier le dropdown à la fonction de changement de résolution
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // Désactiver le panneau des paramètres au démarrage
        settingsPanel.SetActive(false);

        // Assigner le volume par défaut au Slider et le connecter
        volumeSlider.value = musiqueDeFond.volume; // Assigner la valeur actuelle de l'AudioSource
        volumeSlider.onValueChanged.AddListener(SetVolume); // Lier le slider à la fonction SetVolume

        // Lier le toggle de plein écran
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    // Fonction appelée par le bouton "Paramètres"
    public void OpenSettings()
    {
        // Ouvrir le panneau des paramètres
        settingsPanel.SetActive(true);
        
        // Cacher les boutons du menu principal
        menuPanel.SetActive(false);

        // Changer le texte du titre en "Paramètres"
        gameTitleText.text = "Paramètres";
    }

    // Fonction pour fermer le panneau des paramètres
    public void CloseSettings()
    {
        // Fermer le panneau des paramètres
        settingsPanel.SetActive(false);

        // Réafficher les boutons du menu principal
        menuPanel.SetActive(true);

        // Réinitialiser le texte du titre au nom original du jeu
        gameTitleText.text = originalGameTitle;
    }

    // Fonction appelée pour changer le volume à partir du slider
    public void SetVolume(float volume)
    {
        if (musiqueDeFond != null)
        {
            musiqueDeFond.volume = Mathf.Clamp(volume, 0f, 1f); // Clamp pour s'assurer que le volume est entre 0 et 1
        }
        else
        {
            Debug.LogWarning("Aucune source audio assignée à 'musiqueDeFond'.");
        }
    }

    // Fonction appelée pour changer la résolution à partir du dropdown
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        // Appliquer la résolution et tenir compte de l'état plein écran
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Fonction appelée pour activer/désactiver le plein écran
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}