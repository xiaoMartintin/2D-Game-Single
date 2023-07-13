using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AdvancedDialogSO : ScriptableObject
{
    public DialogActors[] actors;

    [Header("Random Actor Info")]
    public string randomActorName;
    public Sprite randomActorPortrait;


    [Header("Dialog")]
    [TextArea]
    public string[] dialog;

    [Tooltip("The words that will be displayed on the button")]
    public string[] optionText;

    public AdvancedDialogSO option0;
    public AdvancedDialogSO option1;
    public AdvancedDialogSO option2;
    public AdvancedDialogSO option3;
}
