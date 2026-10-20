using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
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
    private float currentEnergy;

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
        /*
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

        if (math.abs(rigidbody.angularVelocity) < 25)
        {
            rigidbody.linearVelocity = rigidbody.linearVelocity.magnitude * new Vector2(-math.sin(Mathf.Deg2Rad*rigidbody.rotation), math.cos(Mathf.Deg2Rad*rigidbody.rotation));
        }*/
        if (Mathf.Abs(input) > 0)
        {   
            float speedBaseSteerLimit = rigidbody.linearVelocity.y / 0.5f;
            speedBaseSteerLimit = Mathf.Clamp01(speedBaseSteerLimit);

            rigidbody.AddTorque(turnFactor * input * speedBaseSteerLimit);

            float normalizedX = rigidbody.linearVelocity.x / maxAngularVelocity;
            normalizedX = Mathf.Clamp(normalizedX, -1.0f, 1.0f);

            rigidbody.linearVelocity = new Vector3(normalizedX * maxAngularVelocity, 0, rigidbody.linearVelocity.y);
        }
        else
        {
            rigidbody.linearVelocity = Vector3.Lerp(rigidbody.linearVelocity, new Vector3(0, 0, rigidbody.linearVelocity.y), Time.fixedDeltaTime * 3);
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
    }
}
