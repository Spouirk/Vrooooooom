using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] Rigidbody2D car;
    [SerializeField] float cameraPredictionFactor;
    private Vector3 offset = new Vector3(0, 0, -10);

    // Update is called once per frame
    void Update()
    {   
        Vector2 cameraxypos = car.linearVelocity * cameraPredictionFactor;
        transform.position = (Vector3)Vector2.Lerp((Vector2)transform.position, (Vector2)car.transform.position + cameraxypos, 0.02f) + offset;
    }
}
