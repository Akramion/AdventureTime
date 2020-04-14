using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SawController : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 2f;

    int waypointIndex = 0;
    private SceneController sceneController;
    private void Awake() {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    private void Start() {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        Debug.Log(waypointIndex);



        if(transform.position == waypoints[waypointIndex].transform.position) {
            waypointIndex++;
        }

        if(waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }

        Debug.Log(transform.position);
        Debug.Log(waypoints[waypointIndex].transform.position);
}

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            sceneController.RestartLevel();
        }
    }
}
