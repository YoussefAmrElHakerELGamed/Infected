using UnityEngine;

public class MainMenuBtns : MonoBehaviour
{
    [SerializeField] private string toSceneName = "PlayerHub";
    public void OnPlayClick()
    {
        GameSceneManager.Instance.TransitionWithReplaceScene("MainMenu", toSceneName, 1f, true);
    }

    public void OnQuitClick() => Application.Quit();
}
