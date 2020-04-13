﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundScrolling : MonoBehaviour
{
    public float speed;
    public Vector3 startPosition;
    private float scrollHeight;

    void Start()
    {
        scrollHeight = Camera.main.orthographicSize * 2;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y * 2.5f);

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPosition.y - transform.position.y >= scrollHeight)
        {
            transform.position = startPosition;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);

    }
}
