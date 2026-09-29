using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] CarController carController;
    private InputAction moveAction;
    private InputAction accelerateAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("move");
        accelerateAction = InputSystem.actions.FindAction("accelerate");
    }

    void Update()
    {
        if(accelerateAction.IsPressed())
        {
            carController.Accelerate();
        }
        carController.Steer(moveAction.ReadValue<float>());
    }
}
