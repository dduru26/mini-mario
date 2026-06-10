using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    // hooked to the Play / Start button
    public void StartGame() {
        Time.timeScale = 1f;                     // safety, in case it was left frozen
        SceneManager.LoadScene("GameScene-ALU"); // loads the gameplay scene
    }

    // hooked to the Quit button
    public void QuitGame() {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

} // class