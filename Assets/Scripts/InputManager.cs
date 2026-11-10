using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] CarController carController;
    [SerializeField] ScreenShake shake;
    private InputAction moveAction;
    private InputAction accelerateAction;

    // Mécanique de rewind : faire des cercles dans le sens anti-horaire recharge l'énergie
    private InputAction rewindAction;
    private Vector2 currentRewindVector;
    private Vector2 previousRewindVector;
    private float rewindDetectionTimer;
    private float rotationAngle;
    [SerializeField] float rewindDetectionMaxDuration;
    [SerializeField] float stickDeadzone;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("move");
        accelerateAction = InputSystem.actions.FindAction("accelerate");
        rewindAction = InputSystem.actions.FindAction("Rewind");
        currentRewindVector = Vector2.zero;
        previousRewindVector = Vector2.zero;
        rewindDetectionTimer = 0;
        rotationAngle = 0;
    }

    float RewindAngle()
    {
        rewindDetectionTimer = Mathf.Max(rewindDetectionTimer - Time.deltaTime, 0);
        if (currentRewindVector != Vector2.zero) {previousRewindVector = currentRewindVector;}
        currentRewindVector = rewindAction.ReadValue<Vector2>();
        
        // Stick deadzone check 
        if (currentRewindVector.sqrMagnitude < stickDeadzone)
        {
            currentRewindVector = Vector2.zero;
            rotationAngle = 0;
        } else
        {
            rotationAngle = Vector2.SignedAngle(previousRewindVector, currentRewindVector);
        }

        // Prevent left/right taps on keyboard for continuous 180 values
        if (rotationAngle > 90)
        {
            rotationAngle = 0;
        }

        // On vérifie que la rotation se fait à plus de 1tour/2secondes (uniquement pour la manette)
        if (Mathf.Abs(rotationAngle) > 180 * Time.deltaTime)
        {
            rewindDetectionTimer = rewindDetectionMaxDuration;
        }
        return rotationAngle;
    }

    void Update()
    {
        float rotation = RewindAngle();
        if (rewindDetectionTimer > 0)
        {
            carController.Refuel(rotation);
            shake.shake();
        } else
        {
            carController.Deplete();
        }


        bool accelerating = false;
        if(accelerateAction.IsPressed() && rewindDetectionTimer<= 0)
        {
            accelerating = true;
            carController.Accelerate();
        }
        carController.KillOrthogonalVelocity();
        carController.Steer(moveAction.ReadValue<float>(), accelerating);
        carController.UpdateSFX();
    }
}
