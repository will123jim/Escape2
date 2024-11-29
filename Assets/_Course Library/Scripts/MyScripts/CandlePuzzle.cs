using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CandlePuzzle : MonoBehaviour
{
    [SerializeField] private GameObject lighter;
private ParticleSystem lighterFlame;

    [SerializeField] private List<GameObject> candles;// add candle objects her in inspector
    [SerializeField] private List<int> correctOrder;// define the correct lighting order

private List<bool> candlesLit; // track which candles are lit
   
    private bool puzzleSolved = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<int> currentOrder; // track the order in which candles are lit
    public AudioSource revealSound; // reference to the audiosource
    public TextMeshProUGUI revealText; // reveal text at completion
    public string puzzleCompletionMessage = "0";
    private void Start()
    {
        Debug.Log ("Lighter is active: " + lighter.activeInHierarchy);
        lighterFlame = lighter.GetComponentInChildren<ParticleSystem>();
        //Initialize candles as unlit (assuming each candle has a light or particlesystem component
        Debug.Log("Lighter initialized: " +(lighter != null));
        Debug.Log("Lighter flame initialized: " + ( lighterFlame != null));

        // Initialize the order list
        currentOrder = new List<int>();
    

        foreach (GameObject candle in candles)
        {
            SetCandleLit(candle, false);
        }
}

    public void TryLightCandle(GameObject candle)
    {
       if (lighterFlame.isPlaying)// check if the flame is on
    {
        Debug.Log("Lighter flame detected near candle");
        LightCandle(candle); // call your method to light
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

    // Prvent relighting the same candle
    if (currentOrder.Contains(candleIndex)) return;

    currentOrder.Add(candleIndex); 
        SetCandleLit(candle, true);
        
        Debug.Log($"Candle lit: {candle.name}, Index: {candleIndex}");

//check if all candles are lit
        if (currentOrder.Count == correctOrder.Count)
        {
            CheckPuzzleSolution();
        }
    }
    private void CheckPuzzleSolution()
    {
        for (int i =0; i < correctOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
           
            {
                Debug.Log("Incorrect order. Resetting puzzle");
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
            revealSound.Play(); // play the sound when the puzzle is solved
        }
        if (revealText != null)
        {
            revealText.text = "0";
            revealText.color = Color.green; //set the desired message
            
        }
        }
        private void ResetPuzzle()
            {
                Debug.Log("Resetting the puzzle");
              
                // Clear the current order and reset all candles
                currentOrder.Clear();

                foreach (GameObject candle in candles)
                {
                    SetCandleLit(candle, false);
                }
            }
}
        
    