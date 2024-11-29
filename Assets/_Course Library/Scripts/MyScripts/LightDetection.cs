using UnityEngine;
using TMPro; // Import textmeshpro if using it
public class LightDetection : MonoBehaviour
{
    public Light flashlight; // reference to the flashlight or spotlight
    public TMP_Text targetText; // reference to  the TextMeshPro text obejct
    public GameObject book; // Reference to the book object
    public GameObject bookCover; // reference to the book cover object
    public float detectionRange = 10f; //Maximum distance to detect the text
    public AudioSource revealSound; // Reference to the audiosource for sound effect
    public bool lighterRevealed = false; //track if the lighter is revealed
    public GameObject lighter; //reference to the lighter object
    private bool textVisible = false;
    private bool bookRevealed = false; // track if the cover has been removed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (targetText != null)
        {
            targetText.gameObject.SetActive(false); //hide the text initially
        }
        if (book != null)
        {
            book.SetActive(true); // hide the book initially
        }
        if (lighter != null)
        {
            lighter.SetActive(false); //hide the lighter initially
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (flashlight != null && flashlight.enabled) //check if flashlight is on
        {
            Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * detectionRange, Color.green);// Visualize the raycast in the scene

            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                if (!textVisible && targetText != null)// check for tag
                {
                    targetText.gameObject.SetActive(true);
                    textVisible = true;

                }
                if (!bookRevealed)// hide the cover when the player interacts
                {
                    RevealBook(); // Call the revealbook method
                }
                if (!lighterRevealed) // Reveal the lighter when the player interacts
                {
                    RevealLighter(); //call the reveal lighter method
                }
            }
            else if (textVisible)
            {

                HideText();
            }
        }

        else if (textVisible)
        {
            HideText();
        }

    }

    private void HideText()
    {
        if (targetText != null)
        {
            targetText.gameObject.SetActive(false); // hide the text
            textVisible = false;
        }
    }
    private void RevealBook()
    {
        if (book != null)
        {
            bookCover.SetActive(false); // hide the book cover
            Debug.Log("Book cover hidden");
        }
        if (revealSound != null)
        {
            revealSound.Play(); // Play the sound when the book is revealed
        }
        bookRevealed = true; // ensure the book doesnt keep reappearing
    }
    private void RevealLighter()
    {
        if (lighter != null)
        {
            lighter.SetActive(true); //reveal the lighter
            Debug.Log("Lighter revealed");
        }
        lighterRevealed = true; //ensure the lighter doesnt keep reappearing
    }
}