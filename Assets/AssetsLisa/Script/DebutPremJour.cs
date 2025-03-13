using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutPremJour : MonoBehaviour
{
    public float transitionDuration = 2f;
    public Camera cameraScrean;
    public Camera mainCamera;
    public Narration Dialogue;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HandleCameraTransition());
    }

    IEnumerator HandleCameraTransition()
    {
        // Activer la premi�re cam�ra et d�sactiver la principale
        mainCamera.gameObject.SetActive(false);
        cameraScrean.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        // Transition vers la deuxi�me cam�ra
        yield return StartCoroutine(TransitionCameras(cameraScrean, mainCamera));

        yield return new WaitForSeconds(0.001f);

        // Lancer le dialogue
        Dialogue.ChangeDialogueSetByName("Reveil");

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
    void Update()
    {
        
    }
}
