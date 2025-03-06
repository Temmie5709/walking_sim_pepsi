using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputs : MonoBehaviour
{
    // Référence au script InputBinding
    private InputBinding inputBinding;

    // Start is called before the first frame update
    void Start()
    {
        // Trouver l'objet qui contient le script InputBinding (assure-toi qu'il soit sur un GameObject avec le tag "GameController" par exemple)
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

        // Vérifier si la touche Z est appuyée (Avancer)
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Avancer"]))
        {
            Debug.Log("Avancer");
        }

        // Vérifier si la touche Q est appuyée (Aller à gauche)
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Gauche"]))
        {
            Debug.Log("Gauche");
        }

        // Vérifier si la touche D est appuyée (Aller à droite)
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Droite"]))
        {
            Debug.Log("Droite");
        }

        // Vérifier si la touche S est appuyée (Reculer)
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Reculer"]))
        {
            Debug.Log("Reculer");
        }

        // Vérifier si la touche F est appuyée (Interagir)
        if (Input.GetKey((KeyCode)inputBinding.inputsDictionary["Interagir"]))
        {
            Debug.Log("Interagir");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("ça marche pas");
        }
    }
}