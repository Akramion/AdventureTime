using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCurrentPlayerButtonText : MonoBehaviour
{
    [SerializeField]
    private GameObject curPlayerText;

    void Start()
    {
        RatingController ratingController = GameObject.Find("SceneController").GetComponent<RatingController>();
        Text text = curPlayerText.GetComponent<Text>();
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => {
            text.text = "Сейчас играет " + ratingController.curPlayerName;
        });
    }
}
