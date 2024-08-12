using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    private string scencName;
    public void NextScene()
    {
        SceneManager.LoadScene(scencName); 
    }

}