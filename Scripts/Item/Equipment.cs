using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    // public EquipmentMeshRegion[] coveredMeshRegions;
    public int armorModifier;
    public int damageModifier;
    public int healthModifier;

    public override void Use()
    {
        base.Use();
        //equip the item
        EquipmentManager.instance.Equip(this);
        //remove it from the inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {Weapon, HealthPotion}
// public enum EquipmentMeshRegion {Legs, Arms, Torso} //correspondes to body blend shapes