using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro; // Importation de TextMeshPro

public class End : MonoBehaviour
{
    int taskVita = 0;
    int taskPlay = 0;
    public Narration Dialogue;
    public Camera mainCamera;
    public Camera targetCamera;
    public TextMeshProUGUI taskProgressText; // Référence au texte UI TextMeshPro
    public AudioSource Complite;

    private bool hasTriggeredEnd = false;
    private bool hasTriggeredEgg = false;
    private bool hasTriggeredTask3 = false;
    private bool hasTriggeredTask6 = false;
    private bool hasTriggeredMiroirDo = false;
    private bool hasTriggeredSacDo = false;

    void Start()
    {
        UpdateTaskProgressText(); // Initialisation de l'affichage
    }

    public void DoVITATask()
    {
        taskVita++;
    }

    public void DoPlayerTask()
    {
        taskPlay++;
        Complite.Play();
        UpdateTaskProgressText(); // Met à jour l'affichage après incrémentation
    }

    void UpdateTaskProgressText()
    {
        if (taskProgressText != null)
        {
            taskProgressText.text = taskPlay + "/12";
        }
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
        yield return new WaitForSeconds(10);
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
        if (!hasTriggeredMiroirDo)
        {
            hasTriggeredMiroirDo = true;
            StartCoroutine(TriggerMiroirDo());
        }
    }

    public void SacDo()
    {
        if (!hasTriggeredSacDo)
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
