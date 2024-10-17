using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform _camPos;

    public float _speed = 5f;
    public float _rotationSpeed = 10f;
    public float _dashForce;

    public float _turnSpeed = 10f;

    public LayerMask _groundMask;

    public float speed = 5f; // Speed at which the object moves

    void Start()
    {
        _camPos = FindObjectOfType<Camera>().transform;

        transform.rotation = Quaternion.Euler(0, _camPos.rotation.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            // Get the mouse position in viewport coordinates
            Vector3 mousePositionViewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ViewportPointToRay(mousePositionViewport);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundMask))
            {
                // Get the point on the ground where the ray hits
                Vector3 targetPosition = hit.point;

                // Calculate the direction from the object to the target position
                Vector3 direction = (targetPosition - transform.position).normalized;

                // Move the object towards the target position
                transform.position += direction * speed * Time.deltaTime;

                // Calculate rotation only if moving
                if (direction != Vector3.zero)
                {
                    // Ignore rotation on Y-axis to prevent looking down
                    Vector3 targetDirection = new Vector3(direction.x, 0f, direction.z);

                    // Rotate towards the direction of movement
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
