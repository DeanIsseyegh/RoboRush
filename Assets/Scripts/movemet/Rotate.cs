using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotateYSpeed = 360;
    [SerializeField] private float rotateZSpeed = 360;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateYSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotateZSpeed * Time.deltaTime);
    }
}
