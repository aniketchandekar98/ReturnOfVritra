using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp(){
        Debug.Log("Player picked up " + item.name);
        //add item to inventory
        bool wasPickedUp = Inventory.instance.Add(item);
        //Destroy(gameObject);
        if(wasPickedUp){
            Destroy(gameObject);
        }

        
    }    
}
