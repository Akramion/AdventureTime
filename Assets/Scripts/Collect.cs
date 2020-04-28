using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private ScoreController score;

    private void Awake()
    {
        score = GameObject.Find("GameCanvas/GamePanel/Score").GetComponent<ScoreController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            score.ChangeScore();
            Destroy(gameObject);
        }
    }
}
