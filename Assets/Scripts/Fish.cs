using UnityEngine;

public class Fish : Character
{
    public FishData.PassiveEffect PassiveEffect { get; private set; }
    
    public override void Attack(Character target, int damage)
    {
        switch (PassiveEffect)
        {
            case FishData.PassiveEffect.Aggression:
                if (Random.value <= 0.4f) // 40% chance
                {
                    target.TakeDamage(damage);
                    Debug.Log($"{CharName} strikes again due to Aggression");
                }
                break;
            case FishData.PassiveEffect.Poison:
                Player player = target.GetComponent<Player>();
                if (player != null)
                {
                    player.TriggerPoison();
                    Debug.Log($"{CharName} poisons {player.CharName}!");
                }
                break;
        }
        
        target.TakeDamage(damage);
        
        UpdateStatsUI();
    }
    
    public override void TakeDamage(int damage)
    {
        if (PassiveEffect == FishData.PassiveEffect.Agility && Random.value <= 0.2f) // 20% chance
        {
            Debug.Log($"{CharName} dodges the attack due to Agility");
            return; 
        }
        
        base.TakeDamage(damage);
    }

    public void UpdateFish(FishData selectedFishData)
    {
        CharName = selectedFishData.fishName;
        
        UpdateStats(selectedFishData);
        
        UpdateCharUI(selectedFishData.fishSprite);
    }

    private void UpdateStats(FishData selectedFishData)
    {
        Health = selectedFishData.fishHealth;
        Strength = selectedFishData.fishStrength;
        PassiveEffect = selectedFishData.passiveEffect;
    }

    protected override void UpdateStatsUI()
    {
        string passiveText = PassiveEffect != FishData.PassiveEffect.None ? PassiveEffect.ToString() : "None";
        statsText.text = $"Health: {Health} \nStrength: {Strength} \nPassive: {passiveText}";
    }
}
