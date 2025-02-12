using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class DialogLine
{
    public string characterName = ""; // Nom du personnage
    public string line = "";
    public UnityEvent lineEvent;
}

[System.Serializable]
public class NamedDialogue
{
    public string name;
    public List<DialogLine> dialogues = new List<DialogLine>();
}

public class Narration : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent; // Affichage du nom du personnage
    public float textSpeed = 0.05f;
    private Coroutine activeCoroutine;

    public Image continueIcon;

    public AudioClip typeSound;
    public float typeSoundVolume = 0.5f;
    private AudioSource audioSource;

    public Dictionary<string, NamedDialogue> dialogueSets = new Dictionary<string, NamedDialogue>();
    public List<NamedDialogue> namedDialogues = new List<NamedDialogue>();

    public NamedDialogue currentDialogue;
    private int textIndex = 0;
    private bool isTextDisplaying = false;

    void Awake()
    {
        foreach (var namedDialogue in namedDialogues)
        {
            if (!dialogueSets.ContainsKey(namedDialogue.name))
            {
                dialogueSets.Add(namedDialogue.name, namedDialogue);
            }
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = typeSound;
        audioSource.volume = typeSoundVolume;
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }

    void Start()
    {
        ChangeDialogueSetByName("Start");

        if (continueIcon != null) continueIcon.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTextDisplaying && currentDialogue != null)
        {
            if (continueIcon != null) continueIcon.enabled = false;
            NextText();
        }
    }

    public void ChangeDialogueSetByName(string dialogueName)
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        if (dialogueSets.TryGetValue(dialogueName, out var namedDialogue))
        {
            currentDialogue = namedDialogue;
            textIndex = 0;

            if (currentDialogue.dialogues.Count > 0)
            {
                currentDialogue.dialogues[textIndex].lineEvent.Invoke();
                activeCoroutine = StartCoroutine(DisplayText(currentDialogue.dialogues[textIndex]));
            }
            else
            {
                Debug.LogWarning($"Dialogue set '{dialogueName}' is empty.");
            }
        }
        else
        {
            Debug.LogWarning($"Dialogue set '{dialogueName}' not found.");
        }
    }

    void NextText()
    {
        if (currentDialogue != null && textIndex < currentDialogue.dialogues.Count - 1)
        {
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = null;
            }

            textIndex++;
            currentDialogue.dialogues[textIndex].lineEvent.Invoke();
            activeCoroutine = StartCoroutine(DisplayText(currentDialogue.dialogues[textIndex]));
        }
    }

    IEnumerator DisplayText(DialogLine dialogue)
    {
        isTextDisplaying = true;
        textComponent.text = "";
        nameComponent.text = dialogue.characterName; // Affiche le nom du personnage

        if (typeSound != null && audioSource != null)
        {
            float randomStartTime = Random.Range(0f, typeSound.length);
            audioSource.time = randomStartTime;
            audioSource.Play();
        }

        foreach (char c in dialogue.line)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (continueIcon != null) continueIcon.enabled = true;
        isTextDisplaying = false;
    }
}
