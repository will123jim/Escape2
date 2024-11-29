using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CandlePuzzle : MonoBehaviour
{
    [SerializeField] private GameObject lighter;
    private ParticleSystem lighterFlame;

    [SerializeField] private List<GameObject> candles; // Add candle objects in Inspector
    [SerializeField] private List<int> correctOrder; // Define the correct lighting order

    private List<int> currentOrder; // Track the order in which candles are lit
    private bool puzzleSolved = false;

    public AudioSource revealSound; // Reference to the AudioSource
    public TextMeshProUGUI revealText; // Reveal text at completion
    public string puzzleCompletionMessage = "Puzzle solved!";

    private void Start()
    {
        Debug.Log("Initializing puzzle...");
        lighterFlame = lighter.GetComponentInChildren<ParticleSystem>();

        if (lighter == null || lighterFlame == null)
        {
            Debug.LogError("Lighter or lighter flame is not set up properly!");
            return;
        }

        // Initialize the order list
        currentOrder = new List<int>();

        // Ensure all candles start unlit
        foreach (GameObject candle in candles)
        {
            SetCandleLit(candle, false);
        }
        //Deactivate the reveal text
        if (revealText != null)
        {
            revealText.gameObject.SetActive(false);
        }
    }

    public void TryLightCandle(GameObject candle)
    {
        if (puzzleSolved) return; // Do nothing if the puzzle is already solved

        if (lighterFlame != null && lighterFlame.isPlaying) // Check if the flame is on
        {
            Debug.Log("Lighter flame detected near candle");
            LightCandle(candle);
        }
        else
        {
            Debug.Log("Lighter flame is not active");
        }
    }

    private void LightCandle(GameObject candle)
    {
        if (puzzleSolved) return;

        int candleIndex = candles.IndexOf(candle);

        // Prevent relighting the same candle
        if (currentOrder.Contains(candleIndex))
        {
            Debug.Log($"Candle {candleIndex} already lit. Ignoring.");
            return;
        }

        currentOrder.Add(candleIndex);
        SetCandleLit(candle, true);

        Debug.Log($"Candle lit: {candle.name}, Index: {candleIndex}");

        // Check if all candles are lit
        if (currentOrder.Count == correctOrder.Count)
        {
            CheckPuzzleSolution();
        }
    }

    private void CheckPuzzleSolution()
    {
        Debug.Log($"Current Order: {string.Join(", ", currentOrder)}");
        Debug.Log($"Correct Order: {string.Join(", ", correctOrder)}");

        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                Debug.Log("Incorrect order detected. Resetting puzzle.");
                ResetPuzzle();
                return;
            }
        }

        PuzzleComplete();
    }

    private void SetCandleLit(GameObject candle, bool isLit)
    {
        var flame = candle.GetComponentInChildren<ParticleSystem>();
        if (flame != null)
        {
            if (isLit) flame.Play();
            else flame.Stop();
        }
    }

    private void PuzzleComplete()
    {
        puzzleSolved = true;
        Debug.Log("Puzzle solved! The next clue is unlocked.");

        if (revealSound != null)
        {
            revealSound.Play(); // Play the sound when the puzzle is solved
        }
        if (revealText != null)
        {
            revealText.gameObject.SetActive(true);
            revealText.text = puzzleCompletionMessage; // Set the desired message
            revealText.color = Color.green; // Change text color to indicate success
        }
    }

    private void ResetPuzzle()
    {
        Debug.Log("Resetting the puzzle...");

        // Turn off all candle flames
        foreach (GameObject candle in candles)
        {
            SetCandleLit(candle, false);
        }

        // Clear the current order
        currentOrder.Clear();

        Debug.Log("All candles reset.");
    }
}
