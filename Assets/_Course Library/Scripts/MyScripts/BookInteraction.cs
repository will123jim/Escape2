using UnityEngine;


public class BookInteraction : MonoBehaviour
{
    public Animator bookAnimator; //Assign the animator component in inspector
    private bool isBookOpen = false; // track if the book is already open
      
      void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isBookOpen)
        {
            Debug.Log("Player entered trigger");
            OpenBook();
        }
    }

    void OpenBook()
    {
        Debug.Log("Opening book...");//logs when open book is called
        isBookOpen = true; // mark the book as open
        bookAnimator.SetTrigger("Open"); // play opening animation
        Invoke("SetIdleOpen", 0.5f); // delay to ensure the open animation completes before moving to idelopen
    }
    void SetIdleOpen()
    {
        Debug.Log("setting idleopen state");
        bookAnimator.SetBool("isOpen", true); // move to the idle open state
    }
    // to closee book
   public void CloseBook()
    {
        isBookOpen = false;
        bookAnimator.SetBool("isOpen", false); //allow transition back to closed stat if needed
    }
}

