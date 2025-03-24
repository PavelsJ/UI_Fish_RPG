using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hook_Manager : MonoBehaviour
{
    public enum HookType
    {
        DoubleHook, // setups 2 crit tabs in minigame
        PrecisionHook, // if crit, deals 3 times more dmg instead of 2
        AnchorHook // slows down fish by 40%
    }
    
    [SerializeField] private Button[] hookButtons;
    private Hook currentHook;
    
    private void Start()
    {
        hookButtons[0].onClick.AddListener(() => SelectHook(HookType.DoubleHook));
        hookButtons[1].onClick.AddListener(() => SelectHook(HookType.PrecisionHook));
        hookButtons[2].onClick.AddListener(() => SelectHook(HookType.AnchorHook));

        if (currentHook == null)
        {
            SelectHook(HookType.PrecisionHook);  
        }
    }
    
    public void SelectHook(HookType hookType)
    {
        if (currentHook != null) Destroy(currentHook);
        
        var fishPulling = Fishing_Manager.Instance.GetFishPulling();
        fishPulling.SetReticleCount(hookType == HookType.DoubleHook ? 2 : 1);
        
        currentHook = hookType switch
        {
            HookType.DoubleHook => gameObject.AddComponent<DoubleHook>(),
            HookType.PrecisionHook => gameObject.AddComponent<PrecisionHook>(),
            HookType.AnchorHook => gameObject.AddComponent<AnchorHook>(),
            _ => null
        };

        Debug.Log($"Selected Hook: {currentHook.GetHookName()}");
    }
    
    public Hook GetCurrentHook()
    {
        return currentHook;
    }
}
