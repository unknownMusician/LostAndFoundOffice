using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play() => SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    public void Tutorial() => SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    public void Exit() => Application.Quit();
}
