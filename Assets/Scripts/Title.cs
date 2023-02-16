using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Button newGame;
    public Button loadGame;
    public Button credits;
    public Button crow;

    public GameObject title;
    public GameObject buttons;
    public GameObject creditsText;
    public Button exitCredits;

    private void Start()
    {
        newGame.onClick.AddListener(NewGame);
        newGame.interactable = true;
        if (PlayerPrefs.HasKey("Level"))
        {
            loadGame.onClick.AddListener(LoadGame);
            loadGame.interactable = true;
        }

        newGame.onClick.AddListener(NewGame);
        newGame.interactable = true;

        credits.onClick.AddListener(Credits);
        credits.interactable = true;

        exitCredits.onClick.AddListener(ExitCredits);
        exitCredits.interactable = true;
        // crow.onClick.AddListener(Crow);
        // crow.interactable = true;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        title.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        title.transform.localPosition = new Vector3(0, 150, 0);
        buttons.SetActive(false);
        creditsText.SetActive(true);
    }

    public void ExitCredits()
    {
        title.transform.localScale = new Vector3(1, 1, 1);
        title.transform.localPosition = new Vector3(0, 80, 0);
        buttons.SetActive(true);
        creditsText.SetActive(false);
    }

    public void Crow()
    {
        throw new NotImplementedException();
    }
}