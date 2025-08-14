using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;
    public int maxAirJump = 0;
    public int airJumpCount = 0;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    public GroundCheck groundCheck;
    FirstPersonMovement player;
    public CombineBonusManager bonusManager;

    void Awake()
    {
        player = GetComponent<FirstPersonMovement>();
        rigidbody = GetComponent<Rigidbody>();
    }
    public void JumpPress()
    {
        if (player.isSliding == false && (groundCheck.isGrounded || airJumpCount > 0))
        {
            if (!groundCheck.isGrounded)
            {
                airJumpCount -= 1;
            }

            player.Falling(true);

            Vector3 velocity = rigidbody.linearVelocity;
            velocity.y = 0;
            rigidbody.linearVelocity = velocity;

            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
        if (airJumpCount <= 0)
        {
            bonusManager.wingsAnimatorFunc(false,0);
        }
    }
    public void recoveryAirJump()
    {
        airJumpCount = maxAirJump;
    }
}
