using System.Collections;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerMove playerMove; // Script de mouvement du joueur
    public Camera mainCamera;
    public Camera zoomCamera;
    public float transitionSpeed = 2f;

    private bool isZoomed = false;
    private bool inTransition = false;

    private AudioListener mainAudioListener;
    private AudioListener zoomAudioListener;

    void Start()
    {
        zoomCamera.gameObject.SetActive(false);
        mainAudioListener = mainCamera.GetComponent<AudioListener>();
        zoomAudioListener = zoomCamera.GetComponent<AudioListener>();
        if (zoomAudioListener != null)
        {
            zoomAudioListener.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inTransition)
        {
            if (isZoomed)
            {
                StartCoroutine(SwitchCamera(zoomCamera, mainCamera, true));
            }
        }
    }

    public void ActivateZoom()
    {
        if (!isZoomed && !inTransition)
        {
            zoomCamera.gameObject.SetActive(true);
            StartCoroutine(SwitchCamera(mainCamera, zoomCamera, false));
        }
    }

    private IEnumerator SwitchCamera(Camera from, Camera to, bool enableMovement)
    {
        inTransition = true;
        float elapsedTime = 0f;

        Vector3 startPos = from.transform.position;
        Quaternion startRot = from.transform.rotation;

        Vector3 endPos = to.transform.position;
        Quaternion endRot = to.transform.rotation;

        to.gameObject.SetActive(true);

        AudioListener fromAudio = from.GetComponent<AudioListener>();
        AudioListener toAudio = to.GetComponent<AudioListener>();
        if (fromAudio != null) fromAudio.enabled = false;
        if (toAudio != null) toAudio.enabled = true;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * transitionSpeed;
            from.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime);
            from.transform.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime);
            yield return null;
        }

        from.gameObject.SetActive(false);
        isZoomed = !enableMovement;
        characterController.enabled = enableMovement;
        playerMove.enabled = enableMovement;

        if (enableMovement)
        {
            zoomCamera.gameObject.SetActive(false);
        }

        inTransition = false;
    }
}
