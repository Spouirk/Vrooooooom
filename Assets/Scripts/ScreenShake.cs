using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] float shakeStrength;
    public void shake()
    {
        transform.position = transform.position + new Vector3(Random.Range(-shakeStrength, shakeStrength), Random.Range(-shakeStrength, shakeStrength), 0);
    }
}
