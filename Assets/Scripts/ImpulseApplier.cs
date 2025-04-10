using UnityEngine;

public class ImpulseApplier : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public float impulseForce = 5f; // ���� �������� �� ���������

    void Update()
    {
        // ������: ���������� �������� � ��������� ����������� �� ������� �������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ��������, ���������� �������� ������
            ApplyImpulse(Vector3.right * impulseForce);
        }

        // ������: ���������� �������� �����
        if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyImpulse(Vector3.left * impulseForce);
        }

        // ������: ���������� �������� �����
        if (Input.GetKeyDown(KeyCode.W))
        {
            ApplyImpulse(Vector3.forward * impulseForce);
        }

        // ������: ���������� �������� ����
        if (Input.GetKeyDown(KeyCode.S))
        {
            ApplyImpulse(Vector3.back * impulseForce);
        }
    }

    void ApplyImpulse(Vector3 direction)
    {
        ballRigidbody.AddForce(direction, ForceMode.Impulse);
    }

    // ����� ��� ��������� ���� �������� ����� ��������� Unity
    public void SetImpulseForce(float newForce)
    {
        impulseForce = newForce;
    }
}
