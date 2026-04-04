using System;
using UnityEngine;
using UnityEngine.UI;

public class upgradeUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] upgradeBtns;
    [SerializeField] private GameObject UpgradeMenu;
    void Start()
    {
        GameEventBus.Instance.OnSystemSelectedUpgrade += displayUpgrades;
    }

    private void displayUpgrades(OnUpgradeSystemSelectedUpgradesEventArg arg)
    {
        UpgradeMenu.SetActive(true);
        for (int btnIdx = 0; btnIdx < upgradeBtns.Length; btnIdx++)
        {
            upgradeBtns[btnIdx].GetComponent<Image>().sprite = arg.upgrade[0].upgradeSprite;
            upgradeBtns[btnIdx].GetComponent<Button>().onClick.AddListener(() =>
            {
                GameEventBus.Instance.OnHideAndApplyUpgrade?.Invoke(new() { upgradeToApply = arg.upgrade[btnIdx] });
                UpgradeMenu.SetActive(false);
            });
        }
    }
}
