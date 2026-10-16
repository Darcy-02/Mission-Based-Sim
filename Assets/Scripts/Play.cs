using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    [Header("Set your Game Scene Name")]
    public string gameSceneName = "SpaceTime";

    
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}