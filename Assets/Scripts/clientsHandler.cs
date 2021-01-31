using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class clientsHandler : MonoBehaviour
{

    public Transform spawnPosition;
    public List<GameObject> clientsPrefabs = new List<GameObject>();
    public int clientsPerDay;
    public int numberOfDays;
    public enum itemType { olhos, computador, faca, vassoura, pendrive, peixe, necro, mala, jujuba, guardachuva, garrafa, chave, carteira, carregador, cano };

    private GameObject curClient;
    private List<GameObject> dayClients = new List<GameObject>();
    private int curClientNumber;
    private int curDay;
    private int itensQuantity;
    private int curItensQuantity;
    private List<int> uniqueIndexes = new List<int>();
    private conversationsHandler gameManager;
    private cutSceneHandler cutSceneHandle;
    private void Start()
    {
        cutSceneHandle = GetComponent<cutSceneHandler>();
        gameManager = conversationsHandler.current;
        curClientNumber = 9;
        itensQuantity = 6;
        curItensQuantity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (curClient == null && !gameManager.waitCutScene)
        {
            spawnClient();
        }else if (gameManager.waitCutScene)
        {
            curClient = cutSceneHandle.curCut;
        }

    }

    private void spawnClient()
    {
        if (curClientNumber < clientsPerDay)
        {
            //print("qual cliente na fila " + curClientNumber);
            //print("quantos na lista " + dayClients.Count);
            curClient = Instantiate(dayClients[curClientNumber], spawnPosition.position, Quaternion.identity);
            curClientNumber++;
        }
        else
        {
            if (curDay < numberOfDays)
            {
                print("proximo dia");
                curClientNumber = 0;
                fillDayClientsList();
                fillInventory();
                curDay++;
                gameManager.dayText.text = "Dia: " + curDay;
            }
            else
            {
                Destroy(curClient);
                clearInventory();
                GetComponent<cutSceneHandler>().nextCut = true;
            }
        }
    }
    public void destroyClient()
    {
        if (gameManager.waitCutScene)
        {
            gameManager.waitCutScene=false;
        }
        Destroy(curClient);
    }
    private void fillInventory()
    {
        curItensQuantity = 0;
        foreach (buttonItemType item in gameManager.itemsButtons)
        {
            item.gameObject.SetActive(false);
            foreach (GameObject dayClient in dayClients)
            {
                if (dayClient.GetComponent<StartConversation>().itemWanted == item.item)
                {
                    curItensQuantity++;
                    item.gameObject.SetActive(true);
                    //print(curItensQuantity);
                }
            }
        }
        for (; curItensQuantity < itensQuantity;)
        {
            int indexx = Random.Range(0, gameManager.itemsButtons.Count);
            if (!gameManager.itemsButtons[indexx].gameObject.activeSelf)
            {
                gameManager.itemsButtons[indexx].gameObject.SetActive(true);
                curItensQuantity++;
            }
        }
    }
    private void clearInventory()
    {
        foreach (buttonItemType item in gameManager.itemsButtons)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void fillDayClientsList()
    {
        if (dayClients != null)
            dayClients.Clear();
        if (uniqueIndexes != null)
            uniqueIndexes.Clear();

        generateUniqueIndex();
        for (int i = 0; i < uniqueIndexes.Count; i++)
        {
            dayClients.Add(clientsPrefabs[uniqueIndexes[i]]);
            //print("foi");
        }
    }

    public void generateUniqueIndex()
    {
        for (int i = 0; i < clientsPerDay; i++)
        {
            int numToAdd = Random.Range(0, clientsPrefabs.Count);
            while (uniqueIndexes.Contains(numToAdd))
            {
                numToAdd = Random.Range(0, clientsPrefabs.Count);
            }
            uniqueIndexes.Add(numToAdd);
        }
    }
    public void pay()
    {
        StartConversation conv = cutSceneHandle.curCut.GetComponent<StartConversation>();
        gameManager.playerMoney -= 700;
        if (gameManager.playerMoney < 0)
        {
            print("pagou mal");
            gameManager.moneyText.text = 0 + " $";
            StartCoroutine(conv.ShowText(conv.incorrectItemDialogue));
        }
        else
        {
            print("pagou bem");
            gameManager.moneyText.text = gameManager.playerMoney + " $";
            StartCoroutine(conv.ShowText(conv.correctItemDialogue));
        }
        conv.payButton.SetActive(false);
        conv.endGameButton.SetActive(true);
    }
}
