using UnityEngine;

public class SmoothCameraMoveAndRotate : MonoBehaviour
{
    public float moveSpeed = 2.0f;     // Швидкість переміщення та повороту
    public float maxFOV = 90.0f;       // Максимальне значення FOV на середині шляху
    public float normalFOV = 60.0f;    // Нормальне значення FOV

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Camera cameraComponent;    // Компонент камери для зміни FOV
    private Transform targetPosition;  // Цільова позиція, яка буде передаватися через метод
    private bool isMoving = false;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        cameraComponent = GetComponent<Camera>(); // Отримуємо компонент камери
        cameraComponent.fieldOfView = normalFOV;  // Встановлюємо початкове значення FOV
    }

    void Update()
    {
        if (isMoving)
        {
            MoveAndRotateCamera();
        }
    }

    // Метод, який викликається для переміщення камери і задання цільового Transform
    public void StartMoving(Transform newTarget)
    {
        targetPosition = newTarget;
        startPosition = transform.position;    // Зберігаємо поточну позицію камери
        startRotation = transform.rotation;    // Зберігаємо поточний поворот камери
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, targetPosition.position);
        isMoving = true;
    }

    void MoveAndRotateCamera()
    {
        // Обчислюємо пройдену частину шляху на основі часу
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        // Використовуємо Mathf.SmoothStep для плавного старту та завершення руху
        float smoothStep = Mathf.SmoothStep(0.0f, 1.0f, fractionOfJourney);

        // Плавно переміщуємо камеру з початкової позиції до цільової
        transform.position = Vector3.Lerp(startPosition, targetPosition.position, smoothStep);

        // Плавно повертаємо камеру до цільового повороту
        Quaternion targetRotation = targetPosition.rotation;
        transform.rotation = Quaternion.Lerp(startRotation, targetRotation, smoothStep);

        // Змінюємо FOV: плавне збільшення до середини шляху, потім плавне зменшення
        if (fractionOfJourney <= 0.5f)
        {
            // На першій половині шляху плавно збільшуємо FOV до maxFOV
            float fovStep = Mathf.SmoothStep(normalFOV, maxFOV, fractionOfJourney * 2);
            cameraComponent.fieldOfView = fovStep;
        }
        else
        {
            // На другій половині шляху плавно зменшуємо FOV до normalFOV
            float fovStep = Mathf.SmoothStep(maxFOV, normalFOV, (fractionOfJourney - 0.5f) * 2);
            cameraComponent.fieldOfView = fovStep;
        }

        // Зупиняємо рух, якщо камера досягла цільової позиції
        if (fractionOfJourney >= 1.0f)
        {
            isMoving = false;
            cameraComponent.fieldOfView = normalFOV; // Повертаємо FOV до нормального після завершення руху
        }
    }
}
