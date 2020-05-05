using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button b = GetComponent<Button>();
        SceneController sc = GameObject.Find("SceneController").GetComponent<SceneController>();
        b.onClick.AddListener(() => sc.Exit());
    }
}
