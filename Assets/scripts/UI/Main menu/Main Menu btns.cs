using UnityEngine;

public class MainMenuBtns : MonoBehaviour
{

    public void OnPlayClick()
    {
        GameSceneManager.Instance.TransitionWithReplaceScene("MainMenu", "GameScene", 1f, true);
    }

    public void OnQuitClick() => Application.Quit();
}
