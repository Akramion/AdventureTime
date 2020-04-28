using UnityEngine;
using UnityEngine.UI;

public class UpdateCurrentPlayerText : MonoBehaviour
{
    private Text text;
    private RatingController ratingController;

    void Awake()
    {
        text = GetComponent<Text>();
        ratingController = GameObject.Find("SceneController").GetComponent<RatingController>();
    }

    void OnEnable()
    {
        text.text = "Сейчас играет " + ratingController.curPlayerName;
    }
}
