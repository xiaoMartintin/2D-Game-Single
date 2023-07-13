using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogBoxText;
    public string signText;
    private bool isPlayerInSign = false;

    //public GameObject follow;

    // Start is called before the first frame update
    void Start()
    {
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }

        dialogBoxText.text = signText;

    }

    // Update is called once per frame
    void Update()
    {
        //if(isPlayerInSign)
        ////if (Input.GetKeyDown(KeyCode.E) && isPlayerInSign)
        //{
        //    //if (dialogBox.activeInHierarchy)
        //    //{
        //    //    dialogBox.SetActive(false);
        //    //}
        //    else
        //    {
        //        dialogBoxText.text = signText;
        //        dialogBox.SetActive(true);
        //    }
        //}

        //Vector2 DialogBoxFollow = Camera.main.WorldToScreenPoint(this.transform.position);
        //DialogBoxFollow.y = DialogBoxFollow.y + 45;
        //this.GetComponent<Canvas>().GetComponent<DialogBoxSign>().GetComponent<RectTransform>().position = DialogBoxFollow;
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
            dialogBox.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
            dialogBox.SetActive(false);
        }
        
    }
}
