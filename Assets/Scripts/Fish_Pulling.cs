using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Fish_Pulling : MonoBehaviour
{
    [Header("Pulling Settings")]
    [SerializeField] private RectTransform fish;
    [SerializeField] private RectTransform pullingArea;
    
    [Header("Reticle Settings")]
    [SerializeField] private GameObject reticlePrefab;
    private List<RectTransform> reticles = new List<RectTransform>();
    
    [Header("Fish Settings")]
    [SerializeField] public float fishSpeed = 200f;
    [SerializeField] private float offset = 20;
    [SerializeField] private float smoothTurnTime = 0.2f;
    
    private float smoothVelocity;
    private float currentSpeed;
    private float halfWidth;
    
    private Vector2 direction = Vector2.right;
    private bool isMoving = false;
    private bool canPull = true;
    
    private Coroutine pullingCoroutine;
    private Animator animator;

    private void Start()
    {
        halfWidth = pullingArea.GetComponent<RectTransform>().rect.width / 2f;
        animator = fish.GetComponent<Animator>();
    }
    
    public void SetReticleCount(int count)
    {
        foreach (var reticle in reticles)
        {
            Destroy(reticle.gameObject);
        }
        
        reticles.Clear();
        
        float[] positions = count == 2 ? new[] { -30f, 30f } : new[] { 0f };
        
        for (int i = 0; i < count; i++)
        {
            GameObject newReticle = Instantiate(reticlePrefab, pullingArea);
            RectTransform rect = newReticle.GetComponent<RectTransform>();
            
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            
            rect.anchoredPosition = new Vector2(positions[i], 0f);
        
            reticles.Add(rect);
        }
    }


    public void StartPulling()
    {
        ResetFish();
        isMoving = true;
    }

    public void StopPulling()
    {
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveFish();
        }
    }

    private void MoveFish()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, direction.x * fishSpeed, ref smoothVelocity, smoothTurnTime);
        fish.anchoredPosition += new Vector2(currentSpeed * Time.deltaTime, 0f);

        if (fish.anchoredPosition.x > halfWidth - offset)
        {
            direction = Vector2.left;
        }
        else if (fish.anchoredPosition.x < -halfWidth + offset)
        {
            direction = Vector2.right;
        }
    }

    public bool TryPull()
    {
        if (!canPull) return false;
        
        bool isHit = RectOverlaps();
        ResetFish();
        return isHit;
    }

    private bool RectOverlaps()
    {
        Vector3[] fishCorners = new Vector3[4];
        fish.GetWorldCorners(fishCorners);
        Rect fishRect = new Rect(fishCorners[0], fishCorners[2] - fishCorners[0]);
        
        foreach (var reticle in reticles)
        {
            Vector3[] reticleCorners = new Vector3[4];
            reticle.GetWorldCorners(reticleCorners);

            Rect reticleRect = new Rect(reticleCorners[0], reticleCorners[2] - reticleCorners[0]);

            if (fishRect.Overlaps(reticleRect))
            {
                return true;
            }
        }

        return false;
    }

    public void ResetFish()
    {
        if (pullingCoroutine != null)
        {
            StopCoroutine(pullingCoroutine);
            pullingCoroutine = null;
            
        }
        
        canPull = true;
        pullingCoroutine = StartCoroutine(ResetFishRoutine());
    }

    private IEnumerator ResetFishRoutine()
    {
        canPull = false;
        animator.SetTrigger("FadeOut");
        
        yield return new WaitForSeconds(0.5f);
            
        canPull = true;
        animator.SetTrigger("FadeIn");
        
        fish.anchoredPosition = new Vector2(Random.Range(-65, 65), fish.anchoredPosition.y);
    }
    
    public void SetFishSpeed(float newSpeed)
    {
        fishSpeed = newSpeed;
    }
}
