using UnityEngine;

public class LaunchBallAtCoordinates : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public Transform tableSurface; // Ссылка на объект стола или плоскости, на которой находится стол

    public Vector3 targetCoordinates = new Vector3(0f, 0f, 0f); // Координаты приземления мяча (x, z)
    public float speedMultiplier = 0.5f; // Множитель для уменьшения силы удара

    void Start()
    {
        LaunchBallToCoordinates(targetCoordinates);
    }

    void LaunchBallToCoordinates(Vector3 targetPosition)
    {
        // Проверяем, есть ли ссылка на объект стола
        if (tableSurface == null)
        {
            Debug.LogError("Table surface reference is not set!");
            return;
        }

        // Получаем высоту стола
        float tableHeight = tableSurface.position.y;

        // Начальная высота мяча
        float ballStartHeight = ballRigidbody.position.y;

        // Расстояние по вертикали до высоты стола
        float verticalDistance = tableHeight - ballStartHeight;

        // Рассчитываем время полета для достижения целевой высоты (стола)
        float timeToTarget = CalculateTimeToTarget(verticalDistance);

        // Рассчитываем начальную вертикальную скорость
        float initialVerticalSpeed = CalculateVerticalSpeed(verticalDistance, timeToTarget);

        // Рассчитываем горизонтальную скорость для достижения целевых X и Z координат
        Vector3 horizontalDistance = new Vector3(targetPosition.x - ballRigidbody.position.x, 0, targetPosition.z - ballRigidbody.position.z);
        Vector3 horizontalSpeed = horizontalDistance / timeToTarget;

        // Применяем множитель скорости для уменьшения силы удара
        Vector3 launchVelocity = new Vector3(horizontalSpeed.x, initialVerticalSpeed, horizontalSpeed.z) * speedMultiplier;

        // Применяем скорость к Rigidbody мяча
        ballRigidbody.velocity = launchVelocity;

        // Выводим информацию о запуске для отладки
        Debug.Log($"Ball launched with velocity: {launchVelocity}");
    }

    float CalculateVerticalSpeed(float verticalDistance, float timeToTarget)
    {
        float g = Mathf.Abs(Physics.gravity.y); // Ускорение свободного падения (гравитация)

        // Учитываем направление скорости: вверх или вниз
        if (verticalDistance > 0)
        {
            // Если цель выше начальной точки
            return (verticalDistance + 0.5f * g * Mathf.Pow(timeToTarget, 2)) / timeToTarget;
        }
        else
        {
            // Если цель ниже начальной точки
            return (verticalDistance - 0.5f * g * Mathf.Pow(timeToTarget, 2)) / timeToTarget;
        }
    }

    float CalculateTimeToTarget(float verticalDistance)
    {
        float g = Mathf.Abs(Physics.gravity.y); // Ускорение свободного падения (гравитация)
        return Mathf.Sqrt(2 * Mathf.Abs(verticalDistance) / g);
    }

    bool IsValidVelocity(Vector3 velocity)
    {
        // Проверяем, что скорость не содержит NaN или бесконечность
        return !float.IsNaN(velocity.x) && !float.IsNaN(velocity.y) && !float.IsNaN(velocity.z) &&
               !float.IsInfinity(velocity.x) && !float.IsInfinity(velocity.y) && !float.IsInfinity(velocity.z);
    }
}
