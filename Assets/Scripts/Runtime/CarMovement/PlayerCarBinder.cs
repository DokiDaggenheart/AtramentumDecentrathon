using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCarBinder : MonoBehaviour
{
    [SerializeField] private Transform carBody;
    [SerializeField] private Transform[] carWheels; // ��� ������
    [SerializeField] private Collider[] rearWheelColliders; // ���������� ������ �����
    [SerializeField] private float raycastLength = 0.5f; // ����� ��������
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

        // ������� ������� �������� ������� ������ �����
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

        // ��������� ����� ��� �������� ������/�����
        if (Input.GetKey(KeyCode.W))
        {
            moveInput = 1f; // ��������� ������
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveInput = -1f; // ����������
        }

        // ��������� ����� ��� ��������
        if (Input.GetKey(KeyCode.A))
        {
            turnInput = -1f; // ������� �����
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnInput = 1f; // ������� ������
        }

        _carController.UpdateInputs(moveInput, turnInput);
    }
}





