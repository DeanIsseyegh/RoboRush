using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private bool isInGame = false;
    [SerializeField] private bool shouldDestroyWhenOutOfBounds = true;

    public float speed = 5;
    public float xBoundary = -25;

    public void StartGame()
    {
        isInGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInGame)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            if (transform.position.x < xBoundary && shouldDestroyWhenOutOfBounds) Destroy(gameObject);
        }
    }

    public void StopGame()
    {
        isInGame = false;
    }
}
