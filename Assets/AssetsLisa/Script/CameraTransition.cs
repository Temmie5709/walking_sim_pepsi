using System.Collections;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Camera mainCamera; // La cam�ra principale
    public Camera camera1; // Premi�re cam�ra
    public Camera camera2; // Deuxi�me cam�ra
    public GameObject cameraFolder; // Dossier contenant les cam�ras de transition
    public float transitionDuration = 2f; // Dur�e de la transition
    public Narration Dialogue;
    public TaskManager tache;
    public AudioSource Notif;
    public AudioSource Clavier;

    void Start()
    {
        StartCoroutine(HandleCameraTransition());
        tache.setTaskText("Faire du code");
        tache.CreateTask(1);
        tache.setTaskText("Récupérer le document dans le classeur");
        tache.CreateTask(2);
    }

    IEnumerator HandleCameraTransition()
    {
        // Activer la premi�re cam�ra et d�sactiver la principale
        mainCamera.gameObject.SetActive(false);
        camera1.gameObject.SetActive(true);
        Clavier.Play();
        yield return new WaitForSeconds(9f);
        Clavier.Stop();
        Notif.Play();
        yield return new WaitForSeconds(1f);
        // Transition vers la deuxi�me cam�ra
        yield return StartCoroutine(TransitionCameras(camera1, camera2));

        yield return new WaitForSeconds(3f);

        // Lancer le dialogue
        Dialogue.ChangeDialogueSetByName("phone");

        // Transition vers la cam�ra principale
        yield return StartCoroutine(TransitionCameras(camera2, mainCamera));

        // D�sactiver le dossier contenant les cam�ras de transition
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
