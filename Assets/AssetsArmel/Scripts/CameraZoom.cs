using UnityEngine;
using UnityEngine.SceneManagement; // Import nécessaire pour changer de scène

public class CameraZoom : MonoBehaviour
{
    public Transform startCameraPosition;  // Position initiale (dézoomée)
    public Transform targetCameraPosition; // Position finale (vue première personne)
    public float zoomSpeed = 2f;           // Vitesse du zoom
    public string sceneToLoad = "Map";     // Nom de la scène à charger après le zoom

    private bool isZooming = false;        // Pour détecter si le zoom est en cours

    // Update appelé chaque frame
    void Update()
    {
        if (isZooming)
        {
            // Interpolation de la position de la caméra vers la cible
            transform.position = Vector3.Lerp(transform.position, targetCameraPosition.position, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetCameraPosition.rotation, Time.deltaTime * zoomSpeed);

            // Si la caméra est suffisamment proche de la cible, on arrête le zoom
            if (Vector3.Distance(transform.position, targetCameraPosition.position) < 0.1f)
            {
                isZooming = false;
                LoadNextScene(); // Appel à la fonction de changement de scène
            }
        }
    }

    // Fonction appelée pour démarrer le zoom (sera déclenchée après la fin du son)
    public void StartZoom()
    {
        isZooming = true;
    }

    // Fonction pour charger la scène après le zoom
    private void LoadNextScene()
    {
        // Charger la scène spécifiée après le zoom
        SceneManager.LoadScene(sceneToLoad);
    }
}