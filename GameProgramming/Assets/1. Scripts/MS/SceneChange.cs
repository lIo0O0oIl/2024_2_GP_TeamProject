using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private string scencName;
    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scencName); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}