using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneController sc = GameObject.Find("SceneController").GetComponent<SceneController>();

        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => sc.LoadMenu());
    }
}
