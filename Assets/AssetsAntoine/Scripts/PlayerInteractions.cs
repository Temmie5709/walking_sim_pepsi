using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Utiliser TMP_Text pour TextMeshPro

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] float interactionDistance = 3f;
    [SerializeField] GameObject InteractPopUp;

    // Référence pour le texte du pop-up avec TextMeshPro
    [SerializeField] TMP_Text interactPopUpText;  // Remplacé par TMP_Text pour TextMeshPro

    private IInteractable LastInteractable;

    // Référence au script InputBinding pour les touches personnalisées
    private InputBinding inputBinding;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called une fois par frame
    void Update()
    {
        if (inputBinding == null) return; // S'assurer que le script InputBinding est trouvé
        RaycastInteraction();
    }

    void RaycastInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null && interactable.IsActive())
            {
                // Si on regarde un nouvel objet interactif
                if (interactable != LastInteractable)
                {
                    if (LastInteractable != null)
                    {
                        InteractPopUp.SetActive(false);
                        LastInteractable.StopLooking(); // On arrête de regarder l'ancien objet
                    }

                    interactable.Looking(); // Active l'effet de "regard"
                    InteractPopUp.SetActive(true);

                    // Mettre à jour le texte d'interaction avec la touche d'interaction actuelle
                    string interactionKey = inputBinding.inputsDictionary["Interagir"].ToString().ToUpper();  // Convertir la touche en majuscule
                    interactPopUpText.text = interactionKey;  // Met à jour le texte

                    LastInteractable = interactable; // Mettre à jour l'objet regardé
                }

                // Interaction avec la touche définie dans le dictionnaire
                if (Input.GetKeyDown((KeyCode)inputBinding.inputsDictionary["Interagir"]))
                {
                    interactable.Interact();
                }
            }
            else
            {
                // Si le raycast ne touche plus d'objet interactif
                if (LastInteractable != null)
                {
                    InteractPopUp.SetActive(false);
                    LastInteractable.StopLooking();
                    LastInteractable = null;
                }
            }
        }
        else
        {
            // Si aucun objet n'est touché par le Raycast
            if (LastInteractable != null)
            {
                InteractPopUp.SetActive(false);
                LastInteractable.StopLooking();
                LastInteractable = null;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Obtenir le raycast actuel
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Définir la couleur du Gizmo (vert si on touche un objet, rouge sinon)
        Gizmos.color = Color.red;

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            Gizmos.color = Color.green; // Si on touche un objet, on change la couleur en vert
            Gizmos.DrawLine(ray.origin, hit.point); // Dessiner le rayon jusqu'à l'impact
            Gizmos.DrawSphere(hit.point, 0.1f); // Dessiner un petit point sur l'impact
        }
        else
        {
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * interactionDistance); // Dessiner le rayon complet
        }
    }
}