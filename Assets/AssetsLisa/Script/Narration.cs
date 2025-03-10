using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public enum CharacterName
{
    VITA,
    Toshiaki
}

[System.Serializable]
public class DialogLine
{
    public CharacterName character;
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
    public GameObject dialogueContainer; // Empty qui contient toute l'UI
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;
    public float textSpeed = 0.05f;
    private Coroutine activeCoroutine;

    public Image continueIcon;

    public AudioClip vitaSound;
    public AudioClip joueurSound;
    public float typeSoundVolume = 0.5f;
    private AudioSource audioSource;

    public Dictionary<string, NamedDialogue> dialogueSets = new Dictionary<string, NamedDialogue>();
    public List<NamedDialogue> namedDialogues = new List<NamedDialogue>();

    private NamedDialogue currentDialogue;
    private int textIndex = 0;
    private bool isTextDisplaying = false;

    public PlayerMove playerMove; // R�f�rence au script PlayerMove
    public PlayerLook cameraController; // R�f�rence au script de la cam�ra (si vous en avez un)

    private Vector3 initialCameraPosition; // Position initiale de la cam�ra
    private Quaternion initialCameraRotation; // Rotation initiale de la cam�ra

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
        audioSource.volume = typeSoundVolume;
        audioSource.playOnAwake = false;
        audioSource.loop = true;

        if (cameraController != null)
        {
            initialCameraPosition = Camera.main.transform.position; // Position initiale de la cam�ra
            initialCameraRotation = Camera.main.transform.rotation; // Rotation initiale de la cam�ra
        }
    }

    void Start()
    {
        dialogueContainer.SetActive(false); // D�sactive l'UI au d�marrage
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
            dialogueContainer.SetActive(true); // Active l'UI quand un dialogue commence

            // D�sactiver les contr�les du joueur pendant le dialogue
            if (playerMove != null)
            {
                playerMove.enabled = false; // D�sactive le mouvement du joueur
            }

            // Rendre la cam�ra statique pendant le dialogue
            if (cameraController != null)
            {
                cameraController.enabled = false; // D�sactive le contr�leur de la cam�ra
            }
            else
            {
                // Si aucun script de cam�ra n'est pr�sent, on fixe la position et la rotation de la cam�ra
                Camera.main.transform.position = initialCameraPosition;
                Camera.main.transform.rotation = initialCameraRotation;
            }

            if (currentDialogue.dialogues.Count > 0)
            {
                currentDialogue.dialogues[textIndex].lineEvent.Invoke();
                activeCoroutine = StartCoroutine(DisplayText(currentDialogue.dialogues[textIndex]));
            }
            else
            {
                dialogueContainer.SetActive(false); // D�sactive si aucun dialogue
            }
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
        else
        {
            // R�activer les contr�les du joueur apr�s la fin du dialogue
            if (playerMove != null)
            {
                playerMove.enabled = true; // R�active le mouvement du joueur
            }

            // R�activer le contr�leur de la cam�ra apr�s la fin du dialogue
            if (cameraController != null)
            {
                cameraController.enabled = true; // R�active le contr�leur de la cam�ra
            }

            dialogueContainer.SetActive(false); // D�sactive l'UI � la fin des dialogues
        }
    }

    IEnumerator DisplayText(DialogLine dialogue)
    {
        isTextDisplaying = true;
        textComponent.text = "";
        nameComponent.text = dialogue.character.ToString();

        PlayCharacterSound(dialogue.character);

        foreach (char c in dialogue.line)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (continueIcon != null) continueIcon.enabled = true;
        isTextDisplaying = false;
    }

    void PlayCharacterSound(CharacterName character)
    {
        switch (character)
        {
            case CharacterName.VITA:
                audioSource.clip = vitaSound;
                break;
            case CharacterName.Toshiaki:
                audioSource.clip = joueurSound;
                break;
        }
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}

