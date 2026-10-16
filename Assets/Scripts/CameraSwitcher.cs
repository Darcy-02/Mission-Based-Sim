using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Cameras - DRAG THE WHOLE OBJECT FROM HIERARCHY")]
    public GameObject orbitCameraObject;
    public GameObject followCameraObject;

    [Header("Planet for Follow Camera")]
    public Transform planet;

    private bool isOrbit = true;

    void Start()
    {
        SwitchToOrbit();
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return; 

        if (kb.cKey.wasPressedThisFrame)
            ToggleCamera();
    }

    
    public void ToggleCamera()
    {
        if (isOrbit) SwitchToFollow();
        else SwitchToOrbit();
    }

    public void SwitchToOrbit()
    {
        isOrbit = true;
        if (orbitCameraObject) orbitCameraObject.SetActive(true);
        if (followCameraObject) followCameraObject.SetActive(false);
        Debug.Log("Switched to ORBIT");
    }

    public void SwitchToFollow()
    {
        isOrbit = false;

        if (followCameraObject)
        {

            if (orbitCameraObject)
            {
                followCameraObject.transform.position = orbitCameraObject.transform.position;
                followCameraObject.transform.rotation = orbitCameraObject.transform.rotation;
            }
            followCameraObject.SetActive(true);
        }

        if (orbitCameraObject) orbitCameraObject.SetActive(false);
        Debug.Log("Switched to FOLLOW PLANET");
    }
}