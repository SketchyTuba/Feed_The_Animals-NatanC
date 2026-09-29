 /*

* GameObject: Player
* Component: PlayerMovement
* Description: Manages the player's movement along the horizontal axis within specified boundaries.
* Reads input callbacks from the Player Input component, updates animation parameters directly on
* the Animator component without using state enums, and handles positioning.
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;

    private float centerToEdge = 22.0f;
    private bool directionXPositive;

    private Animator animator;
    private Vector2 rawInputVector;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    // Called by: Player Input
    public void OnMovementInput(InputAction.CallbackContext ctx)
    {
        rawInputVector = ctx.ReadValue<Vector2>();
        DeterminePlayerDirection(rawInputVector);
    }

    private void DeterminePlayerDirection(Vector2 xValue)
    {
        if (xValue.x > 0)
        {
            directionXPositive = true;
        }
        else if (xValue.x < 0)
        {
            directionXPositive = false;
        }
    }

    private void Move()
    {
        bool isMoving = Mathf.Abs(rawInputVector.x) > 0.01f;

        if (animator != null)
        {
            animator.SetBool("IsRunning", isMoving);
        }

        if (isMoving)
        {
            Vector3 targetPosition = transform.position + new Vector3(rawInputVector.x, 0f, 0f) * playerSpeed * Time.deltaTime;
            targetPosition.x = Mathf.Clamp(targetPosition.x, -centerToEdge, centerToEdge);

            transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        }
    }
}
