using UnityEngine;

public class ImpulseApplier : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public float impulseForce = 5f; // Сила импульса по умолчанию

    void Update()
    {
        // Пример: применение импульса в выбранном направлении по нажатию клавиши
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Например, применение импульса вправо
            ApplyImpulse(Vector3.right * impulseForce);
        }

        // Пример: применение импульса влево
        if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyImpulse(Vector3.left * impulseForce);
        }

        // Пример: применение импульса вверх
        if (Input.GetKeyDown(KeyCode.W))
        {
            ApplyImpulse(Vector3.forward * impulseForce);
        }

        // Пример: применение импульса вниз
        if (Input.GetKeyDown(KeyCode.S))
        {
            ApplyImpulse(Vector3.back * impulseForce);
        }
    }

    void ApplyImpulse(Vector3 direction)
    {
        ballRigidbody.AddForce(direction, ForceMode.Impulse);
    }

    // Метод для изменения силы импульса через интерфейс Unity
    public void SetImpulseForce(float newForce)
    {
        impulseForce = newForce;
    }
}
