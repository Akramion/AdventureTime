using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private Canvas canvas;
    private Camera camera;
    
    private void Awake() {
        DontDestroyOnLoad(this);
        canvas = GetComponent<Canvas>();
        
    }

    public void CameraAttach() {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas.worldCamera = camera;
    }



    void Update()
    {
        
    }
}
