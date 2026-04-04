using System.Collections.Generic;
using UnityEngine;

public class upgradeSelector : MonoBehaviour
{
    [SerializeField] private upgrade[] availableUpgrades;
    [SerializeField] private int rerollValue;
    [SerializeField] private StageData stageData;
    [SerializeField] private Spawner spawner;

    void Start()
    {
        GameEventBus.Instance.OnShowUpgradeScreen += SelectRandomUpgrade;
        GameEventBus.Instance.OnHideAndApplyUpgrade += HideAndApplySelectedUpgrade;
        GameEventBus.Instance.OnPlayerRerollUpgrades += RerollUpgrades;
    }

    private void RerollUpgrades()
    {
        if (stageData.currentStageScore >= rerollValue)
        {
            SelectRandomUpgrade(new() { numberOfUpgradeCards = 4 });
            return;
        }

        GameEventBus.Instance.OnRerollNotAllowed?.Invoke();
    }

    private void HideAndApplySelectedUpgrade(OnHideAndApplyUpgradeEventMassage args)
    {
        args.upgradeToApply.logic.ApplyUpgrade(stageData, PlayerMovement.PublicPlayerTransform.gameObject, spawner);
    }

    private void SelectRandomUpgrade(OnUpgradeStartEventMassage args)
    {
        List<upgrade> m_selectedUpgrades = new();
        for (int idx = 0; idx < args.numberOfUpgradeCards;)
        {
            upgrade m_selectedCard = availableUpgrades[Random.Range(0, availableUpgrades.Length)];
            if (m_selectedUpgrades.Contains(m_selectedCard))
                continue;

            m_selectedUpgrades.Add(m_selectedCard);
            idx++;
        }
        GameEventBus.Instance.OnSystemSelectedUpgrade?.Invoke(new() { upgrade = m_selectedUpgrades.ToArray() });
    }
}
