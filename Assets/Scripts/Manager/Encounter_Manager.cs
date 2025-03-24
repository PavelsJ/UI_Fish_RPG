using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Encounter_Manager : MonoBehaviour
{
    [SerializeField] private FishData noneType;
    
    [Header("Fish_Types")] 
    [SerializeField] private FishData[] fishTypes;
    private FishData nextFishType;
    
    [Header("Fish_Encounter_Types")] 
    [SerializeField] private FishData[] encounterTypes;
    
    [Header("Item_Types")] 
    [SerializeField] private FishData[] itemTypes;
    
    [Header("Trap_Types")] 
    [SerializeField] private FishData[] trapTypes;
    
    [Header("Fish_UI")] 
    [SerializeField] GameObject descriptionTab;
    private bool isReadingPassive = false;

    public void SetupNewEncounter(Fish currentFish, Fish nextFish)
    {
        UpdateFish(currentFish, GenerateFish());
        
        nextFishType = GenerateFish();
        UpdateFish(nextFish, nextFishType);
        
        UpdateDescription(currentFish);
    }

    public void ResetCurrentFish(Fish currentFish)
    {
        UpdateFish(currentFish, nextFishType);
        UpdateDescription(currentFish);
    }
    
    public void ResetNextFish(Fish nextFish)
    {
        nextFishType = GenerateFish();
        UpdateFish(nextFish, nextFishType);
    }
    
    public void ResetFishToNone(Fish currentFish, Fish nextFish)
    {
        UpdateFish(currentFish, noneType);
        UpdateFish(nextFish, noneType);
        
        UpdateDescription(currentFish);
    }

    private void UpdateFish(Fish fish, FishData fishType)
    {
        if (fish != null)
        {
            fish.UpdateFish(fishType);
        }
    }

    private FishData GenerateFish()
    {
        return fishTypes[Random.Range(0, fishTypes.Length)];
    }
    
    public void ShowPassiveDescription()
    {
        isReadingPassive = !isReadingPassive;

        if (descriptionTab != null)
        {
            descriptionTab.SetActive(isReadingPassive);
        }
    }

    private void UpdateDescription(Fish currentFish)
    {
        if (descriptionTab != null)
        {
            TextMeshProUGUI description = descriptionTab.GetComponentInChildren<TextMeshProUGUI>();

            if (currentFish != null)
            {
                string passiveText = "SILLY FISH";
                string passiveDescription = "This fish has no special abilities";

                FishData.PassiveEffect passiveEffect = currentFish.PassiveEffect;

                switch (passiveEffect)
                {
                    case FishData.PassiveEffect.Poison:
                        passiveText = "POISON";
                        passiveDescription = "Deals 1 damage for 3 turns after hit";
                        break;
                    case FishData.PassiveEffect.Aggression:
                        passiveText = "AGGRESSION";
                        passiveDescription = "40% chance to attack twice in one turn";
                        break;
                    case FishData.PassiveEffect.Agility:
                        passiveText = "AGILITY";
                        passiveDescription = "20% chance to dodge incoming damage";
                        break;
                }

                description.text = $"{passiveText}\n{passiveDescription}";
            }
            else
            {
                description.text = "No fish selected.";
            }
        }
    }
}
