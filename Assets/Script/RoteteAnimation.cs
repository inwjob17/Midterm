using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoteteAnimation : MonoBehaviour
{
    public float _rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}