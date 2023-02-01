using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour {
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}