using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class End : MonoBehaviour
{
    int taskVita = 0;
    public int taskPlay = 0;
    public Narration Dialogue; // Déclaration du dialogue
    public Camera mainCamera;
    public Camera targetCamera;
    private bool hasTriggeredEnd = false; // Vérifie si la fin a déjà été déclenchée
    private bool hasTriggeredEgg = false;
    private bool hasTriggeredTask3 = false;
    private bool hasTriggeredTask6 = false;
    private bool hasTriggeredMiroirDo = false; // Vérifie si le dialogue miroirdo a été lancé
    private bool hasTriggeredSacDo = false;   // Vérifie si le dialogue sacdo a été lancé

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
        hasTriggeredEnd = true;
        yield return new WaitForSeconds(15);
        SwitchCamera();
        Dialogue.ChangeDialogueSetByName("fin");
    }

    void Update()
    {
        if (taskPlay == 12 && !hasTriggeredEnd)
        {
            StartCoroutine(TriggerEndSequence());
        }

        if (taskVita == 100 && !hasTriggeredEgg)
        {
            hasTriggeredEgg = true;
            Dialogue.ChangeDialogueSetByName("egg");
        }

        if (taskVita == 3 && !hasTriggeredTask3)
        {
            hasTriggeredTask3 = true;
            Dialogue.ChangeDialogueSetByName("3task");
        }

        if (taskVita == 6 && !hasTriggeredTask6)
        {
            hasTriggeredTask6 = true;
            Dialogue.ChangeDialogueSetByName("6task");
        }
    }

    public void finfin()
    {
        mainCamera.gameObject.SetActive(true);
        targetCamera.gameObject.SetActive(false);
    }

    public void MiroirDo()
    {
        if (!hasTriggeredMiroirDo) // Vérifie si la séquence n'a pas encore été jouée
        {
            hasTriggeredMiroirDo = true;
            StartCoroutine(TriggerMiroirDo());
        }
    }

    public void SacDo()
    {
        if (!hasTriggeredSacDo) // Vérifie si la séquence n'a pas encore été jouée
        {
            hasTriggeredSacDo = true;
            StartCoroutine(TriggerSacDo());
        }
    }

    IEnumerator TriggerMiroirDo()
    {
        yield return new WaitForSeconds(7);
        Dialogue.ChangeDialogueSetByName("miroirdo");
    }

    IEnumerator TriggerSacDo()
    {
        yield return new WaitForSeconds(7);
        Dialogue.ChangeDialogueSetByName("sacdo");
    }
}
