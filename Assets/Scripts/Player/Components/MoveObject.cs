using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform defoultTransform;
    public Transform moveToTransform;
    public GameObject gameObject;

    public float transitionSpeed = 2f; // Швидкість переходу
    private bool isMovingToTarget = false; // Флаг для переміщення до нової позиції
    private bool isMovingToDefault = false; // Флаг для повернення в початкову позицію
    private float transitionProgress = 0f; // Прогрес переходу

    void Start()
    {
        defoultSet();
    }

    void Update()
    {
        if (isMovingToTarget)
        {
            // Плавний перехід до нової позиції
            transitionProgress += Time.deltaTime * transitionSpeed;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, moveToTransform.position, transitionProgress);
            //gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, moveToTransform.rotation, transitionProgress);

            if (transitionProgress >= 1f)
            {
                isMovingToTarget = false;
                transitionProgress = 0f;
            }
        }

        if (isMovingToDefault)
        {
            // Плавний перехід до початкової позиції
            transitionProgress += Time.deltaTime * transitionSpeed;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, defoultTransform.position, transitionProgress);
            //gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, defoultTransform.rotation, transitionProgress);

            if (transitionProgress >= 1f)
            {
                isMovingToDefault = false;
                transitionProgress = 0f;
            }
        }
    }

    public void moveToSet()
    {
        isMovingToTarget = true;
        isMovingToDefault = false;
        transitionProgress = 0f; // Почати інтерполяцію з нуля
    }

    public void defoultSet()
    {
        isMovingToDefault = true;
        isMovingToTarget = false;
        transitionProgress = 0f; // Почати інтерполяцію з нуля
    }
}
