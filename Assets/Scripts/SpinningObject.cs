using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeedInDegrees;

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeedInDegrees * Time.deltaTime);
    }
}
