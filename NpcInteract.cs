using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public TextAsset dialogueFile;
    private string[] dialogueLines;
    private int currentLine = 0;
    private bool isPlayerNearby = false;
    private bool isDialogueActive = false;


    private void Start()
    {
        if (dialogueFile != null)
        {
            dialogueLines = dialogueFile.text.Split('\n');
            Debug.Log("Dialogue file loaded successfully!");
        }
        else
        {
            Debug.LogError("Dialogue file is missing! Make sure to assign it in the Inspector.");
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            if (!isDialogueActive)
                StartDialogue();
            else
                AdvanceDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogueLines[currentLine].Trim();
    }

    private void AdvanceDialogue()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine].Trim();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        currentLine = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            EndDialogue();
        }
    }
}
