
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
            $"PLANET MASS\n{planetRigidbody.mass:F2} kg\n\n" +
            "M  + Increase\nN  - Decrease";
    }
}
