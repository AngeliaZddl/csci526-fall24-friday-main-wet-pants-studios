using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InverntoryManager inverntoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inverntoryManager = GameObject.Find("Inventory").GetComponent<InverntoryManager>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            int leftOverItems = inverntoryManager.AddItem(itemName, quantity,sprite,itemDescription);
            if (leftOverItems <= 0)
                Destroy(gameObject);
            else
                quantity = leftOverItems;

        }
    }
}
