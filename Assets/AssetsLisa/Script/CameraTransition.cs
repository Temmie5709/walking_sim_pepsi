using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Assurez-vous d'inclure cette directive pour l'UI

public class CameraTransition : MonoBehaviour
{
    public Camera mainCamera; // La caméra principale
    public Camera camera1; // Première caméra
    public Camera camera2; // Deuxième caméra
    public GameObject cameraFolder; // Dossier contenant les caméras de transition
    public float transitionDuration = 2f; // Durée de la transition
    public Narration Dialogue;
    public TaskManager tache;
    public AudioSource Notif;
    public AudioSource Clavier;

    public Image blackScreen; // L'image noire utilisée pour le fondu
    public float fadeDuration = 2f; // Durée du fondu (2 secondes)

    void Start()
    {
        // Démarrer le fondu au noir au début
        StartCoroutine(HandleCameraTransition());
        tache.setTaskText("Faire du code");
        tache.CreateTask(1);
        tache.setTaskText("Récupérer le document dans le classeur");
        tache.CreateTask(2);
    }

    IEnumerator HandleCameraTransition()
    {
        // Activer la première caméra et désactiver la principale
        mainCamera.gameObject.SetActive(false);
        camera1.gameObject.SetActive(true);
        Clavier.Play();
        // Faire le fondu du noir à la transparence
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackScreen.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration));
            yield return null;
        }

        // Désactiver l'image noire après le fondu
        blackScreen.gameObject.SetActive(false);
        yield return new WaitForSeconds(9f);
        Clavier.Stop();
        Notif.Play();
        yield return new WaitForSeconds(1f);

        // Transition vers la deuxième caméra
        yield return StartCoroutine(TransitionCameras(camera1, camera2));

        yield return new WaitForSeconds(3f);

        // Lancer le dialogue
        Dialogue.ChangeDialogueSetByName("phone");

        // Transition vers la caméra principale
        yield return StartCoroutine(TransitionCameras(camera2, mainCamera));

        // Désactiver le dossier contenant les caméras de transition
        cameraFolder.SetActive(false);
    }

    IEnumerator TransitionCameras(Camera from, Camera to)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            from.transform.position = Vector3.Lerp(from.transform.position, to.transform.position, elapsedTime / transitionDuration);
            from.transform.rotation = Quaternion.Slerp(from.transform.rotation, to.transform.rotation, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        from.gameObject.SetActive(false);
        to.gameObject.SetActive(true);
    }
}
