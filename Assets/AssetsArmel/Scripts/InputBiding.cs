using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBinding : MonoBehaviour
{
    [SerializeField] private InputInfos[] baseInputs; // Liste des entrées de base
    private Dictionary<string, char> inputsDictionary = new Dictionary<string, char>(); // Dictionnaire pour gérer les entrées

    [SerializeField] private Text[] inputTexts; // Référence à l'élément UI Text pour afficher les touches
    [SerializeField] private Button[] inputButtons; // Référence aux boutons pour chaque action
    private string bindingAxis = ""; // Axe actuellement modifié

    public Text messageText; // Texte pour afficher le message temporaire

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation des entrées de base dans le dictionnaire
        foreach (var _input in baseInputs)
        {
            inputsDictionary.Add(_input.Name, _input.Key);
        }

        // Cacher le message au démarrage
        messageText.gameObject.SetActive(false);

        // Afficher les touches par défaut dans l'UI
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Si une touche est en cours de modification
        if (bindingAxis != "")
        {
            messageText.gameObject.SetActive(true); // Affiche le message pendant la modification
            
            if (Input.anyKeyDown)
            {
                foreach (KeyCode _keycode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(_keycode))
                    {
                        // Met à jour la touche dans le dictionnaire
                        inputsDictionary[bindingAxis] = (char)_keycode;

                        // Met à jour l'affichage dans l'UI
                        UpdateUI();

                        // Cacher le message une fois la touche attribuée
                        messageText.gameObject.SetActive(false);

                        // Réinitialise l'axe en cours de modification
                        bindingAxis = "";
                        return;
                    }
                }
            }
        }

        // Tester les entrées
        foreach (var _inputAxis in inputsDictionary.Keys)
        {
            TestInput(_inputAxis);
        }
    }

    // Test si l'utilisateur appuie sur la touche assignée
    private void TestInput(string _inputAxis)
    {
        bool _input = Input.GetKey((KeyCode)inputsDictionary[_inputAxis]);
        if (_input) Debug.Log(_inputAxis + " : " + inputsDictionary[_inputAxis]);
    }

    // Méthode pour lier un nouvel axe (touche à modifier)
    public void Bind(string _axis)
    {
        bindingAxis = _axis;

        /*for (int i = 0; i < baseInputs.Length; i++)
        {
            // Mise à jour du texte associé à l'entrée
            inputTexts[i].text = $"?";
        }*/

        // Affiche le message dès que la modification commence
        messageText.gameObject.SetActive(true);

        // Localise le bouton correspondant à l'axe modifié
        for (int i = 0; i < baseInputs.Length; i++)
        {
            if (baseInputs[i].Name == _axis)
            {
                // Positionner le messageText à droite du bouton correspondant
                RectTransform buttonRectTransform = inputButtons[i].GetComponent<RectTransform>();
                Vector3 buttonPosition = buttonRectTransform.position;

                // Ajuster la position du message (ici, on le décale de 100 unités à droite du bouton)
                messageText.rectTransform.position = new Vector3(buttonPosition.x+300 + buttonRectTransform.rect.width + 10, buttonPosition.y, buttonPosition.z);
                break;
            }
        }
    }

    // Mise à jour de l'affichage des touches dans l'UI
    private void UpdateUI()
    {
        for (int i = 0; i < baseInputs.Length; i++)
        {
            string inputName = baseInputs[i].Name;
            char keyAssigned = inputsDictionary[inputName];

            // Mise à jour du texte associé à l'entrée
            inputTexts[i].text = $"{keyAssigned.ToString().ToUpper()}";
        }
    }
}

[System.Serializable]
public struct InputInfos
{
    [SerializeField] private string inputName; // Nom de l'action (ex: Avancer)
    [SerializeField] private char key;        // Touche par défaut

    public string Name => inputName;          // Accesseur pour le nom de l'action
    public char Key => key;                   // Accesseur pour la touche par défaut
}