using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    // Item data  //
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public string itemDescription;
    public bool isFull;
    public Sprite emptySprite;

    [SerializeField]
    private int maxNumberOfItems;

    // Item Slot  //
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    // Item Description Slot  //
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;



    public GameObject selectedShader;
    public bool thisItemSelected;

    private InverntoryManager inverntoryManager;

    private void Start()
    {
        inverntoryManager = GameObject.Find("Inventory").GetComponent<InverntoryManager>();
    }


    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //check if the slot is already full
        if (isFull)
        {
            return quantity;
        }
        //update
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
            isFull = true;


            //return leftovers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        //update quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left){
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inverntoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;

        //description info show
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage == null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }
    }

    public void OnRightClick()
    {
        
    }






}
