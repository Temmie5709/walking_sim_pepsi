using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private CharacterController characterController;

    // Référence au script InputBinding pour les touches personnalisées
    private InputBinding inputBinding;

    // Vecteurs pour le déplacement
    float x = 0f;
    float z = 0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Trouver l'objet qui contient le script InputBinding (assure-toi qu'il soit sur un GameObject avec le tag "GameController")
        GameObject inputBindingObject = GameObject.FindWithTag("GameController");
        if (inputBindingObject != null)
        {
            inputBinding = inputBindingObject.GetComponent<InputBinding>();
        }
        else
        {
            Debug.LogError("Aucun objet avec le script InputBinding trouvé");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputBinding == null) return;

        // Réinitialise les valeurs de x et z
        x = 0f;
        z = 0f;

        // Gérer les déplacements horizontaux (gauche/droite)
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Gauche"]))
        {
            x = -1f; // Déplacement à gauche
        }
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Droite"]))
        {
            x = 1f; // Déplacement à droite
        }

        // Gérer les déplacements verticaux (avancer/reculer)
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Avancer"]))
        {
            z = 1f; // Avancer
        }
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Reculer"]))
        {
            z = -1f; // Reculer
        }
    }

    private void FixedUpdate()
    {
        // Calculer le mouvement
        Vector3 move = Vector3.Normalize(transform.right * x + transform.forward * z) * Time.fixedDeltaTime * speed;
        
        // Appliquer le mouvement
        characterController.Move(move);
    }
}