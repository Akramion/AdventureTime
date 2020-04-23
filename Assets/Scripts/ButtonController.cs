using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SceneController sceneController;
    private void Start() {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }
    public void NextLevel() {
        sceneController.NextLevel();
    }

    public void RestartLevel() {
        sceneController.RestartLevel();
    }
}
