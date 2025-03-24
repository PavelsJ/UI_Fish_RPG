using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    public int WaveCount { get; private set; } = 0;
    public int FishRemaining { get; private set; } = 0;

    [SerializeField] private TextMeshProUGUI waveCountText;
    [SerializeField] private TextMeshProUGUI fishRemainingText;

    public void StartNewWave()
    {
        WaveCount++;
        FishRemaining = Random.Range(1 + WaveCount / 2, 3 + WaveCount / 2);
        UpdateWaveUI();
    }

    public void DecreaseFishCount()
    {
        FishRemaining--;
        UpdateWaveUI();
    }

    private void UpdateWaveUI()
    {
        waveCountText.text = $"Fish Wave: {WaveCount}";
        fishRemainingText.text = $"{FishRemaining} Left";
    }
}
