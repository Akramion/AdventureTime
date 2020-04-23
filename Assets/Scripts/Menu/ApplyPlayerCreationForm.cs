using UnityEngine;
using UnityEngine.UI;

public class ApplyPlayerCreationForm : MonoBehaviour
{
    [SerializeField]
    private NameList nameList;

    void Start()
    {
        RatingController ratingController = GameObject.Find("SceneController").GetComponent<RatingController>();

        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => {
            string playerName = transform.parent.Find("InputField").GetComponent<InputField>().text;
            ratingController.AddPlayer(playerName);
            nameList.AppendPlayer(playerName);
        });
    }
}
