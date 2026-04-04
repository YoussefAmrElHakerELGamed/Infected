using System.Collections;
using UnityEngine;

public class score : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI ScoreText;
    [SerializeField] private float AnimationSpeed = 2f;
    private int _currentScore;

    void Start()
    {
        GameEventBus.Instance.OnEnemiesDeath += changeScore;
    }

    private void changeScore(OnEnemiesDeathEventArg arg)
    {
        int m_oldScore = _currentScore;
        int m_score = _currentScore + arg.enemyValue;

        GameEventBus.Instance.OnScoreChange?.Invoke(new()
        {
            oldScore = m_oldScore,
            scoreDif = arg.enemyValue,
            newScore = _currentScore
        });

        StartCoroutine(AnimateScore(m_oldScore, _currentScore));
    }

    private IEnumerator AnimateScore(int oldScore, int currentScore)
    {
        yield return new WaitUntil(() =>
        {
            int m_lerpValue = Mathf.RoundToInt(Mathf.Lerp(oldScore, currentScore, Time.deltaTime * AnimationSpeed));
            ScoreText.text = $"{FormateText(m_lerpValue)}";
            return m_lerpValue >= 0.95 * _currentScore;
        });
        ScoreText.text = $"{FormateText(_currentScore)}";
    }

    private string FormateText(int currentScore)
    {
        if (currentScore < 10)
            return $"000{currentScore}";

        if (currentScore < 100)
            return $"00{currentScore}";

        if (currentScore < 1000)
            return $"0{currentScore}";

        return $"{currentScore}";
    }
}
