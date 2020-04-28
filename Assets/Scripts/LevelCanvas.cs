using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCanvas: MonoBehaviour
{
    // Start is called before the first frame update
    private Canvas canvas;

    private void Awake() {
        canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        // когда объект включается, перепривязываем камеру
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas.worldCamera = camera;
    }
}
