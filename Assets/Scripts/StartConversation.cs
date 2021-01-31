using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartConversation : MonoBehaviour
{
    public string Name;
    public Sprite imageIcon;
    public clientsHandler.itemType itemWanted;
    public bool waitCutScene;
    public bool isCutSceneFinal;
    [TextArea] public string[] dialogue;

    [TextArea] public string correctItemDialogue;
    [TextArea] public string incorrectItemDialogue;

    private GameObject nextButton;
    private GameObject endButton;
    private GameObject endGameButton;
    private GameObject payButton;
    private TextMeshProUGUI dialogueText;
    private int curIndex;
    private float showChatTimer;
    private float counter;
    private bool firstTime;
    [HideInInspector] public bool itemReceived;
    private GameObject dialoguePainel;
    private conversationsHandler gameManager;

    private float delay = 0.02f;
    private string currentText = "";


    private void Start()
    {
        gameManager = conversationsHandler.current;
        gameManager.curIndex = 0;
        curIndex = 0;
        if (waitCutScene)
            gameManager.waitCutScene = true;

        dialoguePainel = GameObject.FindGameObjectWithTag("Dialogue");
        dialoguePainel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Name;
        dialoguePainel.transform.GetChild(1).GetComponent<Image>().sprite = imageIcon;
        dialogueText = dialoguePainel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        nextButton = dialoguePainel.transform.GetChild(3).gameObject;
        endButton = dialoguePainel.transform.GetChild(4).gameObject;
        endGameButton = dialoguePainel.transform.GetChild(5).gameObject;
        payButton = dialoguePainel.transform.GetChild(6).gameObject;

        nextButton.SetActive(false);
        endButton.SetActive(false);
        endGameButton.SetActive(false);
        payButton.SetActive(false);

        dialoguePainel.transform.position = gameManager.hiddenDialoguePos.position;
        showChatTimer = 1f;
        counter = 0f;
        firstTime = true;
        itemReceived = false;
        gameManager.moneyText.text = gameManager.playerMoney + " $";
    }

    private void Update()
    {
        if (firstTime)
        {
            counter += Time.deltaTime;
            if (counter >= showChatTimer && firstTime && !itemReceived)
            {
                StartCoroutine(ShowText(dialogue[gameManager.curIndex]));
                firstTime = false;
            }
        }
        else if (curIndex < gameManager.curIndex && dialogue.Length >= (gameManager.curIndex + 1) && !itemReceived)
        {
            curIndex = gameManager.curIndex;
            StartCoroutine(ShowText(dialogue[gameManager.curIndex]));
        }
    }

    IEnumerator ShowText(string dialogues)
    {
        dialoguePainel.transform.position = gameManager.shownDialoguePos.position;
        if (dialogue.Length <= (gameManager.curIndex + 1))
        {
            nextButton.SetActive(false);
            if (waitCutScene)
                endButton.SetActive(true);
            if (isCutSceneFinal)
            {
                payButton.SetActive(true);
            }
        }
        for (int i = 0; i < dialogues.Length+1; i++)
        {
            currentText = dialogues.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        if (dialogue.Length > (gameManager.curIndex + 1))
            nextButton.SetActive(true);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        dragItem item = collision.gameObject.GetComponent<dragItem>();
        if (collision.transform.CompareTag("pickup") && !itemReceived)
        {
            StopAllCoroutines();
            if (itemWanted == item.item)
            {
                gameManager.playerMoney += 100;
                gameManager.moneyText.text = gameManager.playerMoney + " $";
                StartCoroutine(ShowText(correctItemDialogue));
            }
            else
            {
                StartCoroutine(ShowText(incorrectItemDialogue));
            }
            itemReceived = true;
            endButton.SetActive(true);
            for (int i = 0; i < gameManager.itemsButtons.Count; i++)
            {
                if (item.item == gameManager.itemsButtons[i].item)
                    gameManager.itemsButtons[i].gameObject.SetActive(false);
            }
            Destroy(collision.gameObject);
        }
    }
}
