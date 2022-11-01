using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationDict;
    protected override void Start()
    {
        base.Start();
        weaponAnimationDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations item in weaponAnimations)
        {
            weaponAnimationDict.Add(item.weapon, item.clips);
        }
    }
    [System.Serializable]
    public struct WeaponAnimations{
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
