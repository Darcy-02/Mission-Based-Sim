using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlanetCollisionEffects : MonoBehaviour
{
    [Header("Feedback")]
    public TMP_Text messageText;
    public AudioSource hitSound;
    public ParticleSystem hitParticles;

    [Header("Tags")]
    [Tooltip("Only react to collisions with objects that have this tag. Leave empty to react to ANY collision.")]
    public string collisionTag = "";
    public string dangerZoneTag = "DangerZone";

   
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name +
                  " (tag: " + collision.gameObject.tag + ")");

        if (!string.IsNullOrEmpty(collisionTag) &&
            !collision.gameObject.CompareTag(collisionTag))
            return;

        Show("The masses collided! In real life, objects this close would merge.");
        PlayHitSound();
        PlayParticles(collision.GetContact(0).point);
    }

    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered: " + other.gameObject.name +
                  " (tag: " + other.gameObject.tag + ")");

        if (!other.CompareTag(dangerZoneTag)) return;

        Show("You entered the strong gravity zone. The pull is much stronger here.");
        PlayHitSound();
        PlayParticles(transform.position);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(dangerZoneTag)) return;

        Debug.Log("Left DangerZone");
        Show("You left the strong gravity zone.");
    }

    void Show(string message)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message;
        }
        else
        {
            Debug.LogWarning("Message Text is not assigned on PlanetCollisionEffects.");
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null && hitSound.clip != null)
            hitSound.Play();
    }

    void PlayParticles(Vector3 position)
    {
        if (hitParticles == null) return;

        hitParticles.transform.position = position;
        hitParticles.Play();
    }
}