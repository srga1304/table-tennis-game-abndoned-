using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyImpulse(Vector3 impulse)
    {
        rb.AddForce(impulse, ForceMode.Impulse);
    }

    public void ApplySpin(Vector3 spin)
    {
        rb.AddTorque(spin, ForceMode.Impulse);
    }
}
