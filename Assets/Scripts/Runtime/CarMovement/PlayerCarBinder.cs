using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCarBinder : MonoBehaviour
{
    [SerializeField] private Transform carBody;
    [SerializeField] private Transform[] carWheels; // Все колеса
    [SerializeField] private Collider[] rearWheelColliders; // Коллайдеры задних колес
    [SerializeField] private float raycastLength = 0.5f; // Длина рейкаста
    [Header("Car Settings")]
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float brakeForce = 50f;
    [SerializeField] private float turnSpeed = 5f;

    private PlayerCarController _carController;

    [Inject]
    public void Construct(PlayerCarController carController)
    {
        _carController = carController;
    }

    private void Start()
    {
        Rigidbody rb = carBody.GetComponent<Rigidbody>();

        // Создаем объекты проверки касания задних колес
        WheelContactChecker[] rearWheelCheckers = new WheelContactChecker[rearWheelColliders.Length];
        for (int i = 0; i < rearWheelColliders.Length; i++)
        {
            rearWheelCheckers[i] = new WheelContactChecker(rearWheelColliders[i], raycastLength);
        }

        CarModel model = new CarModel(carBody, carWheels, rearWheelCheckers, rb, maxSpeed, acceleration, brakeForce, turnSpeed);
        _carController.Initialize(model);
    }

    private void Update()
    {
        HandleInput();
        _carController.OnUpdate();
    }

    private void HandleInput()
    {
        float moveInput = 0f;
        float turnInput = 0f;

        // Обработка ввода для движения вперед/назад
        if (Input.GetKey(KeyCode.W))
        {
            moveInput = 1f; // Ускорение вперед
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveInput = -1f; // Торможение
        }

        // Обработка ввода для поворота
        if (Input.GetKey(KeyCode.A))
        {
            turnInput = -1f; // Поворот влево
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnInput = 1f; // Поворот вправо
        }

        _carController.UpdateInputs(moveInput, turnInput);
    }
}





