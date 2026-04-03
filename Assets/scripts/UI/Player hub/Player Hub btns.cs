using UnityEngine;

public class PlayerHubBtns : MonoBehaviour
{
    public void OnBackClick()
    {
        GameSceneManager.Instance.TransitionWithReplaceScene("PlayerHub", "MainMenu", 1, true);
    }
}
