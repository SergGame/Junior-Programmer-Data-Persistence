using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreTableUI : MonoBehaviour
{
    [SerializeField] Transform _conteiner;
    [SerializeField] private HighScoreUI _highScore;

    private void Awake()
    {
        float step = 50f;

        for (int i = 0; i < PlayerManager.Instance.MaxCount; i++)
        {
            HighScoreUI tableElement = Instantiate(_highScore, _conteiner);
            tableElement.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -step * i);
            tableElement.SetHighScore(i + 1, PlayerManager.Instance.Leaderboard.PlayerDatas[i].Name,
                PlayerManager.Instance.Leaderboard.PlayerDatas[i].Score);
            tableElement.gameObject.SetActive(true);
        }
    }

    public void LoadMeinMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
