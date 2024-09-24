using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0,2)] float timeScale = 1;
    private void Update() {
        Time.timeScale = timeScale;
    }

    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
            
            Application.Quit();
#endif
    }
}
