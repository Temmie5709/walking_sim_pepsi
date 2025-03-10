using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject settingsPanel;  // Panneau des paramètres
    public GameObject keyBindingsPanel;  // Panneau pour les assignations des touches (à créer dans l'éditeur)
    public GameObject settingsButtonsPanel;  // Panneau pour les deux boutons "Paramètres du jeu" et "Assignations des touches" (à créer dans l'éditeur)
    public Slider volumeSlider;       // Slider pour le volume
    public Dropdown resolutionDropdown; // Dropdown pour choisir la résolution
    public Toggle fullscreenToggle;   // Toggle pour le mode plein écran
    public AudioSource musiqueDeFond;   // Source audio pour gérer le volume

    public GameObject menuPanel;
    public Text gameTitleText;        // Texte du nom du jeu

    private string originalGameTitle = "Walking Simulator"; // Remplacer par le nom de votre jeu
    private Resolution[] resolutions; // Liste des résolutions disponibles

    void Start()
    {
        menuPanel.SetActive(true);

        // Définir le volume initial de la musique de fond à 20 %
        musiqueDeFond.volume = 0.2f; // 20% du volume

        // Initialiser les résolutions disponibles dans le dropdown
        resolutionDropdown.ClearOptions();
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

        // Désactiver les panneaux au démarrage
        settingsPanel.SetActive(false);
        keyBindingsPanel.SetActive(false);
        settingsButtonsPanel.SetActive(false);

        // Assigner le volume par défaut au Slider et le connecter
        volumeSlider.value = musiqueDeFond.volume; // Assigner la valeur actuelle de l'AudioSource
        volumeSlider.onValueChanged.AddListener(SetVolume); // Lier le slider à la fonction SetVolume

        // Lier le toggle de plein écran
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    private void Update()
    {
        // Détecter si la touche Echap est pressée
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Vérifier si le panneau des paramètres est actif
            if (settingsPanel.activeSelf || keyBindingsPanel.activeSelf)
            {
                // Fermer les panneaux de paramètres et d'assignation des touches
                ReturnSettings();
            }
        }
    }

    // Fonction appelée par le bouton "Paramètres"
    public void OpenSettingsMenu()
    {
        // Cacher le menu principal et afficher les deux boutons "Paramètres du jeu" et "Assignations des touches"
        menuPanel.SetActive(false);
        settingsButtonsPanel.SetActive(true);

        // Mettre à jour le texte du titre si nécessaire
        gameTitleText.fontSize = 220;
        gameTitleText.text = "Paramètres";
    }

    // Fonction appelée pour ouvrir les paramètres du jeu
    public void OpenGameSettings()
    {
        // Masquer le panneau des boutons de paramètres et ouvrir le panneau des paramètres du jeu
        settingsButtonsPanel.SetActive(false);
        settingsPanel.SetActive(true);

        // Mettre à jour le texte du titre
        gameTitleText.fontSize = 180;
        gameTitleText.text = "Paramètres du jeu";
    }

    // Fonction appelée pour ouvrir le panneau des assignations de touches
    public void OpenKeyBindingsSettings()
    {
        // Masquer le panneau des boutons de paramètres et ouvrir le panneau des assignations des touches
        settingsButtonsPanel.SetActive(false);
        keyBindingsPanel.SetActive(true);

        // Mettre à jour le texte du titre
        gameTitleText.fontSize = 180;
        gameTitleText.text = "Attribution des touches";
    }

    // Fonction pour fermer les panneaux de paramètres et revenir au menu principal
    public void CloseSettings()
    {
        // Fermer tous les panneaux de paramètres
        settingsPanel.SetActive(false);
        keyBindingsPanel.SetActive(false);
        settingsButtonsPanel.SetActive(false);

        // Réafficher le menu principal
        menuPanel.SetActive(true);

        // Réinitialiser le texte du titre au nom original du jeu
        gameTitleText.fontSize = 220;
        gameTitleText.text = originalGameTitle;
    }

    // Fonction pour fermer les panneaux de paramètres et revenir au menu principal
    public void ReturnSettings()
    {
        // Si le panneau des paramètres est actif, retourner au menu des boutons de paramètres
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            settingsButtonsPanel.SetActive(true); // Retour aux choix de "Paramètres du jeu" ou "Assignations des touches"
            gameTitleText.fontSize = 220;
            gameTitleText.text = "Paramètres";
        }
        // Si le panneau d'assignation des touches est actif, retourner au menu des boutons de paramètres
        else if (keyBindingsPanel.activeSelf)
        {
            keyBindingsPanel.SetActive(false);
            settingsButtonsPanel.SetActive(true); // Retour aux choix de "Paramètres du jeu" ou "Assignations des touches"
            gameTitleText.fontSize = 220;
            gameTitleText.text = "Paramètres";
        }
        // Si aucun sous-panneau n'est actif, retourner au menu principal
        else if (settingsButtonsPanel.activeSelf)
        {
            settingsButtonsPanel.SetActive(false);
            menuPanel.SetActive(true); // Retour au menu principal
            gameTitleText.text = originalGameTitle;
        }
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