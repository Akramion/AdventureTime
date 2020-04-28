using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public bool isGrounded = false;
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Ground") {
            FindObjectOfType<SoundManager>().Play("landing");
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.collider.tag == "Ground") {
            isGrounded = false;
        }
    }
}
