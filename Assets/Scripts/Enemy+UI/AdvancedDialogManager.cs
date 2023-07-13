using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedDialogManager : MonoBehaviour
{
    private AdvancedDialogSO currentConversation;
    private int stepNum;

    private GameObject dialogCanvas;
    //private Text actor;
    public Text actor;
    private Image portrait;
    //private Text dialogText;
    public Text dialogText;
    private bool dialogActivated;

    private string currentSpeaker;
    private Sprite currentPortrait;

    public ActorSO[] actorSO;

    private GameObject[] optionButton;
    private Text[] optionButtonText;
    private GameObject optionsPanel;

    private PlayerController player;

    [SerializeField]
    private float typingSpeed = 0.02f;
    private Coroutine typeWriterRoutine;
    private bool canContinueText = true;

    // Start is called before the first frame update
    void Start()
    {
        //stepNum = 0;
        //find player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Find buttons;
        optionButton = GameObject.FindGameObjectsWithTag("OptionButton");
        optionsPanel = GameObject.Find("OptionPanel");
        optionsPanel.SetActive(false);
        //Find the Text on the buttons
        optionButtonText = new Text[optionButton.Length];


        for(int i = 0; i < optionButton.Length; i++)
        {
            optionButtonText[i] = optionButton[i].GetComponentInChildren<Text>();
        }

        for(int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(false);
        }


        dialogCanvas = GameObject.Find("DialogCanvas");
        //actor = GameObject.Find("ActorText").GetComponent<Text>();
        portrait = GameObject.Find("Portrait").GetComponent<Image>();
        //dialogText = GameObject.Find("DialogText").GetComponent<Text>();

        dialogCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if(dialogActivated && Input.GetButtonDown("Interact") && canContinueText)
        {
            //freeze player
            player.enabled = false;
            if (stepNum >= currentConversation.actors.Length)
            {
                TurnOffDialog();
            }
            else
            {
                PlayDialog();
               
            }

        } 
    }

    void PlayDialog()
    {
        //random
        if (currentConversation.actors[stepNum] == DialogActors.Random)
        {
            SetActorInfo(false);
        }
        //recurring
        else
            SetActorInfo(true);


        actor.text = currentSpeaker;
        portrait.sprite = currentPortrait;

        //If there's a branch
        if (currentConversation.actors[stepNum] == DialogActors.Branch)
        {
            for (int i = 0; i < currentConversation.optionText.Length; i++)
            {
                if (currentConversation.optionText[i] == null)
                {
                    optionButton[i].SetActive(false);
                }
                else
                {
                    optionButtonText[i].text = currentConversation.optionText[i];
                    optionButton[i].SetActive(true);
                }

                //set the first button to be auto-selected
                optionButton[0].GetComponent<Button>().Select();
            }
        }

        if(typeWriterRoutine != null)
        {
            StopCoroutine(typeWriterRoutine);
        }

        if(stepNum < currentConversation.dialog.Length)
        {
            typeWriterRoutine = StartCoroutine(TypeWriterEffect(dialogText.text = currentConversation.dialog[stepNum]));
        }
        else
        {
            optionsPanel.SetActive(true);
            //Debug.Log("stepNum: " + stepNum + "currentConversation.dialog.Length: " + currentConversation.dialog.Length);

        }


        dialogCanvas.SetActive(true);
        stepNum += 1;
    }

    void SetActorInfo(bool recurring)
    {
        if (recurring)
        {
            for(int i = 0; i < actorSO.Length; i++)
            {
                if (actorSO[i].name == currentConversation.actors[stepNum].ToString())
                {
                    currentSpeaker = actorSO[i].actorName;
                    currentPortrait = actorSO[i].actorPortrait;
                }
            }
        }
        else
        {
           currentSpeaker = currentConversation.randomActorName;
          currentPortrait = currentConversation.randomActorPortrait;
        }
    }

    public void Option(int optionNum)
    {
        foreach (GameObject buttons in optionButton)
        {
            buttons.SetActive(false);
        }
        switch(optionNum)
        {
            case 0:
                currentConversation = currentConversation.option0;
                break;
            case 1:
                currentConversation = currentConversation.option1;
                break;
            case 2:
                currentConversation = currentConversation.option2;
                break;
            case 3:
                currentConversation = currentConversation.option3;
                break;
        }

        stepNum = 0;
        
    }
    

    private IEnumerator TypeWriterEffect(string line)
    {
        dialogText.text = "";
        canContinueText = false;
        bool addingRichTextTag = true;
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in line.ToCharArray())
        {
            if(Input.GetButtonDown("Interact"))
            {
                dialogText.text = line;
                break;
            }

            //check if use rich text
            if(letter == '<' || addingRichTextTag)
            {
                addingRichTextTag = true;
                dialogText.text += letter;
                if (letter == '>')
                    addingRichTextTag = false;
            }
            else
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }
        canContinueText = true;
    }
    public void InitiateDialog(NPCDialog npcDialog)
    {
        currentConversation = npcDialog.conversation[0];
        //Debug.Log("Starting conversation: " + currentConversation);
        dialogActivated = true;
        dialogCanvas.SetActive(true);
    }

    public void TurnOffDialog()
    {

        stepNum = 0;
        dialogActivated = false;
        optionsPanel.SetActive(false);
        dialogCanvas.SetActive(false);

        player.enabled = true;
    }
}



public enum DialogActors
{
    Williard,
    Martin,
    Random,
    Branch
};