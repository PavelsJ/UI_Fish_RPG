using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private string charName;
    internal bool isDead = false;
    
    [SerializeField] private int strength = 2;
    [SerializeField] private int health = 100;
    
    [SerializeField] private Image charImage;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] internal TextMeshProUGUI statsText;
    public string CharName
    {
        get => charName;
        set => charName = value;
    }

    public int Strength
    {
        get => strength;
        set => strength = value;
    }
    
    public int Health
    {
        get => health;
        internal set => health = Mathf.Max(0, value);
    }
    
    public virtual void Attack(Character target, int damage)
    {
        target.TakeDamage(Strength);
        Debug.Log($"{charName} attacks {target.CharName}");
        
        UpdateStatsUI();
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{charName} takes {damage} damage / {Health} health left.");
        
        UpdateStatsUI();
    }
    
    internal void UpdateCharUI(Sprite fishSprite)
    {
        nameText.text = charName;
        charImage.sprite = fishSprite;
       
        UpdateStatsUI();
    }

    protected virtual void UpdateStatsUI()
    {
        statsText.text = $"Health: {Health} Strength: {Strength}";
    }
}
