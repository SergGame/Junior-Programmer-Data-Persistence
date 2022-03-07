using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private Text _layerName;

    public void StartGame()
    {
        if (string.IsNullOrEmpty(_layerName.text))
        {
            _inputField.image.color = Color.red;
        }
        else
        {
            PlayerManager.Instance.CurrentName = _layerName.text;
            SceneManager.LoadScene("Main");
        }
    }

    public void SetWiteColor()
    {
        _inputField.image.color = Color.white;
    }

    public void HighScore()
    {
        SceneManager.LoadScene("High score");
    }

    public void ExitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
