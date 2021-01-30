using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientsHandler : MonoBehaviour
{
    public Transform spawnPosition;
    public List<GameObject> clientsPrefabs = new List<GameObject>();

    private bool isOccupied;


    // Update is called once per frame
    void Update()
    {
        if (!isOccupied)
        {
            spawnClient();
        }
    }

    private void spawnClient()
    {
        int index = Random.Range(0, clientsPrefabs.Count);
        Instantiate(clientsPrefabs[index], spawnPosition.position, Quaternion.identity);
        isOccupied = true;
    }
}
