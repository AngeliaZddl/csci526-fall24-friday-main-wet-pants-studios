using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverntoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActived;
    public ItemSlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //close bp
        if (Input.GetKeyDown(KeyCode.B) && menuActived)
        {
            //time release
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActived = false;
        }
        //open bp
        else if (Input.GetKeyDown(KeyCode.B) && !menuActived)
        {
            //time stop
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActived = true;

        }

    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //Debug.Log("Itemname: " + itemName + ", quantity: " + quantity + ", itemSprite: " + itemSprite);
        for(int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity==0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, quantity, itemSprite, itemDescription);
                }

                return leftOverItems;

            }
        }
        return quantity;

    }

    public bool itemCheck(string itemName, int quantity)
    {
        //Debug.Log("Itemname: " + itemName + ", quantity: " + quantity + ", itemSprite: " + itemSprite);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemName == itemName && itemSlot[i].quantity == quantity)
            {
                return true;
            }
        }
        return false;
    }

    public void DeselectAllSlots()
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
