using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaintingPuzzleManager : MonoBehaviour
{
    [SerializeField] private List<SlotBehavior> slots; // Assign all slots in the Inspector
    [SerializeField] private TextMeshProUGUI clueText; // TextMeshPro for clue
    [SerializeField] private AudioSource revealSound; // AudioSource for sound

    private void Start()
    {
        if (clueText != null)
        {
            clueText.gameObject.SetActive(false); // Hide clue initially
        }
    }

    public void CheckPuzzle()
    {
        Debug.Log("Checking puzzle...");

        bool allCorrect = true;

        foreach (SlotBehavior slot in slots)
        {
            Debug.Log($"Slot {slot.gameObject.name} isCorrect: {slot.isCorrect}");

            if (!slot.isCorrect)
            {
                allCorrect = false;
                Debug.Log($"Slot {slot.gameObject.name} is not correctly filled.");
            }
        }

        if (allCorrect)
        {
            Debug.Log("All slots are correctly filled! Puzzle solved.");
            RevealClue();
        }
        else
        {
            Debug.Log("Puzzle not solved yet.");
        }
    }

    private void RevealClue()
    {
        Debug.Log("Puzzle solved! Revealing clue.");

        if (revealSound != null)
        {
            revealSound.Play();
            Debug.Log("Reveal sound played.");
        }

        if (clueText != null)
        {
            clueText.gameObject.SetActive(true);
            Debug.Log("Clue text revealed.");
        }
    }
}
