using UnityEngine;

public class PlanetFollowCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform planet; 

    [Header("Follow")]
    public Vector3 offset = new Vector3(0, 5, -15);
    public float followSpeed = 5f;
    public float lookSpeed = 5f;

    void LateUpdate()
    {
        if (planet == null) return;

        
        Vector3 desiredPos = planet.position + planet.TransformDirection(offset);

        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);


        Quaternion desiredRot = Quaternion.LookRotation(planet.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, lookSpeed * Time.deltaTime);
    }
}
