using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSystem dialogueSystem;// assignt the dialgouesystem script in the inspector
    public float detectionRadius = 2f; // adjust the radius as needed

    private bool dialogueStarted = false;

    void Update()
    {
        if (!dialogueStarted)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player")) // ensure xr rig has "player" tag
                {
                    Debug.Log("Player detected within range");
                    dialogueSystem.StartDialogue();
                    dialogueStarted = true; // prevent repeated triggers
                    break;
                }
            }
        }
    }

   
}
