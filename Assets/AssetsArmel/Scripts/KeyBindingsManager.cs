using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingsManager : MonoBehaviour
{
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); // Stockage des touches actives
    private Dictionary<string, KeyCode> pendingKeys = new Dictionary<string, KeyCode>(); // Stockage temporaire des touches modifiées

    public Text forwardText, backwardText, leftText, rightText, interactText; // UI Texts pour afficher les touches actuelles
    public Text messageText; // Texte pour afficher le message temporaire
    public Button validateButton; // Bouton "Valider" pour confirmer les changements

    private GameObject currentKey; // Panneau d'assignation actuel

    void Start()
    {
        // Initialisation des touches avec les valeurs par défaut ou les préférences de l'utilisateur
        keys["Forward"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        keys["Backward"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        keys["Left"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        keys["Right"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        keys["Interact"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "F"));

        // Copier les touches actives dans les touches temporaires
        pendingKeys = new Dictionary<string, KeyCode>(keys);

        // Mise à jour des textes de l'interface utilisateur
        UpdateKeyText();

        // Initialiser le message à vide
        messageText.text = "";

        // Désactiver le bouton "Valider" tant qu'aucun changement n'est fait
        validateButton.interactable = false;
    }

    void Update()
    {
        // Utilisation des touches dans le jeu avec les touches actives (pas celles en attente)
        if (Input.GetKey(keys["Forward"]))
        {
            //Debug.Log("Avancer");
        }
        if (Input.GetKey(keys["Backward"]))
        {
            //Debug.Log("Reculer");
        }
        if (Input.GetKey(keys["Left"]))
        {
            //Debug.Log("Aller à gauche");
        }
        if (Input.GetKey(keys["Right"]))
        {
            //Debug.Log("Aller à droite");
        }
        if (Input.GetKey(keys["Interact"]))
        {
            //Debug.Log("Interagir");
        }
    }

    public void ChangeKey(GameObject clickedKey)
    {
        currentKey = clickedKey;

        // Afficher le message d'instruction en italique
        messageText.text = "<i>Veuillez appuyer sur une touche de clavier</i>";

        // Placer le message à droite du bouton cliqué
        RectTransform buttonRect = clickedKey.GetComponent<RectTransform>();
        messageText.transform.position = new Vector3(buttonRect.position.x + 280 + buttonRect.rect.width + 20, buttonRect.position.y, 0);
    }

    private void OnGUI()
    {
        // S'assurer qu'une clé est en attente de modification
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                // Récupérer le nom de l'action correspondante au bouton cliqué
                string actionName = currentKey.name;

                // Vérifier si la touche n'est pas déjà assignée à une autre action dans les touches actives
                if (!pendingKeys.ContainsValue(e.keyCode))
                {
                    // Mettre à jour la touche dans le dictionnaire temporaire
                    pendingKeys[actionName] = e.keyCode;

                    // Mettre à jour le visuel du texte sur le bouton cliqué
                    currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();

                    // Activer le bouton "Valider" après un changement
                    validateButton.interactable = true;

                    // Effacer le message une fois la touche assignée
                    messageText.text = "";

                    // Réinitialiser la touche courante
                    currentKey = null;
                }
                else
                {
                    // Si la touche est déjà assignée à une autre action, afficher un message d'erreur
                    messageText.text = "<i>Cette touche est déjà assignée à une autre action !</i>";
                }
            }
        }
    }

    // Méthode pour valider les changements et les rendre actifs
    public void ValidateChanges()
    {
        // Appliquer les changements de pendingKeys dans keys
        keys = new Dictionary<string, KeyCode>(pendingKeys);

        // Sauvegarder les nouvelles touches dans les préférences utilisateur
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        // Désactiver le bouton "Valider" après validation
        validateButton.interactable = false;

        // Afficher un message de confirmation
        messageText.text = "<i>Changements validés !</i>";

        // Démarrer la coroutine pour faire disparaître le message après 3 secondes
        StartCoroutine(FadeOutMessage());
    }

    // Coroutine pour faire disparaître progressivement le message
    private IEnumerator FadeOutMessage()
    {
        // Durée totale de l'animation (3 secondes)
        float duration = 3f;
        float elapsedTime = 0f;

        // Récupérer la couleur actuelle du texte
        Color originalColor = messageText.color;

        // Boucle sur la durée
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            // Calculer la nouvelle opacité (de 100% à 0%)
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            messageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null; // Attendre le prochain frame
        }

        // Assurer que l'opacité soit bien à 0 à la fin
        messageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Effacer le texte une fois l'animation terminée
        messageText.text = "";
    }

    // Fonction pour mettre à jour les textes affichés dans l'UI
    private void UpdateKeyText()
    {
        forwardText.text = keys["Forward"].ToString();
        backwardText.text = keys["Backward"].ToString();
        leftText.text = keys["Left"].ToString();
        rightText.text = keys["Right"].ToString();
        interactText.text = keys["Interact"].ToString();
    }
}