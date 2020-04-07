using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public bool isGrounded = true;
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Ground") {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.collider.tag == "Ground") {
            isGrounded = false;
        }
    }
}
