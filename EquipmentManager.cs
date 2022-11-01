using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake(){
        instance = this;
    }
    #endregion

//     public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    public delegate void OnEquipmentchanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentchanged onEquipmentChanged;
    Inventory inventory;

    void Start(){
        //inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        //equipDefaultItems();
    }

     public void Equip(Equipment newItem){
        int slotIndex = (int)newItem.equipSlot;
        //Debug.Log("Slot Index: " + slotIndex);        
        Equipment oldItem = Unequip(slotIndex);
        if(currentEquipment[slotIndex] != null){
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        if(onEquipmentChanged != null){
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        // setEquipmentBlendShapes(newItem, 100);
        currentEquipment[slotIndex] = newItem;
        // SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        // //newMesh.transform.parent = targetMesh.transform;
        // //targetMesh.transform.SetParent(newMesh.transform);
        // newMesh.transform.parent = newMesh.transform;

        // newMesh.bones = targetMesh.bones;
        // newMesh.rootBone = targetMesh.rootBone;
        // currentMeshes[slotIndex] = newMesh;
     }

    public Equipment Unequip(int slotIndex){
        //Debug.Log("Current equipment slot: " + currentEquipment[slotIndex]);
        if(currentEquipment[slotIndex] != null){
            // if(currentMeshes[slotIndex] != null){
            //     Destroy(currentMeshes[slotIndex].gameObject);
            // }
            Equipment oldItem = currentEquipment[slotIndex];
            //setEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            if(onEquipmentChanged != null){
            onEquipmentChanged.Invoke(null, oldItem);
        }
            return oldItem;
        }
        return null;
    }

    public void UnequipAll(){
        //Debug.Log("Length "+currentEquipment.Length);
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        //equipDefaultItems();
    }

//     void setEquipmentBlendShapes(Equipment item, int weight){
//         foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
//         {
//             targetMesh.SetBlendShapeWeight((int)blendShape, weight);
//         }
//     }

//     void equipDefaultItems(){
//         foreach (Equipment item in defaultItems)
//         {
//             Equip(item);
//         }
//     }

    void Update(){
        //Debug.Log("aaaaa ");
        if(Input.GetKeyDown(KeyCode.U)){
            //Debug.Log("aaaaa ");
            UnequipAll();
        }
    }
}


