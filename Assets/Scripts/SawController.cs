using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(waypoints.Length > 0) {
            transform.position = waypoints[waypointIndex].transform.position;
        }
    }

    private void Update() {
        if(waypoints.Length > 0) {
            Move();
        }
        
    }

    private void Move() {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);



        if(transform.position == waypoints[waypointIndex].transform.position) {
            waypointIndex++;
        }

        if(waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
}

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            sceneController.LoadCurrentLevel();
        }
    }
}
