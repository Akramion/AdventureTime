using UnityEngine;
using UnityEngine.UI;


public class LevelStartButton : MonoBehaviour
{
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener( () => StartLevel() );
    }

    private void StartLevel()
    {
        int levelIndex = int.Parse(transform.Find("Text").GetComponent<Text>().text) - 1;

        SceneController sc = GameObject.Find("SceneController").GetComponent<SceneController>();
        sc.SetLevel(levelIndex);
        sc.NextLevel();
    }
}
