using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class IncreasePlanetMass : MonoBehaviour
{
    [Header("Planet Mass")]
    public float increaseAmount = 1f;
    public float decreaseAmount = 1f;
    public float minimumMass = 0f;

    private Rigidbody planetRigidbody;

    [Header("Voice")]
    public AudioSource Audio;
    public AudioClip increaseVoice;
    public AudioClip decreaseVoice;

    [Header("Captions")]
    public TMP_Text Caption;
    public GameObject captionObject;

    void Awake()
    {
        planetRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        ShowCaption("Welcome, explorer! Change the planet's mass and " +
            "observe how our spacetime model responds.");
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.mKey.wasPressedThisFrame) IncreaseMass();
        if (kb.nKey.wasPressedThisFrame) DecreaseMass();

        if (kb.deleteKey.wasPressedThisFrame) HideCaption();
        if (kb.iKey.wasPressedThisFrame)
        {
            if (captionObject != null) captionObject.SetActive(true);
        }
    }

    // Public so mobile UI buttons can call them from OnClick
    public void IncreaseMass()
    {
        ChangeMass(increaseAmount, increaseVoice);
        ShowCaption("You've increased the planet's mass. Notice how the spacetime grid changes.\nIs increasing the mass alone enough to make a planet move?");
    }

    public void DecreaseMass()
    {
        ChangeMass(-decreaseAmount, decreaseVoice);
        ShowCaption("You've decreased the planet's mass. Observe how the spacetime grid responds.\nWhat rule is the simulation using?");
    }

    public void HideCaption()
    {
        if (captionObject != null)
            captionObject.SetActive(false);
    }

    void ShowCaption(string message)
    {
        if (Caption != null)
            Caption.text = message;

        if (captionObject != null)
            captionObject.SetActive(true);
    }

    void ChangeMass(float amount, AudioClip voice)
    {
        if (planetRigidbody == null)
        {
            Debug.LogWarning("Planet Rigidbody is missing!");
            return;
        }

        planetRigidbody.mass =
            Mathf.Max(minimumMass, planetRigidbody.mass + amount);

        if (Audio != null && voice != null && !Audio.isPlaying)
        {
            Audio.clip = voice;
            Audio.Play();
        }
    }
}