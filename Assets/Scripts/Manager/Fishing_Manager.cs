using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fishing_Manager : MonoBehaviour
{
    public static Fishing_Manager Instance { get; private set; }
    
    [Header("Fishing")] 
    [SerializeField] private Player player;
    [SerializeField] private Fish currentFish;
    [SerializeField] private Fish nextFish;

    private bool isFishing = false;
    
    [Header("Fishing_Animation")]
    private Animator playerAnimator;

    [Header("Fishing_Compounds")] 
    [SerializeField] private Wave_Manager waveManager;
    [SerializeField] private Hook_Manager hookManager;
    [SerializeField] private Encounter_Manager encounterManager;

    [Header("Fishing_UI")] 
    [SerializeField] private Fish_Pulling fishPulling;
    [SerializeField] private Button castButton, skipButton, pullButton;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        
        castButton.onClick.AddListener(CastFishingRod);
        skipButton.onClick.AddListener(SkipFish);
        pullButton.onClick.AddListener(PullFish);

        encounterManager.ResetFishToNone(currentFish, nextFish);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            PullFish();
        }
    }

    // deactivates this button
    // activates Skip fish button

    // Refreshes current fish and next fish
    // adds +1 to wave count

    private void CastFishingRod()
    {
        if (isFishing) return;
        
        isFishing = true;
        playerAnimator.SetTrigger("Cast");

        waveManager.StartNewWave();
        fishPulling.StartPulling();
        
        encounterManager.SetupNewEncounter(currentFish, nextFish);

        Debug.Log("Casting the fishing line...");
    }

    // deactivates this button
    // activates Cast fish button

    // Refreshes current fish and next fish
    // adds +1 to wave count

    private void SkipFish()
    {
        if (!isFishing) return;

        isFishing = false;
        playerAnimator.SetTrigger("Skip");
        fishPulling.StopPulling();
        
        encounterManager.ResetFishToNone(currentFish, nextFish);

        Debug.Log("Skipping turn...");
    }

    // applies dmg to current fish

    // if fish is dead, sets next fish as current, updates next fish
    // updates remaining fish count in current wave

    // applies fish dmg to player if fish is not dead

    private void PullFish()
    {
        if (!isFishing || currentFish == null) return;

        Hook currentHook = hookManager.GetCurrentHook();
        
        bool isHit = fishPulling.TryPull();
        int baseDamage = player.Strength;
        
        int damage = currentHook != null ? currentHook.GetDamage(baseDamage, isHit) : baseDamage;
        
        player.Attack(currentFish, damage);
        
        if (currentHook != null)
        {
            currentHook.LureEffect(currentFish, nextFish, fishPulling);
        }

        if (currentFish.Health <= 0)
        {
            waveManager.DecreaseFishCount();

            if (waveManager.FishRemaining > 0)
            {
                encounterManager.ResetCurrentFish(currentFish);
                encounterManager.ResetNextFish(nextFish);
                    
                fishPulling.ResetFish();
            }
            else
            {
                waveManager.StartNewWave();
                encounterManager.SetupNewEncounter(currentFish, nextFish);
            }
        }
        else
        {
            currentFish.Attack(player, currentFish.Strength);
        }
    }
    
    public Hook_Manager GetHookManager() => hookManager;
    public Fish_Pulling GetFishPulling() => fishPulling;
}
