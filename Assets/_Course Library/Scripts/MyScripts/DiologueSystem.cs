using UnityEngine;
using UnityEngine.UI; // for button and selectable
using TMPro; // include if using TextMeshPro
public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue 
    {
        [TextArea(2, 5)] // to make input easier in inspector
        public string[] sentences; // array of sentences in dialogue
    }

    public Dialogue skeletonDialogue; // assign dialogue in inspector
    public TMP_Text dialogueText; // Assign TextMeshPro Text in Inspector
    public GameObject dialogueBox; // Canvas or UI element containing the text
    public GameObject nextButton; // button for advancing dialogue

    private int currentSentenceIndex = 0;
    private bool isInDialogue = false;

    void Start()
    {
        dialogueBox.SetActive(false); // hide dialogue box initially
        if (nextButton != null) nextButton.SetActive(false);
    }
    public void StartDialogue()
    {
        if (skeletonDialogue.sentences.Length > 0)
        {
           dialogueBox.SetActive(true);
            isInDialogue = true;
            currentSentenceIndex = 0;
            ShowNextSentence();
        }
    }
    public void ShowNextSentence()
    {
        if (currentSentenceIndex < skeletonDialogue.sentences.Length)
        {
            dialogueText.text = skeletonDialogue.sentences[currentSentenceIndex];
            currentSentenceIndex++;

            if (nextButton != null)
            {
                nextButton.SetActive(currentSentenceIndex < skeletonDialogue.sentences.Length);

                //reset the buttons visual state
                Button button = nextButton.GetComponent<Button>();
                if (button != null)
                {
                    //force the button to rehighlight by reselecting it
                    button.Select();
                }
            }
        }
        else
        {
            EndDialogue();
        }
        }
        public void EndDialogue()
        {
            isInDialogue = false;
            dialogueBox.SetActive(false);
            currentSentenceIndex = 0;
            if (nextButton != null) nextButton.SetActive(false);
        }
    }

   

