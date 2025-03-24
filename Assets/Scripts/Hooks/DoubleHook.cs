using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHook : Hook
{
    public override int GetDamage(int baseDamage, bool isCrit)
    {
        return isCrit ? baseDamage * 2 : baseDamage;
    }

    public override void LureEffect(Character currentFish, Character nextFish, Fish_Pulling fishPulling)
    {
        fishPulling.SetReticleCount(2);
    }
}
