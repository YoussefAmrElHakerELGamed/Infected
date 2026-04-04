using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeLogic", menuName = "Upgrade system/BaseUpgradeLogic")]
public class UpgradeLogic : ScriptableObject
{
    public virtual void ApplyUpgrade(StageData stageData, GameObject player, Spawner spawner)
    {

    }
}
