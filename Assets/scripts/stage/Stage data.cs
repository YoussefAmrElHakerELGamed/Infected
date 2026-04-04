using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public GameObject[] StageEnemies;
    public int UnlockEnemyEvery;
    public int NumberOfWaves;
    public int SpawnRate;
    public float HardnessFactor = 1;
    public int MaxEnemyNumber;
    public AnimationCurve EnemiesProgression;

    public int currentStageScore;
    public int NUmberOfWaves { get; internal set; }
}
