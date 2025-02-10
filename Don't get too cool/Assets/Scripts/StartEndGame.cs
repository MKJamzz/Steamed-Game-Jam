using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadHomeBase()
    {
        Debug.Log("Button pressed: Attempting to load scene: " );
        // Loads the scene with the specified name
        SceneManager.LoadScene("Homebase");
    }

    public void ExitGame(){
        Application.Quit();
    }
}
