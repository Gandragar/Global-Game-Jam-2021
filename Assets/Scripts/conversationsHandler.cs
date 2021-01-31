using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class conversationsHandler : MonoBehaviour
{
    [HideInInspector] public int curIndex;
    [HideInInspector] public int playerMoney;
    [HideInInspector] public bool waitCutScene;
    [HideInInspector] public bool finalCutScene;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public List<buttonItemType> itemsButtons = new List<buttonItemType>();
    public Transform hiddenDialoguePos;
    public Transform shownDialoguePos;
    public static conversationsHandler current;

    //private Button curButton;
    private dragItem curItem;

    private void Awake()
    {
        current = this;
    }


    public void nextDialogue()
    {
        curIndex++;
    }
    public void getItem(GameObject item)
    {
        if (curItem != null)
            Destroy(curItem.gameObject);
        curItem = Instantiate(item).GetComponent<dragItem>();

    }
    public void endGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void pay()
    {
        playerMoney -= 700;
        if (playerMoney < 0)
        {
            moneyText.text = 0 + " $";
        }
        else
        {
            moneyText.text = playerMoney + " $";
        }
    }
}
