using UnityEngine;

[CreateAssetMenu(fileName = "FishType_0", menuName = "ScriptableObjects/FishData", order = 1)]
public class FishData : ScriptableObject
{
    public enum PassiveEffect
    {
        None, // default
        Poison, // deals 1 damage 3 turns after hit
        Aggression, // deals 2 hits in 1 turn (chance 40%)
        Agility // do not take any dmg (chance 20%)
    }
    
    public enum MiniGameType
    {
        Fish, // pull fish
        Invasion, // secure fish
        Item, // pull item
        Trap // avoid trap
    }

    public int fishRarity = 1;
    public string fishName = "fish_name";
    public Sprite fishSprite;
    
    public int fishStrength = 2;
    public int fishHealth = 2;
    
    public PassiveEffect passiveEffect;
    public MiniGameType miniGameType;
}
