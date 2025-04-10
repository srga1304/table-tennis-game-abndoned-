using UnityEngine;

public class BallImpulsePlanner : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public Transform groundCheck; // Точка для проверки земли (обычно на уровне стола)
    public float gravity = 9.81f; // Ускорение свободного падения
    public float maxHeight = 3f; // Максимальная высота полета мяча

    // Функция для расчета и применения импульса к мячу
    public void ApplyImpulseToReachPosition(Vector3 targetPosition)
    {
        // Начальные параметры мяча (по дефолту мяч находится на -8.04x 2.04y -0.78z)
        Vector3 initialPosition = transform.position;

        // Проверяем, достижима ли цель из текущего положения мяча
        if (!IsReachable(targetPosition))
        {
            Debug.LogWarning("Цель недостижима из текущего положения мяча.");
            return;
        }

        // Вычисляем начальную скорость для достижения цели
        Vector3 initialVelocity = CalculateInitialVelocity(initialPosition, targetPosition);

        // Применяем вычисленную начальную скорость к мячу
        ballRigidbody.velocity = initialVelocity;
    }

    // Проверка, достижима ли цель из текущего положения мяча
    bool IsReachable(Vector3 targetPosition)
    {
        // Вертикальное расстояние до цели
        float deltaY = targetPosition.y - transform.position.y;

        // Если цель ниже текущего положения мяча, она недостижима
        if (deltaY < 0)
        {
            return false;
        }

        // Вертикальная скорость для достижения максимальной высоты
        float verticalSpeed = Mathf.Sqrt(2 * gravity * maxHeight);

        // Время подъема до максимальной высоты
        float timeToMaxHeight = verticalSpeed / gravity;

        // Время падения с максимальной высоты до цели
        float timeToTarget = Mathf.Sqrt(2 * (deltaY - maxHeight) / gravity);

        // Полное время полета до цели
        float totalTime = timeToMaxHeight + timeToTarget;

        // Цель достижима, если полное время полета больше нуля
        return totalTime > 0;
    }

    // Расчет начальной скорости для достижения цели
    Vector3 CalculateInitialVelocity(Vector3 startPos, Vector3 targetPos)
    {
        // Горизонтальное расстояние до цели
        Vector3 horizontalDirection = targetPos - startPos;
        horizontalDirection.y = 0; // Игнорируем вертикальную составляющую

        // Время полета до достижения цели
        float timeToTarget = CalculateTimeToTarget(startPos, targetPos);

        // Горизонтальная скорость для достижения цели
        Vector3 horizontalVelocity = horizontalDirection / timeToTarget;

        // Вертикальная скорость для достижения цели
        float deltaY = targetPos.y - startPos.y;
        float verticalVelocity = deltaY / timeToTarget + 0.5f * gravity * timeToTarget;

        // Комбинируем горизонтальную и вертикальную скорости
        Vector3 initialVelocity = horizontalVelocity + Vector3.up * verticalVelocity;

        return initialVelocity;
    }

    // Расчет времени полета до цели
    float CalculateTimeToTarget(Vector3 startPos, Vector3 targetPos)
    {
        // Вертикальное расстояние до цели
        float deltaY = targetPos.y - startPos.y;

        // Вертикальная скорость для достижения максимальной высоты
        float verticalSpeed = Mathf.Sqrt(2 * gravity * maxHeight);

        // Время подъема до максимальной высоты
        float timeToMaxHeight = verticalSpeed / gravity;

        // Время падения с максимальной высоты до цели
        float timeToTarget = Mathf.Sqrt(2 * (deltaY - maxHeight) / gravity);

        // Полное время полета до цели
        float totalTime = timeToMaxHeight + timeToTarget;

        return totalTime;
    }
}
