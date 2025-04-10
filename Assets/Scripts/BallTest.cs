using UnityEngine;

public class LaunchBallAtCoordinates : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public Transform tableSurface; // ������ �� ������ ����� ��� ���������, �� ������� ��������� ����

    public Vector3 targetCoordinates = new Vector3(0f, 0f, 0f); // ���������� ����������� ���� (x, z)
    public float speedMultiplier = 0.5f; // ��������� ��� ���������� ���� �����

    void Start()
    {
        LaunchBallToCoordinates(targetCoordinates);
    }

    void LaunchBallToCoordinates(Vector3 targetPosition)
    {
        // ���������, ���� �� ������ �� ������ �����
        if (tableSurface == null)
        {
            Debug.LogError("Table surface reference is not set!");
            return;
        }

        // �������� ������ �����
        float tableHeight = tableSurface.position.y;

        // ��������� ������ ����
        float ballStartHeight = ballRigidbody.position.y;

        // ���������� �� ��������� �� ������ �����
        float verticalDistance = tableHeight - ballStartHeight;

        // ������������ ����� ������ ��� ���������� ������� ������ (�����)
        float timeToTarget = CalculateTimeToTarget(verticalDistance);

        // ������������ ��������� ������������ ��������
        float initialVerticalSpeed = CalculateVerticalSpeed(verticalDistance, timeToTarget);

        // ������������ �������������� �������� ��� ���������� ������� X � Z ���������
        Vector3 horizontalDistance = new Vector3(targetPosition.x - ballRigidbody.position.x, 0, targetPosition.z - ballRigidbody.position.z);
        Vector3 horizontalSpeed = horizontalDistance / timeToTarget;

        // ��������� ��������� �������� ��� ���������� ���� �����
        Vector3 launchVelocity = new Vector3(horizontalSpeed.x, initialVerticalSpeed, horizontalSpeed.z) * speedMultiplier;

        // ��������� �������� � Rigidbody ����
        ballRigidbody.velocity = launchVelocity;

        // ������� ���������� � ������� ��� �������
        Debug.Log($"Ball launched with velocity: {launchVelocity}");
    }

    float CalculateVerticalSpeed(float verticalDistance, float timeToTarget)
    {
        float g = Mathf.Abs(Physics.gravity.y); // ��������� ���������� ������� (����������)

        // ��������� ����������� ��������: ����� ��� ����
        if (verticalDistance > 0)
        {
            // ���� ���� ���� ��������� �����
            return (verticalDistance + 0.5f * g * Mathf.Pow(timeToTarget, 2)) / timeToTarget;
        }
        else
        {
            // ���� ���� ���� ��������� �����
            return (verticalDistance - 0.5f * g * Mathf.Pow(timeToTarget, 2)) / timeToTarget;
        }
    }

    float CalculateTimeToTarget(float verticalDistance)
    {
        float g = Mathf.Abs(Physics.gravity.y); // ��������� ���������� ������� (����������)
        return Mathf.Sqrt(2 * Mathf.Abs(verticalDistance) / g);
    }

    bool IsValidVelocity(Vector3 velocity)
    {
        // ���������, ��� �������� �� �������� NaN ��� �������������
        return !float.IsNaN(velocity.x) && !float.IsNaN(velocity.y) && !float.IsNaN(velocity.z) &&
               !float.IsInfinity(velocity.x) && !float.IsInfinity(velocity.y) && !float.IsInfinity(velocity.z);
    }
}
