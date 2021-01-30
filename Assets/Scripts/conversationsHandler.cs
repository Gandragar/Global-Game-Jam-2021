using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conversationsHandler : MonoBehaviour
{
    [HideInInspector] public int curIndex;
    public Transform hiddenDialoguePos;
    public Transform shownDialoguePos;
    public static conversationsHandler current;



    private void Awake()
    {
        current = this;
    }


    public void nextDialogue()
    {
        curIndex++;
    }
}
