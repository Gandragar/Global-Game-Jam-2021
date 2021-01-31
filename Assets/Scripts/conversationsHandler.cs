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
    public Image fadePanel;
    public Image credits;
    public GameObject Menu;
    public static conversationsHandler current;
    private float fadeSpeed = 0.01f;
    private bool isGameEnded = true;
    //private Button curButton;
    private dragItem curItem;

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        if(isGameEnded && fadePanel.color.a<=1)
        {
            print("ff");
            Color tempColor = fadePanel.color;
            tempColor.a = fadeSpeed;
            fadePanel.color = tempColor;
        }
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
        Fade();
        isGameEnded = true;
    }
    public void Fade()
    {
        Menu.SetActive(true);
    }

    public void goMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
