using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    public Camera mainCamera; // Caméra principale
    public Camera zoomCamera; // Caméra de zoom
    public GameObject overlayUI; // Overlay affiché lors du zoom

    public bool isZoomed = false;

    void Start()
    {
        zoomCamera.gameObject.SetActive(false);
        overlayUI.SetActive(false);
    }

    void Update()
    {
        // Si le joueur est en zoom, il peut appuyer sur E pour en sortir
        if (isZoomed && Input.GetKeyDown(KeyCode.E))
        {
            ToggleZoom();
        }
    }

    public void ToggleZoom()
    {
        isZoomed = !isZoomed;

        if (isZoomed)
        {
            mainCamera.gameObject.SetActive(false);
            zoomCamera.gameObject.SetActive(true);
            overlayUI.SetActive(true);
        }
        else
        {
            zoomCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            overlayUI.SetActive(false);
        }
    }
}
