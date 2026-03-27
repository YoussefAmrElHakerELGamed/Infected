using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private StageData stageData;

    private Transform _objTransform;
    private Camera _mainSceneCamera;
    private int _currentWave, _unlockedEnemiesIdx;

    void Start()
    {
        _objTransform = transform;
        _mainSceneCamera = Camera.main;

        // call for first wave
        StartCoroutine(SpawnEnemies());

        // call spawner to spawn when finish upgrading
        // event => spawnEnemies()
    }

    private IEnumerator SpawnEnemies()
    {
        for (int idx = 0; idx < Mathf.Ceil(stageData.EnemiesProgression.Evaluate(_currentWave / stageData.NumberOfWaves) * stageData.MaxEnemyNumber); idx++)
        {
            int m_enemyIdx = Random.Range(0, _unlockedEnemiesIdx);
            Vector2 m_spawnLocation = GetRandomLoc();

            GameObject m_spawnEnemy = Instantiate(stageData.StageEnemies[m_enemyIdx], m_spawnLocation, Quaternion.identity, _objTransform);
            m_spawnEnemy.GetComponent<EnemiesHealth>().SetHardness(stageData.HardnessFactor);

            yield return new WaitForSeconds(1f / stageData.SpawnRate);
        }
        WaveFinished();
    }

    private Vector2 GetRandomLoc()
    {
        Vector2 m_location = Random.insideUnitCircle;
        m_location.Normalize();
        return _mainSceneCamera.orthographicSize * 2 * m_location;
    }

    private void WaveFinished()
    {
        // upgrade call from here
        _currentWave++;
        if (_currentWave % stageData.UnlockEnemyEvery == 0)
            _unlockedEnemiesIdx++;
    }
}
