
using UnityEngine;

public class Voice : MonoBehaviour
{
    [SerializeField] private AudioSource teacherAudio;

    void Start()
    {
        if (teacherAudio != null)
        {
            teacherAudio.Play();
        }
        else
        {
            Debug.LogWarning("Teacher Audio Source is missing!");
        }
    }
}