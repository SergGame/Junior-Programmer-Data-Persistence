using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private Text _positionText;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _scoreText;

    public void SetHighScore(int position, string name, int score)
    {
        _positionText.text = position.ToString();
        _nameText.text = name;
        _scoreText.text = score.ToString();
    }
}
