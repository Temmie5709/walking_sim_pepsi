using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    int taskVita = 0;
    public int taskPlay = 0;
    public Narration Dialogue; // Déclaration du dialogue
    public Camera mainCamera;
    public Camera targetCamera;
    private bool hasTriggeredEnd = false; // Vérifie si la fin a déjà été déclenchée

    public void DoVITATask()
    {
        taskVita++;
    }

    public void DoPlayerTask()
    {
        taskPlay++;
    }

    void SwitchCamera()
    {
        if (mainCamera != null && targetCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            targetCamera.gameObject.SetActive(true);
        }
    }

    IEnumerator TriggerEndSequence()
    {
        hasTriggeredEnd = true; // Marque la séquence comme déclenchée
        yield return new WaitForSeconds(5);
        SwitchCamera();
        Dialogue.ChangeDialogueSetByName("fin");
    }

    // Update is called once par frame
    void Update()
    {
        if (taskPlay == 12 && !hasTriggeredEnd) // Vérifie si la séquence n'a pas encore été jouée
        {
            StartCoroutine(TriggerEndSequence());
        }
    }

    public void finfin()
    {
        mainCamera.gameObject.SetActive(true);
        targetCamera.gameObject.SetActive(false);
    }
}
