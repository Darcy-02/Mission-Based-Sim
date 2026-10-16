
using UnityEngine;
using TMPro;

public class PlanetMassDisplay : MonoBehaviour
{
    [SerializeField] private Rigidbody planetRigidbody;
    [SerializeField] private TMP_Text massValueText;

    void Update()
    {
        if (planetRigidbody == null || massValueText == null)
            return;

        massValueText.text =
            $"Mass of the Planet\n{planetRigidbody.mass:F2} kg\n\n";
    }
}
