using UnityEngine;

public class gameSceneBtns : MonoBehaviour
{
    public void OnRestartGame()
    {
        GameSceneManager.Instance.TransitionWithReplaceScene("GameScene", "GameScene", 1, true);
    }

    public void OnExitGame()
    {
        GoToPlayerHub();
    }
    public void OnRestartStageForPoints()
    {
        GoToPlayerHub();
        // Save TotalPoints 100 > 1
    }

    public void RerollUpgrades()
    {
        GameEventBus.Instance.OnPlayerRerollUpgrades?.Invoke();
    }

    private void GoToPlayerHub()
    {
        GameSceneManager.Instance.TransitionWithReplaceScene("GameScene", "PlayerHub", 1, true);
    }

}
