using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField] private float accelerationFactor;
    [SerializeField] private float turnFactor;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyDepletionPerSecond;
    [SerializeField] private float energyGainPerRotation;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Slider energySlider;
    [SerializeField] private float maxAngularVelocity;
    [SerializeField] private float slowdownOnReload;
    private float currentEnergy;
    private float newMovementRotation = 0;

    void Start()
    {
        currentEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy;
    }

    public void Accelerate()
    {
        if (currentEnergy > 0f)
        {
            rigidbody.AddForce(accelerationFactor * transform.up);
        }
    }

    public void Steer(float input, bool accelerating)
    {
        // Limit max speed correlated to energy bar
        
        float variableMaxSpeed = Mathf.Lerp(maxSpeed, 0, (1 - (currentEnergy/maxEnergy)));
        if (rigidbody.linearVelocity.magnitude > variableMaxSpeed)
        {
            rigidbody.linearVelocity = rigidbody.linearVelocity.normalized * variableMaxSpeed;
        }

        if (input == 0)
        {
            rigidbody.angularVelocity = Mathf.Lerp(rigidbody.angularVelocity, 0, 0.05f);
            return;
        }

        rigidbody.AddTorque(turnFactor * input);
        if (rigidbody.angularVelocity > maxAngularVelocity)
        {
            rigidbody.angularVelocity = maxAngularVelocity;
        }
        if (rigidbody.angularVelocity < -maxAngularVelocity)
        {
            rigidbody.angularVelocity = -maxAngularVelocity;
        }

        if (Vector2.Dot(rigidbody.linearVelocity, transform.up) > 0)
        {
            if (math.abs(rigidbody.angularVelocity) < maxAngularVelocity)
            {
                newMovementRotation = Mathf.Deg2Rad*rigidbody.rotation;
            } else
            {
                newMovementRotation = Mathf.Deg2Rad*(rigidbody.rotation - rigidbody.angularVelocity / 4);
            }
            rigidbody.linearVelocity = rigidbody.linearVelocity.magnitude * new Vector2(-math.sin(newMovementRotation), math.cos(newMovementRotation));
        }
    }

    public void Deplete()
    {
        currentEnergy = Mathf.Max(currentEnergy - energyDepletionPerSecond * Time.deltaTime, 0);
        energySlider.value = currentEnergy;
    }

    public void Refuel(float rotation) {
        currentEnergy = MathF.Min(currentEnergy + energyGainPerRotation * rotation / 360, maxEnergy);
        energySlider.value = currentEnergy;
        
        rigidbody.AddForce(accelerationFactor / 3 * -transform.up);
    }
}
