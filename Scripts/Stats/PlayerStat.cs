using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem){
        if(newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.RemoveModifier(newItem.armorModifier);
        }
        if(oldItem != null){
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.armorModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        //kill the player
        PlayerManager.instance.KillPlayer();
    }
}
