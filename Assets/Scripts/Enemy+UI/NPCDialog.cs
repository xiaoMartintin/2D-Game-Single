using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public AdvancedDialogSO[] conversation;

    private Transform player;
    private SpriteRenderer speechBubbleRenderer;

    private AdvancedDialogManager advancedDialogManager;


    private bool dialogInitiated;
    // Start is called before the first frame update
    void Start()
    {
        advancedDialogManager = GameObject.Find("DialogManager").GetComponent<AdvancedDialogManager>();
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player" && !dialogInitiated)
        {
            speechBubbleRenderer.enabled = true;
            player = other.gameObject.GetComponent<Transform>();
            if (player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip();
            }
            else if (player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }

            advancedDialogManager.InitiateDialog(this);
            dialogInitiated = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            speechBubbleRenderer.enabled = false;

            advancedDialogManager.TurnOffDialog();
            dialogInitiated = false;
        }

    }


    private void Flip()
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }

    
}