﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class conversationsHandler : MonoBehaviour
{
    [HideInInspector] public int curIndex;
    [HideInInspector] public int playerMoney;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public List<buttonItemType> itemsButtons = new List<buttonItemType>();
    public Transform hiddenDialoguePos;
    public Transform shownDialoguePos;
    public static conversationsHandler current;

    private Button curButton;
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
}
