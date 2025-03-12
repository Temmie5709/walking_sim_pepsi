using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    public Camera mainCamera;
    public Camera zoomCamera;
    public GameObject overlayUI;

    public bool isZoomed = false;
    private bool canToggle = true; // Empêche le double toggle immédiat

    void Start()
    {
        zoomCamera.gameObject.SetActive(false);
        overlayUI.SetActive(false);
    }

    void Update()
    {
        if (canToggle && isZoomed && Input.GetKeyDown(KeyCode.E))
        {
            ToggleZoom();
        }
    }

    public void ToggleZoom()
    {
        if (!canToggle) return; // Empêche le spam
        StartCoroutine(ToggleCooldown());

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

    IEnumerator ToggleCooldown()
    {
        canToggle = false;
        yield return new WaitForSeconds(0.1f); // Petit délai pour éviter le double appel
        canToggle = true;
    }
}
