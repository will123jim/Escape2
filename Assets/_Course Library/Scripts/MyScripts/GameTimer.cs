using UnityEngine;
using UnityEngine.SceneManagement; // needed to reload the scene
using TMPro; // import if using TextMeshPro

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 3600f; // 1 hour in secondes
    private bool timerIsRunning = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public TMP_Text timerText; // assign this in the inspector
    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Count down
                DisplayTime(timeRemaining);
            }
            else
            {
            timeRemaining = 0;
            timerIsRunning = false;
            ResetGame(); // Call the game reset function
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void ResetGame()
    {
        //REload the current scene to reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
