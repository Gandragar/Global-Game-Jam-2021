using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragItem : MonoBehaviour
{
    public enum itemType { computador, cabo, faca };
    public itemType item;
    private Color red = new Color(1f, 0.35f, 0.35f, 0.8f);
    private Color normal = new Color(1f, 1f, 1f, 0.8f);

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = red;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        if (Input.GetMouseButtonDown(1))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Cliente"))
        {
            GetComponent<SpriteRenderer>().color = normal;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Cliente"))
        {
            GetComponent<SpriteRenderer>().color = red;
        }
    }
}
