using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;
//using UnityEngine.SceneManagement;
//using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("ScriptsLinks")]
    public AdForEndGame deathAdManager;
    public SceneLoader sceneLoader;
    public SoundScript soundScript;
    public MoveObject moveObjectSceletalMesh;
    public CameraMove cameraMove;
    Animator animator;
    Animator wingsAnimator;
    Animator shieldAnimator;
    public GroundCheck groundCheck;
    public Coins_Counter TextCoins;
    public CombineBonusManager bonusManager;
    public Jump jumpScript;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    private float speed = 4;
    private float horizontalValue = 0;
    private float timeElapsed = 0;

    [Header("Boost other")]
    public float scaleReductionFactor = 0.5f; // На скільки зменшиться масштаб під час бусту
    public float doubleClickThreshold = 0.3f; // Часовий поріг для подвійного кліку


    [Header("Forward Boosts")]
    public float forwardBoostDuration = 1f; // Тривалість бусту
    public float forwardBoostSpeed = 16f;
    private float lastJumpTime = 0f; // Час останнього натискання
    private float forwardBoostTimer = 0f; // Таймер для контролю бусту
    private int jumpPressCount = 0; // Лічильник натискань
    private bool isForwardBoosting = false;
    public bool forwardBoostTrigger = false;


    [Header("Horizontal Boosts")]
    public float horizontalBoostDuration = 0.1f;
    private float lastClickTimeLeft = -1f; // Час останнього кліку для лівої кнопки
    private float lastClickTimeRight = -1f; // Час останнього кліку для правої кнопки
    private float horizontalBoostTimer = 0f;
    private bool isHorizontalBoosting = false;
    public bool horizontalBoostTrigger = false;

    private float rayDistance = 10f;
    public LayerMask collisionLayer; // Шар для перевірки колізій
    private float wallOffset = 1.85f; // Відступ від стіни для телепортації
    private float lowerRayHeight = 0.5f; // Висота для нижнього променя
    private float upperRayHeight = 2f;   // Висота для верхнього променя


    private Vector3 rayDirection;


    public bool isFalling = false;
    public bool isSliding = false;


    [Header("Speed")]
    private Vector3 moveDirection;
    public float forceMultiplier;
    public float startSpeed = 4f;
    public float endSpeed = 8f;
    public float rotationSpeed = 5f;
    public float durationSpeed = 100f;
    public float downForce = 10f;

    public GameObject[] characters { get; private set; }
    public GameObject DestroyCapsule;
    public GameObject skeletalMesh;
    public Rigidbody rigidbody;
    public CapsuleCollider capsuleCollider;

    [Header("Scale")]
    private float durationScale = 0.1f; // Тривалість зменшення
    public Vector3 minScale = new Vector3(0f, 0f, 0f); // Мінімальний розмір персонажа
    private Vector3 startScale; // Початковий розмір персонажа

    void Awake()
    {
        startSpeed = PlayerPrefs.GetInt("StartSpeed");
        endSpeed = PlayerPrefs.GetInt("EndSpeed");
        Destroy(DestroyCapsule);
        speed = startSpeed;

        startScale = transform.localScale; // Зберігаємо початковий розмір
    }

    public void SetAnimator(Animator animatorSend)
    {
        animator = animatorSend;
    }

    public void OnPressLeftButton()
    {
        // Перевірка на подвійне натискання для лівого напрямку
        if (Time.time - lastClickTimeLeft < doubleClickThreshold)
        {
            rayDirection = Vector3.left;
            PerformImpulse(rayDirection); // Викликаємо нову функцію для імпульсу
        }
        else
        {
            horizontalValue = -1; // Зміна напрямку руху вліво
        }
        lastClickTimeLeft = Time.time;
    }

    public void OnPressRightButton()
    {
        // Перевірка на подвійне натискання для правого напрямку
        if (Time.time - lastClickTimeRight < doubleClickThreshold)
        {
            rayDirection = Vector3.right;
            PerformImpulse(rayDirection); // Викликаємо нову функцію для імпульсу
        }
        else
        {
            horizontalValue = 1; // Зміна напрямку руху вправо
        }
        lastClickTimeRight = Time.time;
    }

    public void UnPressHorizontalButton()
    {
        horizontalValue = 0;

    }
    void PerformImpulse(Vector3 direction)
    {
        RaycastHit hitLower;
        RaycastHit hitUpper;

        Vector3 lowerRayOrigin = transform.position + Vector3.up * lowerRayHeight;
        Vector3 upperRayOrigin = transform.position + Vector3.up * upperRayHeight;

        Debug.DrawRay(lowerRayOrigin, direction * rayDistance, Color.red, 5f);
        Debug.DrawRay(upperRayOrigin, direction * rayDistance, Color.green, 5f);

        bool lowerHit = Physics.Raycast(lowerRayOrigin, direction, out hitLower, rayDistance, collisionLayer);
        bool upperHit = Physics.Raycast(upperRayOrigin, direction, out hitUpper, rayDistance, collisionLayer);

        Vector3 targetPosition;

        if (lowerHit && upperHit)
        {
            if (hitLower.distance <= hitUpper.distance)
            {
                float adjustedWallOffset = Mathf.Min(wallOffset, hitLower.distance - 0.1f);
                targetPosition = new Vector3(hitLower.point.x - direction.normalized.x * adjustedWallOffset, transform.position.y, transform.position.z);
            }
            else
            {
                float adjustedWallOffset = Mathf.Min(wallOffset, hitUpper.distance - 0.1f);
                targetPosition = new Vector3(hitUpper.point.x - direction.normalized.x * adjustedWallOffset, transform.position.y, transform.position.z);
            }
        }
        else if (lowerHit)
        {
            float adjustedWallOffset = Mathf.Min(wallOffset, hitLower.distance - 0.1f);
            targetPosition = new Vector3(hitLower.point.x - direction.normalized.x * adjustedWallOffset, transform.position.y, transform.position.z);
        }
        else if (upperHit)
        {
            float adjustedWallOffset = Mathf.Min(wallOffset, hitUpper.distance - 0.1f);
            targetPosition = new Vector3(hitUpper.point.x - direction.normalized.x * adjustedWallOffset, transform.position.y, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x + direction.normalized.x * rayDistance, transform.position.y, transform.position.z);
        }

        if (!isHorizontalBoosting && horizontalBoostTrigger)
        {
            StartCoroutine(ApplyImpulseTowardsPosition(targetPosition, direction));
        }
    }

    IEnumerator ApplyImpulseTowardsPosition(Vector3 targetPosition, Vector3 direction)
    {
        isHorizontalBoosting = true;
        horizontalBoostTimer = 0f;

        Vector3 startPosition = transform.position;
        Vector3 impulseDirection = (targetPosition - startPosition).normalized;
        float distance = Vector3.Distance(startPosition, targetPosition);

        Vector3 initialScale = skeletalMesh.transform.localScale; // Початковий масштаб
        Vector3 reducedScale = initialScale * scaleReductionFactor; // Мінімальний масштаб

        // Плавний рух під час імпульсу з одночасною зміною масштабу
        while (horizontalBoostTimer < horizontalBoostDuration)
        {
            horizontalBoostTimer += Time.deltaTime;
            float t = horizontalBoostTimer / horizontalBoostDuration;

            // Плавний рух в бік
            float boostAmount = Mathf.SmoothStep(0, distance, t); // Плавний рух в бік

            // Обчислення нової позиції без руху вперед
            Vector3 newPosition = startPosition + impulseDirection * boostAmount;   

            // Встановлення нової позиції
            rigidbody.MovePosition(newPosition);
            Debug.Log($"New Position: {newPosition}");

            // Логіка зміни масштабу
            if (t <= 0.5f)
            {
                float scaleT = t / 0.5f;
                skeletalMesh.transform.localScale = Vector3.Lerp(initialScale, reducedScale, scaleT);
            }
            else
            {
                float scaleT = (t - 0.5f) / 0.5f;
                skeletalMesh.transform.localScale = Vector3.Lerp(reducedScale, initialScale, scaleT);
            }

            yield return null;
        }

        rigidbody.linearVelocity = Vector3.zero; // Обнулення швидкості
        rigidbody.angularVelocity = Vector3.zero; // Обнулення кутової швидкості
                                    
        yield return new WaitForSeconds(0.5f); // Wait for 1 second
        isHorizontalBoosting = false;
    }

    void FixedUpdate()
    {
        if (timeElapsed < durationSpeed)
        {
            timeElapsed += Time.deltaTime;
            speed = Mathf.Lerp(startSpeed, endSpeed, timeElapsed / durationSpeed);
        }

        // Relative rotation for the skeletal mesh.
        if (skeletalMesh != null)
        {
            // Calculate relative direction based on the character's local space.
            Vector3 localDirection = new Vector3(horizontalValue, 0, 1);

            // Ensure the direction vector has enough magnitude to avoid jittery movement.
            if (localDirection.magnitude > 0.1f)
            {
                // Create a target rotation based on the relative direction.
                Quaternion targetRotation = Quaternion.LookRotation(localDirection, Vector3.up);

                // Remove any rotation on the X and Z axes (restrict rotation to Y axis).
                targetRotation.x = 0;
                targetRotation.z = 0;

                // Smoothly rotate the skeletal mesh towards the target rotation.
                skeletalMesh.transform.localRotation = Quaternion.Lerp(skeletalMesh.transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        PlayerMove();
        LogSpeed();
    }

    void PlayerMove()
    {
        float targetMovingSpeed = speed;

        Vector2 targetVelocity = new Vector2(horizontalValue * targetMovingSpeed, 1 * targetMovingSpeed);

        if (rigidbody.isKinematic == false)
        {
            rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
        }
    }

    void LogSpeed()
    {
        if(animator != null)
        {
            animator.SetFloat("Speed", speed / endSpeed);
        }
        //Debug.Log("Current Speed: " + speed + "AnimSpeed: " + speed / endSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Falling(false);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Death();
        }
    }

    public void isGroundFunc(bool isGround)
    {
        if (!isGround)
        {
            if (!isFalling)
            {
                isFalling = true;
                bonusManager.wingsAnimatorFunc(true, jumpScript.airJumpCount);
                Falling(!isGround);
            }
        }
        else
        {
            if (isFalling)
            {
                isFalling = false;
                bonusManager.wingsAnimatorFunc(false, jumpScript.airJumpCount);
                if(wingsAnimator != null)
                {
                    wingsAnimator.SetBool("isFalling", false);
                }
            }
        }
    }

    public void Falling(bool falling)
    {
        if (animator != null)
        {
            animator.SetBool("IsFalling", falling);
        }
    }

    public void Slide()
    {
        if (!isFalling && !isSliding)
        {
            isSliding = true;
            moveObjectSceletalMesh.moveToSet();
            capsuleCollider.height = 0.5f;
            capsuleCollider.center = new Vector3(0f, 0.5f, 0f);
            cameraMove.slideCameraSet();
            if (animator != null)
            {
                animator.SetBool("IsSliding", true);
            }

            StartCoroutine(ResetCollider());
        }
        if (isFalling)
        {
            // Додаємо силу вниз, коли персонаж падає
            rigidbody.AddForce(Vector3.down * downForce, ForceMode.VelocityChange); // 10f — сила, можна налаштувати
            animator.SetBool("IsFalling", false);
        }
    }

    public void DoublePressJump()
    {
        // Оновлюємо кількість натискань на кнопку
        if (Time.time - lastJumpTime > doubleClickThreshold)
        {
            jumpPressCount = 0; // Скидаємо, якщо натискання були з великим інтервалом
        }

        lastJumpTime = Time.time;
        jumpPressCount++;
        // Перевірка на подвійне натискання
        if (jumpPressCount >= 2)
        {
            if (forwardBoostTrigger && !isForwardBoosting)
            {
                StartCoroutine(ForwardImpulseCoroutine());
            }
            jumpPressCount = 0; // Скидаємо лічильник натискань
        }
    }

    private IEnumerator ForwardImpulseCoroutine()
    {
        isForwardBoosting = true; // Активуємо буст
        forwardBoostTimer = 0f; // Скидаємо таймер

        while (forwardBoostTimer < forwardBoostDuration)
        {
            forwardBoostTimer += Time.deltaTime;
            float t = forwardBoostTimer / forwardBoostDuration; // Нормалізуємо таймер від 0 до 1

            float speedForwardBoost = forwardBoostSpeed / speed;
            // Використовуємо синусоїдальну функцію для плавного підйому і спаду
            float boostAmount = Mathf.Sin(t * Mathf.PI) * speedForwardBoost;
            rigidbody.AddForce(transform.forward * boostAmount, ForceMode.VelocityChange); // Додаємо силу вперед

            yield return null; // Чекаємо наступного кадру
        }

        yield return new WaitForSeconds(0.25f); // Wait 
        isForwardBoosting = false; // Деактивуємо буст
    }
    private IEnumerator ResetCollider()
    {
        // Затримка на N секунд
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("SlideDuration"));

        moveObjectSceletalMesh.defoultSet();
        capsuleCollider.height = 2f;
        capsuleCollider.center = new Vector3(0f, 1f, 0f);
        cameraMove.defoultCameraSet();
        if (animator != null)
        {
            isSliding = false;
            animator.SetBool("IsSliding", false);
        }
    }
    public void Death()
    {
        soundScript.deathSound();
        rigidbody.isKinematic = true;
        animator.enabled = false;
        SaveGame();
        Debug.Log(PlayerPrefs.GetInt("Coins"));
        deathAdManager.DeathAd();

        StartCoroutine(ShrinkCharacter());
        StartCoroutine(RestartSceneAfterDelay(1f));
    }
    public void SaveGame()
    {
        int totalCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", totalCoins + TextCoins.Coins);
        PlayerPrefs.Save();
    }
    IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Затримка
        sceneLoader.RestartScene(); // Виклик методу після затримки
    }

    private IEnumerator ShrinkCharacter()
    {
        float elapsedTime = 0f;

        while (elapsedTime < durationScale)
        {
            // t варіюється від 0 до 1 протягом тривалості
            float t = elapsedTime / durationScale;

            // Параболічна функція: y = 1 - (1 - t)^2
            float parabolicFactor = 1 - Mathf.Pow(1 - t, 2);

            // Зменшення масштабу по параболі
            skeletalMesh.transform.localScale = Vector3.Lerp(startScale, minScale, parabolicFactor);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Переконаємося, що після закінчення персонаж має мінімальний розмір
        Destroy(skeletalMesh);
    }
}