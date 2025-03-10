using System.Collections;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Camera mainCamera; // La caméra principale
    public Camera camera1; // Première caméra
    public Camera camera2; // Deuxième caméra
    public GameObject cameraFolder; // Dossier contenant les caméras de transition
    public float transitionDuration = 2f; // Durée de la transition
    public Narration Dialogue;

    void Start()
    {
        StartCoroutine(HandleCameraTransition());
    }

    IEnumerator HandleCameraTransition()
    {
        // Activer la première caméra et désactiver la principale
        mainCamera.gameObject.SetActive(false);
        camera1.gameObject.SetActive(true);

        yield return new WaitForSeconds(10f);

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
