using UnityEngine;

public class Monkey : MonoBehaviour, ISelectable
{
    [Header("Monkey Config")]
    [SerializeField] private float movementSpeed;
    [Tooltip("How close the monkey will try to get to where you click before it stops moving.")]
    [SerializeField] private float accuracyThreshold;
    [SerializeField] private float walkingThreshold;
    private Vector2? target = null;
    private Rigidbody rb;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Select()
    {
        Debug.Log($"{name} was selected");
    }

    public void Deselect()
    {
        Debug.Log($"{name} was deselected");
    }

    public void MoveTo(Vector3 destination)
    {
        target = new Vector2(destination.x, destination.z);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 currentPos2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetPos2D = target.Value;

        if (target.Value != null)
        {
            Vector2 flatDirection = targetPos2D - currentPos2D;
            float distance = flatDirection.magnitude;

            if (distance > accuracyThreshold)
            {
                Vector2 direction = flatDirection.normalized;
                Vector3 direction3D = new Vector3(direction.x, 0, direction.y);
                if (direction3D != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction3D);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
                }

                Vector3 move3D = new Vector3(
                    transform.position.x + direction.x * movementSpeed * Time.fixedDeltaTime,
                    transform.position.y,
                    transform.position.z + direction.y * movementSpeed * Time.fixedDeltaTime
                );
                rb.MovePosition(move3D);
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
                target = null;
            }
        }
        // float rb2DVelocityMagnitude = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude * 10f;
        // bool isWalking = rb2DVelocityMagnitude > walkingThreshold;
        // Debug.Log(rb.linearVelocity.magnitude);
        // animator.SetBool("IsWalking", isWalking);
    }
}
