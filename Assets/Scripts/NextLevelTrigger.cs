using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            sceneController.TurnOfButtons();
            sceneController.SetBlackot();

            // Выключения звука передвижения в конце уровня
            FindObjectOfType<SoundManager>().StopSound("movement");
            // убираем игрока со сцены
            Destroy(GameObject.Find("Character").gameObject);
        }
    }
}
