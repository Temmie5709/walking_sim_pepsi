using System.Collections;
using UnityEngine;

public class Manger : MonoBehaviour
{
    public float transitionDuration = 2f;
    public Camera cameraScrean;
    public Camera mainCamera;
    public Narration Dialogue;
    public AudioSource bouche;
    public AudioSource musique;
    public GameObject bacon;

    void Start()
    {
        cameraScrean.gameObject.SetActive(false);
    }

    public void TransManger()
    {
        StartCoroutine(HandleCameraTransition());
    }

    IEnumerator HandleCameraTransition()
    {
        // Small delay to ensure the frames are properly updated
        yield return new WaitForSeconds(0.01f);

        // Switch cameras
        cameraScrean.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);

        // Play the sound effects
        bouche.Play();
        musique.Pause();

        // Wait before switching to the next part
        yield return new WaitForSeconds(5f);

        // Stop sound effects
        bouche.Stop();
        musique.Play();

        // Hide bacon object
        bacon.GetComponent<MeshRenderer>().enabled = false;

        // Disable the camera we just enabled, and switch back to the main camera
        cameraScrean.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        // Trigger the dialogue change
        Dialogue.ChangeDialogueSetByName("Manger");
    }
}
