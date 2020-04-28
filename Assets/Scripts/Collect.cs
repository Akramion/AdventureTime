using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{

    private ScoreController score;
    SoundManager soundManager;


    private void Start() {
        score = GameObject.Find("Score").GetComponent<ScoreController>();

        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            soundManager.Play("coin");
            score.ChangeScore();
            Destroy(gameObject);
        }
    }
}
