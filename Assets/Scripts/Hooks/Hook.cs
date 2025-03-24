using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hook : MonoBehaviour
{
    public abstract int GetDamage(int baseDamage, bool isCrit);
    public abstract void LureEffect(Character currentFish, Character nextFish, Fish_Pulling fishPulling);
    
    public virtual string GetHookName()
    {
        return GetType().Name.Replace("Hook", "");
    }
}
