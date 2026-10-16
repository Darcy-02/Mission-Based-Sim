using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    [Header("Main References")]
    public FirstPersonSpaceFlight flightScript; 
    public GameObject hoverTooltip;
    public TextMeshProUGUI hoverText;

    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject guidePanel;

    [Header("Settings")]
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;

    void Start()
    {
        if (hoverTooltip != null) hoverTooltip.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (guidePanel != null) guidePanel.SetActive(false);

        if (volumeSlider != null)
        {
            float saved = PlayerPrefs.GetFloat("Volume", 1f);
            volumeSlider.value = saved;
            AudioListener.volume = saved;
            UpdateVolumeText(saved);
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

      
        if (flightScript != null)
            flightScript.LockCursor(false);
    }

    public void OpenSettings()
    {
        if (settingsPanel == null) { Debug.LogError("SettingsPanel NOT assigned!"); return; }
        settingsPanel.SetActive(true);
        guidePanel.SetActive(false);
        if (flightScript != null) flightScript.LockCursor(false);
    }

    public void OpenGuide()
    {
        guidePanel.SetActive(true);
        settingsPanel.SetActive(false);
        if (flightScript != null) flightScript.LockCursor(false);
    }

    public void CloseAllPanels()
    {
        settingsPanel.SetActive(false);
        guidePanel.SetActive(false);
        if (hoverTooltip != null) hoverTooltip.SetActive(false);
        if (flightScript != null) flightScript.LockCursor(false); 
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
        UpdateVolumeText(value);
    }

    void UpdateVolumeText(float v)
    {
        if (volumeValueText != null)
            volumeValueText.text = $"Volume: {Mathf.RoundToInt(v * 100)}%";
    }

    public void ShowHover(string message)
    {
        if (hoverTooltip == null) return;
        hoverText.text = message;
        hoverTooltip.SetActive(true);
    }

    public void HideHover()
    {
        if (hoverTooltip != null)
            hoverTooltip.SetActive(false);
    }

   
}