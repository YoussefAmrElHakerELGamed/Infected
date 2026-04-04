using UnityEngine;
using System.Collections.Generic;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;
    [SerializeField] private bool StopTimeWhenNavigating = false;
    private Stack<GameObject> LoadedMenus = new();

    void Start()
    {
        ShowMenu(menus[0]);
    }

    public void EnterMenuIdx(int idx)
    {
        EnterMenu(menus[idx]);
        if (StopTimeWhenNavigating)
            Time.timeScale = idx == 0 ? 1 : 0;

    }

    public void ExitCurrentMenu()
    {
        ExitMenu();
    }

    private void ShowMenu(GameObject menu)
    {
        menu.SetActive(true);
        LoadedMenus.Push(menu);
    }

    private void EnterMenu(GameObject menu)
    {
        var m_topLoadedMenu = LoadedMenus.Peek();
        m_topLoadedMenu.SetActive(false);

        menu.SetActive(true);
        LoadedMenus.Push(menu);
    }

    private void ExitMenu()
    {
        var m_topLoadedMenu = LoadedMenus.Pop();
        if (m_topLoadedMenu == null) return;

        m_topLoadedMenu.SetActive(false);

        var m_loadedMenu = LoadedMenus.Peek();
        m_loadedMenu.SetActive(true);
    }
}
