using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventBus : MonoBehaviour
{
    public static GameEventBus Instance { get; private set; }

    #region player_events
    public Action<PlayerGunReloadEventMassage> OnPlayerGunReloaded;
    public Action<PlayerTakeDamageEventArgs> OnPlayerTakeDamage;
    #endregion

    #region enemies
    public Action<OnEnemiesDeathEventArg> OnEnemiesDeath;
    #endregion

    #region waves_events
    public Action<SpawnerWaveEventMassage> OnSpawnerWaveStart;
    public Action<SpawnerWaveEventMassage> OnSpawnerWaveEnd;
    #endregion

    #region UI_events
    public Action<OnScoreEventArg> OnScoreChange;
    #endregion

    #region upgrade_screen_events
    public Action<OnUpgradeStartEventMassage> OnShowUpgradeScreen;
    public Action<OnUpgradeSystemSelectedUpgradesEventArg> OnSystemSelectedUpgrade;
    public Action<OnHideAndApplyUpgradeEventMassage> OnHideAndApplyUpgrade;
    public Action OnPlayerRerollUpgrades;
    public Action OnRerollNotAllowed;
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

public struct PlayerTakeDamageEventArgs
{
    public float Health, MaxHealth;
    public float Damage;
}

public struct OnEnemiesDeathEventArg
{
    public int enemyValue;
    public enum enemyType
    {
        BaseEnemy
    }
    public enemyType type;
}

public struct OnScoreEventArg
{
    public int oldScore, scoreDif, newScore;
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

public struct OnUpgradeSystemSelectedUpgradesEventArg
{
    public upgrade[] upgrade;
}

public struct OnHideAndApplyUpgradeEventMassage
{
    public upgrade upgradeToApply;
}