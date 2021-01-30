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
    private conversationsHandler gameManager;
    private void Start()
    {
        gameManager = conversationsHandler.current;
        curClientNumber = 9;
        itensQuantity = 6;
        curItensQuantity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (curClient==null)
        {
            spawnClient();
        }
    }

    private void spawnClient()
    {
        if (curClientNumber < clientsPerDay)
        {
            print("quantos clientes atuais" + curClientNumber);
            print("quantos na lista" + dayClients.Count);
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
                //fillInventory();
                curDay++;
            }
            else
            {
                print("fim de jogo");
            }
        }
    }
    public void destroyClient()
    {
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
                }
            }
        }
        for (int i = curItensQuantity; i < itensQuantity; i++)
        {
            int indexx = Random.Range(0, gameManager.itemsButtons.Count);
            if (!gameManager.itemsButtons[indexx].gameObject.activeSelf)
            {
                gameManager.itemsButtons[indexx].gameObject.SetActive(true);
                //curItensQuantity++;
            }
        }
    }
    private void fillDayClientsList()
    {
        for (int i = 0; i < clientsPerDay; i++)
        {
            if (dayClients != null)
                dayClients.Clear();
            print("foi");
            int index = Random.Range(0, clientsPrefabs.Count-1);
            dayClients.Add(clientsPrefabs[index]);
            print("foi");
        }
    }
}
