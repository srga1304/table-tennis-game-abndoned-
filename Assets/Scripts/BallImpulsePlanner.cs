using UnityEngine;

public class BallImpulsePlanner : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public Transform groundCheck; // ����� ��� �������� ����� (������ �� ������ �����)
    public float gravity = 9.81f; // ��������� ���������� �������
    public float maxHeight = 3f; // ������������ ������ ������ ����

    // ������� ��� ������� � ���������� �������� � ����
    public void ApplyImpulseToReachPosition(Vector3 targetPosition)
    {
        // ��������� ��������� ���� (�� ������� ��� ��������� �� -8.04x 2.04y -0.78z)
        Vector3 initialPosition = transform.position;

        // ���������, ��������� �� ���� �� �������� ��������� ����
        if (!IsReachable(targetPosition))
        {
            Debug.LogWarning("���� ����������� �� �������� ��������� ����.");
            return;
        }

        // ��������� ��������� �������� ��� ���������� ����
        Vector3 initialVelocity = CalculateInitialVelocity(initialPosition, targetPosition);

        // ��������� ����������� ��������� �������� � ����
        ballRigidbody.velocity = initialVelocity;
    }

    // ��������, ��������� �� ���� �� �������� ��������� ����
    bool IsReachable(Vector3 targetPosition)
    {
        // ������������ ���������� �� ����
        float deltaY = targetPosition.y - transform.position.y;

        // ���� ���� ���� �������� ��������� ����, ��� �����������
        if (deltaY < 0)
        {
            return false;
        }

        // ������������ �������� ��� ���������� ������������ ������
        float verticalSpeed = Mathf.Sqrt(2 * gravity * maxHeight);

        // ����� ������� �� ������������ ������
        float timeToMaxHeight = verticalSpeed / gravity;

        // ����� ������� � ������������ ������ �� ����
        float timeToTarget = Mathf.Sqrt(2 * (deltaY - maxHeight) / gravity);

        // ������ ����� ������ �� ����
        float totalTime = timeToMaxHeight + timeToTarget;

        // ���� ���������, ���� ������ ����� ������ ������ ����
        return totalTime > 0;
    }

    // ������ ��������� �������� ��� ���������� ����
    Vector3 CalculateInitialVelocity(Vector3 startPos, Vector3 targetPos)
    {
        // �������������� ���������� �� ����
        Vector3 horizontalDirection = targetPos - startPos;
        horizontalDirection.y = 0; // ���������� ������������ ������������

        // ����� ������ �� ���������� ����
        float timeToTarget = CalculateTimeToTarget(startPos, targetPos);

        // �������������� �������� ��� ���������� ����
        Vector3 horizontalVelocity = horizontalDirection / timeToTarget;

        // ������������ �������� ��� ���������� ����
        float deltaY = targetPos.y - startPos.y;
        float verticalVelocity = deltaY / timeToTarget + 0.5f * gravity * timeToTarget;

        // ����������� �������������� � ������������ ��������
        Vector3 initialVelocity = horizontalVelocity + Vector3.up * verticalVelocity;

        return initialVelocity;
    }

    // ������ ������� ������ �� ����
    float CalculateTimeToTarget(Vector3 startPos, Vector3 targetPos)
    {
        // ������������ ���������� �� ����
        float deltaY = targetPos.y - startPos.y;

        // ������������ �������� ��� ���������� ������������ ������
        float verticalSpeed = Mathf.Sqrt(2 * gravity * maxHeight);

        // ����� ������� �� ������������ ������
        float timeToMaxHeight = verticalSpeed / gravity;

        // ����� ������� � ������������ ������ �� ����
        float timeToTarget = Mathf.Sqrt(2 * (deltaY - maxHeight) / gravity);

        // ������ ����� ������ �� ����
        float totalTime = timeToMaxHeight + timeToTarget;

        return totalTime;
    }
}
