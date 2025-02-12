using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    [SerializeField] float interactionDistance = 3f;

    private IInteractable LastInteractable;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastInteraction();
    }

    void RaycastInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // Si on regarde un nouvel objet interactif
                if (interactable != LastInteractable)
                {
                    if (LastInteractable != null)
                        LastInteractable.StopLooking(); // On arrête de regarder l'ancien objet

                    interactable.Looking(); // Active l'effet de "regard"
                    LastInteractable = interactable; // On met à jour l'objet regardé
                }
                // Interaction avec E
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
            else
            {
                // Si le raycast ne touche plus d'objet interactif
                if (LastInteractable != null)
                {
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