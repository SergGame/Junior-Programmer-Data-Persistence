using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;

    private bool _isStarted = false;
    private int _points;
    private bool _isGameOver = false;

    void Start()
    {
        ScoreText.text = $"{PlayerManager.Instance.CurrentName}  Score : {_points}";
        BestScoreText.text = $"Best Score : {PlayerManager.Instance.Leaderboard.PlayerDatas[0].Name} :" +
            $" {PlayerManager.Instance.Leaderboard.PlayerDatas[0].Score}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!_isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        _points += point;
        ScoreText.text = $"{PlayerManager.Instance.CurrentName}  Score : {_points}";
    }

    public void GameOver()
    {
        PlayerManager.Instance.SaveScore(_points);
        _isGameOver = true;
        GameOverText.SetActive(true);
    }
}
