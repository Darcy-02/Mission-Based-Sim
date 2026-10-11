
using UnityEngine;

public class FirstPersonSpaceFlight : MonoBehaviour
{
    [Header("Flight")]
    public float flySpeed = 15f;
    public float boostSpeed = 40f;

    [Header("Look")]
    public float mouseSensitivity = 2f;

    private float pitch;
    private float yaw;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;

        LockCursor(true);
    }

    void Update()
    {
        // Unlock the mouse to interact with the editor or UI
        if (Input.GetKeyDown(KeyCode.Escape))
            LockCursor(false);

        if (Input.GetMouseButtonDown(0) &&
            Cursor.lockState != CursorLockMode.Locked)
            LockCursor(true);

        // Look around
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, -89f, 89f);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        // Fly in the direction you're looking
        float speed = Input.GetKey(KeyCode.LeftShift)
            ? boostSpeed : flySpeed;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement =
            transform.right * horizontal +
            transform.forward * vertical;

        // Space goes up; Ctrl goes down
        if (Input.GetKey(KeyCode.Space))
            movement += Vector3.up;

        if (Input.GetKey(KeyCode.LeftControl))
            movement += Vector3.down;

        transform.position += movement * speed * Time.deltaTime;
    }

    void LockCursor(bool locked)
    {
        Cursor.lockState = locked
            ? CursorLockMode.Locked
            : CursorLockMode.None;

        Cursor.visible = !locked;
    }
}
