using System;
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

    public void Steer(float input)
    {
        // Limit max speed correlated to energy bar
        float variableMaxSpeed = Mathf.Lerp(maxSpeed, 0, (1 - (currentEnergy/maxEnergy)));
        if (rigidbody.linearVelocity.magnitude > variableMaxSpeed)
        {
            rigidbody.linearVelocity = rigidbody.linearVelocity.normalized * variableMaxSpeed;
        }

        rigidbody.transform.Rotate(new Vector3(0, 0, turnFactor * input));
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
