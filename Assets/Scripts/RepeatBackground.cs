using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private float repeatThreshold;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        var boxCollider = GetComponent<BoxCollider>();
        repeatThreshold = boxCollider.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatThreshold)
        {
            transform.position = startPos;
        }
    }
}
