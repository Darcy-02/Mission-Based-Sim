using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FirstPersonSpaceFlight : MonoBehaviour
{
    [Header("Flight")]
    public float flySpeed = 15f;
    public float boostSpeed = 40f;

    [Header("Look")]
    public float mouseSensitivity = 0.1f;
    public float touchSensitivity = 0.15f;
    public bool canLook = true;

    [Header("Mobile (set by UI buttons)")]
    public Vector2 mobileMove;      
    public float mobileVertical;    

    private float pitch;
    private float yaw;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;

        
        LockCursor(!Application.isMobilePlatform);
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        Mouse mouse = Mouse.current;

        if (kb != null && kb.escapeKey.wasPressedThisFrame)
            LockCursor(false);

        if (mouse != null &&
            mouse.leftButton.wasPressedThisFrame &&
            Cursor.lockState != CursorLockMode.Locked)
        {
            bool overUI = EventSystem.current != null &&
                          EventSystem.current.IsPointerOverGameObject();
            if (!overUI && !Application.isMobilePlatform)
                LockCursor(true);
        }

       
        if (canLook)
        {
            Vector2 lookDelta = Vector2.zero;

            if (mouse != null &&
                (Cursor.lockState == CursorLockMode.Locked ||
                 mouse.rightButton.isPressed))
            {
                lookDelta += mouse.delta.ReadValue() * mouseSensitivity;
            }

            
            if (Touchscreen.current != null)
            {
                foreach (var touch in Touchscreen.current.touches)
                {
                    if (!touch.press.isPressed) continue;

                    Vector2 pos = touch.position.ReadValue();
                    int id = touch.touchId.ReadValue();

                    bool overUI = EventSystem.current != null &&
                                  EventSystem.current.IsPointerOverGameObject(id);

                    if (!overUI && pos.x < Screen.width * 0.5f)
                        lookDelta += touch.delta.ReadValue() * touchSensitivity;
                }
            }

            yaw += lookDelta.x;
            pitch -= lookDelta.y;
            pitch = Mathf.Clamp(pitch, -89f, 89f);
            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }


        bool boost = kb != null &&
                     (kb.leftShiftKey.isPressed || kb.rightShiftKey.isPressed);
        float speed = boost ? boostSpeed : flySpeed;

        Vector3 movement = Vector3.zero;

        if (kb != null)
        {
            if (kb.wKey.isPressed) movement += transform.forward;
            if (kb.sKey.isPressed) movement -= transform.forward;
            if (kb.dKey.isPressed) movement += transform.right;
            if (kb.aKey.isPressed) movement -= transform.right;

            if (kb.spaceKey.isPressed) movement += Vector3.up;
            if (kb.leftCtrlKey.isPressed || kb.rightCtrlKey.isPressed ||
                kb.cKey.isPressed || kb.qKey.isPressed)
                movement += Vector3.down;
        }

  
        movement += transform.forward * mobileMove.y;
        movement += transform.right * mobileMove.x;
        movement += Vector3.up * mobileVertical;

        transform.position += movement * speed * Time.deltaTime;
    }

   
    public void MoveForwardDown() { mobileMove.y = 1f; }
    public void MoveBackDown() { mobileMove.y = -1f; }
    public void MoveLeftDown() { mobileMove.x = -1f; }
    public void MoveRightDown() { mobileMove.x = 1f; }
    public void MoveStop() { mobileMove = Vector2.zero; }
    public void UpDown() { mobileVertical = 1f; }
    public void DownDown() { mobileVertical = -1f; }
    public void VerticalStop() { mobileVertical = 0f; }

    public void LockCursor(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canLook = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canLook = Application.isMobilePlatform; // touch can always look
        }
    }

    public void SetCanLook(bool value)
    {
        canLook = value;
        if (!value) Cursor.lockState = CursorLockMode.None;
    }


}