using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    SceneController sceneController;
    private void Awake() {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            sceneController.NextLevel();
        }
    }
}
