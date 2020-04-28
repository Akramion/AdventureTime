using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // игрок добрался до конца уровня
        if (collider.tag == "Player")
        {
            // включаем промежуточную панель
            SceneController sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
            sceneController.OpenTransitionPanel();

            // убираем игрока со сцены
            Destroy(GameObject.Find("Character").gameObject);
        }
    }
}
