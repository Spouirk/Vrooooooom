using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float accelerationFactor;
    [SerializeField] private float turnFactor;
    [SerializeField] private Rigidbody2D rigidbody;

    public void Accelerate()
    {
        rigidbody.AddForce(accelerationFactor * transform.up);
    }

    public void Steer(float input)
    {
        rigidbody.transform.Rotate(new Vector3(0, 0, turnFactor * input));
    }
}
