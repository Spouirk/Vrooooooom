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
    [SerializeField] private float driftFactor;
    [SerializeField] private int minSpeedToTurnFactor;
    [SerializeField] private float dragFactor;
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
    private float rotationAngle = 0;
    private int isInGrass;

    void Start()
    {
        currentEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy;
    }

    public void Accelerate()
    {
        if (currentEnergy > 0f)
        {
            rigidbody.AddForce(accelerationFactor * transform.up * (2-isInGrass)/2f, ForceMode2D.Force);
        }
    }

    public void Steer(float input, bool accelerating)
    {
        float minSpeedToTurn = rigidbody.linearVelocity.magnitude / minSpeedToTurnFactor;
        minSpeedToTurn = Mathf.Clamp01(minSpeedToTurn);

        rotationAngle += input * turnFactor * minSpeedToTurn;
        rigidbody.MoveRotation(rotationAngle);
        
        // Limit max speed correlated to energy bar

        float variableMaxSpeed = Mathf.Lerp(maxSpeed - 100*(1-isInGrass), 0, (1 - (currentEnergy/maxEnergy)));
        if (rigidbody.linearVelocity.magnitude > variableMaxSpeed)
        {
            rigidbody.linearVelocity = rigidbody.linearVelocity.normalized * variableMaxSpeed;
        }

        // Friction

        if(!accelerating)
        {
            rigidbody.linearDamping = Mathf.Lerp(rigidbody.linearDamping, dragFactor, Time.deltaTime * dragFactor);
        }
        else rigidbody.linearDamping = 0;

        // if (input == 0)
        // {
        //     rigidbody.angularVelocity = Mathf.Lerp(rigidbody.angularVelocity, 0, 0.05f);
        //     return;
        // }

        // rigidbody.AddTorque(turnFactor * input);
        // if (rigidbody.angularVelocity > maxAngularVelocity)
        // {
        //     rigidbody.angularVelocity = maxAngularVelocity;
        // }
        // if (rigidbody.angularVelocity < -maxAngularVelocity)
        // {
        //     rigidbody.angularVelocity = -maxAngularVelocity;
        // }

        // if (Vector2.Dot(rigidbody.linearVelocity, transform.up) > 0)
        // {
        //     if (math.abs(rigidbody.angularVelocity) < maxAngularVelocity)
        //     {
        //         newMovementRotation = Mathf.Deg2Rad*rigidbody.rotation;
        //     } else
        //     {
        //         newMovementRotation = Mathf.Deg2Rad*(rigidbody.rotation - rigidbody.angularVelocity / 4);
        //     }
        //     rigidbody.linearVelocity = rigidbody.linearVelocity.magnitude * new Vector2(-math.sin(newMovementRotation), math.cos(newMovementRotation));
        // }
    }

    public void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rigidbody.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rigidbody.linearVelocity, transform.right);

        rigidbody.linearVelocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void Deplete()
    {
        currentEnergy = Mathf.Max(currentEnergy - energyDepletionPerSecond * Time.deltaTime, 0);
        energySlider.value = currentEnergy;
    }

    public void Refuel(float rotation) {
        currentEnergy = MathF.Min(currentEnergy + energyGainPerRotation * rotation / 360, maxEnergy);
        energySlider.value = currentEnergy;
        
        rigidbody.AddForce(accelerationFactor / 3 * -transform.up, ForceMode2D.Force);
    }

    int grassLayer = 9;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == grassLayer)
        {
            isInGrass = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == grassLayer)
        {
            isInGrass = 0;
        }
    }
}
