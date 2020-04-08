using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score = 0;
    private Text scoreComponent;

    private void Awake() {
        scoreComponent = gameObject.GetComponent<Text>();
    }

    public void ChangeScore() {
        score++;
        scoreComponent.text = score.ToString();
    }
}
