using UnityEngine;

public class CandleInteraction : MonoBehaviour
{
    private CandlePuzzle candlePuzzle; // reference to the main CandlePuzzle script
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find the CandlePuzzle script in the scene
        candlePuzzle = FindObjectOfType<CandlePuzzle>();
    }

    void OnTriggerEnter(Collider other)
    {
    // check if the lighter entered the trigger
    if (other.gameObject.CompareTag("Lighter"))
    {
        Debug.Log("Lighter detected near candle!");
        candlePuzzle.TryLightCandle(gameObject); // Pass this candle to TryLightCandle
    }    
    }
}
