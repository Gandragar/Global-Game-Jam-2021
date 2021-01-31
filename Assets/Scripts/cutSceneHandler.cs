using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutSceneHandler : MonoBehaviour
{
    public GameObject Gerson;
    [HideInInspector] public bool nextCut;
    //[HideInInspector] public int cutSceneIndex;
    [HideInInspector] public GameObject curCut;
    // Start is called before the first frame update
    void Start()
    {
        Gerson.transform.GetChild(0).gameObject.SetActive(true);
        curCut = Gerson.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextCut && Gerson.transform.childCount >= 1)
        {
            Gerson.transform.GetChild(0).gameObject.SetActive(true);
            curCut = Gerson.transform.GetChild(0).gameObject;
        }
    }
}
