using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class waveProgress : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI StartWave, EndWave;
    [SerializeField] private Slider ProgressPar;
    [SerializeField] private float AnimationSpeed = 2;

    private int _nextWave { get { return Math.Clamp(_wave + 1, 0, int.MaxValue); } }
    private int _wave;
    private int _NumberOfEnemiesThisWave;
    private float _waveProgress;
    private int _numberOfDiedEnemies;
    void Start()
    {
        GameEventBus.Instance.OnEnemiesDeath += CountAndUpdateProgress;
        GameEventBus.Instance.OnSpawnerWaveStart += UpdateWaveParams;
    }

    private void UpdateWaveParams(SpawnerWaveEventMassage args)
    {
        _NumberOfEnemiesThisWave = args.waveMaxEnemies;
        _wave = args.waveNumber;

        StartWave.text = $"{FormatWaveText(_wave)}";
        EndWave.text = $"{FormatWaveText(_nextWave)}";

        StartCoroutine(AnimateProgress(0));
    }

    private string FormatWaveText(int wave)
    {
        if (wave < 10)
            return $"0{wave}";

        return $"{wave}";
    }

    private void CountAndUpdateProgress(OnEnemiesDeathEventArg arg)
    {
        _numberOfDiedEnemies++;
        _waveProgress = (float)_numberOfDiedEnemies / _NumberOfEnemiesThisWave;

        StartCoroutine(AnimateProgress(_waveProgress));
    }

    private IEnumerator AnimateProgress(float _currentProgress)
    {
        yield return new WaitUntil(() =>
        {
            ProgressPar.value = Mathf.Lerp(ProgressPar.value, _currentProgress, Time.deltaTime * AnimationSpeed);
            return ProgressPar.value >= _currentProgress;
        });
        ProgressPar.value = _currentProgress;
    }
}
