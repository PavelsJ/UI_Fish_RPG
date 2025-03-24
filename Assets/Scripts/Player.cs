using System;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private TextMeshProUGUI passiveEffect;
    private int poisonCounter = 0;

    private void Start()
    {
        UpdateStatsUI();
    }

    public override void Attack(Character target, int damage)
    {
        ApplyPoisonEffect();
        
        target.TakeDamage(damage);
        
        UpdateStatsUI();
    }
    
    private void ApplyPoisonEffect()
    {
        if (poisonCounter > 0)
        {
            TakeDamage(1);
            
            poisonCounter--;
            passiveEffect.text = $"Poisoned: {poisonCounter}";
        }
        else
        {
            passiveEffect.text = "...";
        }
    }
    
    public void TriggerPoison()
    {
        poisonCounter = 3;
        passiveEffect.text = $"Poisoned: {poisonCounter}";
    }

    protected override void UpdateStatsUI()
    {
        var hookManager = Fishing_Manager.Instance.GetHookManager();
        string currentHook = hookManager.GetCurrentHook()?.GetHookName() ?? "None";
        
        statsText.text = $"Health: {Health} Strength: {Strength} (Crit: {Strength * 2}) Hook: {currentHook}";
    }
}
