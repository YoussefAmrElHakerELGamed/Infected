using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventBus : MonoBehaviour
{
    public static GameEventBus Instance { get; private set; }

    #region player_events
    public Action<PlayerGunReloadEventMassage> OnPlayerGunReloaded;
    #endregion

    #region enemies
    public Action OnEnemiesDeath;
    #endregion

    #region waves_events
    public Action<SpawnerWaveEventMassage> OnSpawnerWaveStart;
    public Action<SpawnerWaveEventMassage> OnSpawnerWaveEnd;
    #endregion

    #region upgrade_screen_events
    public Action<OnUpgradeStartEventMassage> OnShowUpgradeScreen;
    public Action<OnHideAndApplyUpgradeEventMassage> OnHideAndApplyUpgrade;
    #endregion

    #region build_system_events
    public Action<BuildSystemEventMassage> OnBuildObject;
    #endregion

    void Awake()
    {
        if (Instance != null)
        {
            print("there is more one Event bus in scene");
            return;
        }
        Instance = this;
    }

    void Start()
    {
        SceneManager.activeSceneChanged += FreeOldPointers;
    }

    private void FreeOldPointers(Scene arg0, Scene arg1)
    {
        OnBuildObject = null;
    }
}

public struct BuildSystemEventMassage
{
    public int GameObjectToBuild;
}

public struct PlayerGunReloadEventMassage
{
    public float waitingTime;
}

public struct SpawnerWaveEventMassage
{
    public int waveNumber;
    public int waveMaxEnemies;
}

public struct OnUpgradeStartEventMassage
{
    public int numberOfUpgradeCards;
    // we could add rarity and such
}

public struct OnHideAndApplyUpgradeEventMassage
{
    // public upgrade[] upgradesToApply;
}