using System;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Button newGame;
    public Button loadGame;
    public Button crow;

    private void Start()
    {
        newGame.onClick.AddListener(NewGame);
        newGame.interactable = true;
        if (PlayerPrefs.HasKey("Level"))
        {
            loadGame.onClick.AddListener(LoadGame);
            loadGame.interactable = true;
        }
        // crow.onClick.AddListener(Crow);
        // crow.interactable = true;
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Crow()
    {
        throw new NotImplementedException();
    }
}