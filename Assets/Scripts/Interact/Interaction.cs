using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactDistance = 5f;
    public DialoguePlay dialoguePlay;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, interactDistance);

            if(hit.collider !=null)
            {
                Dialogue dialogue = hit.collider.GetComponent<Dialogue>();
                if(dialogue != null) 
                {
                    dialoguePlay.StartDialogue(dialogue);
                }
            }
        }
    }
}
