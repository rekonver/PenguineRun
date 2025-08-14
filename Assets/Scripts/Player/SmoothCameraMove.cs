using UnityEngine;

public class SmoothCameraMoveAndRotate : MonoBehaviour
{
    public float moveSpeed = 2.0f;     // �������� ���������� �� ��������
    public float maxFOV = 90.0f;       // ����������� �������� FOV �� ������� �����
    public float normalFOV = 60.0f;    // ��������� �������� FOV

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Camera cameraComponent;    // ��������� ������ ��� ���� FOV
    private Transform targetPosition;  // ֳ����� �������, ��� ���� ������������ ����� �����
    private bool isMoving = false;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        cameraComponent = GetComponent<Camera>(); // �������� ��������� ������
        cameraComponent.fieldOfView = normalFOV;  // ������������ ��������� �������� FOV
    }

    void Update()
    {
        if (isMoving)
        {
            MoveAndRotateCamera();
        }
    }

    // �����, ���� ����������� ��� ���������� ������ � ������� ��������� Transform
    public void StartMoving(Transform newTarget)
    {
        targetPosition = newTarget;
        startPosition = transform.position;    // �������� ������� ������� ������
        startRotation = transform.rotation;    // �������� �������� ������� ������
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, targetPosition.position);
        isMoving = true;
    }

    void MoveAndRotateCamera()
    {
        // ���������� �������� ������� ����� �� ����� ����
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        // ������������� Mathf.SmoothStep ��� �������� ������ �� ���������� ����
        float smoothStep = Mathf.SmoothStep(0.0f, 1.0f, fractionOfJourney);

        // ������ ��������� ������ � ��������� ������� �� �������
        transform.position = Vector3.Lerp(startPosition, targetPosition.position, smoothStep);

        // ������ ��������� ������ �� ��������� ��������
        Quaternion targetRotation = targetPosition.rotation;
        transform.rotation = Quaternion.Lerp(startRotation, targetRotation, smoothStep);

        // ������� FOV: ������ ��������� �� �������� �����, ���� ������ ���������
        if (fractionOfJourney <= 0.5f)
        {
            // �� ������ ������� ����� ������ �������� FOV �� maxFOV
            float fovStep = Mathf.SmoothStep(normalFOV, maxFOV, fractionOfJourney * 2);
            cameraComponent.fieldOfView = fovStep;
        }
        else
        {
            // �� ����� ������� ����� ������ �������� FOV �� normalFOV
            float fovStep = Mathf.SmoothStep(maxFOV, normalFOV, (fractionOfJourney - 0.5f) * 2);
            cameraComponent.fieldOfView = fovStep;
        }

        // ��������� ���, ���� ������ ������� ������� �������
        if (fractionOfJourney >= 1.0f)
        {
            isMoving = false;
            cameraComponent.fieldOfView = normalFOV; // ��������� FOV �� ����������� ���� ���������� ����
        }
    }
}
