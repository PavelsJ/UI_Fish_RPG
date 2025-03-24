using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecisionHook : Hook
{
    public override int GetDamage(int baseDamage, bool isCrit)
    {
        return isCrit ? baseDamage * 3 : baseDamage;  
    }

    public override void LureEffect(Character currentFish, Character nextFish, Fish_Pulling fishPulling)
    {
        // none
    }
}
