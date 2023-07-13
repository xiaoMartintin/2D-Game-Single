using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (Button.activeInHierarchy)
        {
            Button.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(Button.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            talkUI.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Button.SetActive(true);
        }


        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Button.SetActive(false);
        }
    }


  


}
