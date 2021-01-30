using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartConversation : MonoBehaviour
{
    public string Name;
    public Sprite imageIcon;
    public enum itemType { computador, cabo, faca };
    public itemType itemWanted;

    [TextArea] public string[] dialogue;

    private GameObject nextButton;
    private TextMeshProUGUI dialogueText;
    private int curIndex;
    private float showChatTimer;
    private float counter;
    private bool firstTime;
    private GameObject dialoguePainel;
    private conversationsHandler index;

    private float delay = 0.02f;
    private string currentText = "";


    private void Start()
    {
        index = conversationsHandler.current;
        index.curIndex = 0;
        curIndex = 0;
        dialoguePainel = GameObject.FindGameObjectWithTag("Dialogue");

        dialoguePainel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Name;
        dialoguePainel.transform.GetChild(1).GetComponent<Image>().sprite = imageIcon;
        dialogueText = dialoguePainel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        nextButton = dialoguePainel.transform.GetChild(3).gameObject;
        nextButton.SetActive(false);

        dialoguePainel.transform.position = index.hiddenDialoguePos.position;
        showChatTimer = 1f;
        counter = 0f;
        firstTime = true;
    }

    private void Update()
    {
        if (firstTime)
        {
            counter += Time.deltaTime;
            if (counter >= showChatTimer && firstTime)
            {
                dialoguePainel.transform.position = index.shownDialoguePos.position;
                StartCoroutine(ShowText(dialogue[index.curIndex]));
                firstTime = false;
            }
        }
        else if (curIndex < index.curIndex && dialogue.Length >= (index.curIndex + 1))
        {
            curIndex = index.curIndex;
            StartCoroutine(ShowText(dialogue[index.curIndex]));
        }
    }

    IEnumerator ShowText(string dialogues)
    {
        if (dialogue.Length <= (index.curIndex + 1))
            nextButton.SetActive(false);
        for (int i = 0; i < dialogues.Length; i++)
        {
            currentText = dialogues.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        if (dialogue.Length > (index.curIndex + 1))
            nextButton.SetActive(true);

    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.transform)
    //}
}
