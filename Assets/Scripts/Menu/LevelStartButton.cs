using UnityEngine;
using UnityEngine.UI;


public class LevelStartButton : MonoBehaviour
{
    void Start()
    {
        SceneController sc = GameObject.Find("SceneController").GetComponent<SceneController>();
        Text text = transform.Find("Text").GetComponent<Text>();

        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => {
            int levelIndex = int.Parse(text.text) - 1;
            sc.SetLevel(levelIndex);
            sc.NextLevel();
        });
    }
}
