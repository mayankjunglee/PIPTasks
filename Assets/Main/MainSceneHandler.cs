using UnityEngine;
using UnityEngine.SceneManagement;
public class MainSceneHandler : MonoBehaviour
{
    // public void LoadScene()
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
