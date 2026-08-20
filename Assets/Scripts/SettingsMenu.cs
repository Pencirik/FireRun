using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Master Volume")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private TMP_InputField masterInputField;

    [Header("SFX Volume")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_InputField sfxInputField;

    [Header("Grafica")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    void Start()
    {
        // Imposta valori iniziali (es. 100) e aggiorna i testi dei campi
        if (masterSlider != null) 
        {
            masterSlider.value = 100f;
            if (masterInputField != null) masterInputField.text = "100";
        }
        
        if (sfxSlider != null) 
        {
            sfxSlider.value = 100f;
            if (sfxInputField != null) sfxInputField.text = "100";
        }
    }

    // --- MASTER VOLUME ---

    // Chiamato quando muovi lo SLIDER
    public void OnMasterSliderChanged(float value)
    {
        if (masterInputField != null)
        {
            masterInputField.text = Mathf.RoundToInt(value).ToString();
        }
        
        Debug.Log("Master Volume impostato a: " + value);
    }

    // Chiamato quando SCRIVI nel testo
    public void OnMasterInputChanged(string textValue)
    {
        if (float.TryParse(textValue, out float value))
        {
            value = Mathf.Clamp(value, 0f, 100f);
            if (masterSlider != null)
            {
                masterSlider.value = value;
            }
        }
    }

    // --- SFX VOLUME ---

    public void OnSfxSliderChanged(float value)
    {
        if (sfxInputField != null)
        {
            sfxInputField.text = Mathf.RoundToInt(value).ToString();
        }
        Debug.Log("SFX Volume impostato a: " + value);
    }

    public void OnSfxInputChanged(string textValue)
    {
        if (float.TryParse(textValue, out float value))
        {
            value = Mathf.Clamp(value, 0f, 100f);
            if (sfxSlider != null)
            {
                sfxSlider.value = value;
            }
        }
    }

    // --- GRAFICA (RISOLUZIONE) ---
    public void SetResolution(int index)
    {
        switch (index)
        {
            case 0: Screen.SetResolution(854, 480, Screen.fullScreen); break;
            case 1: Screen.SetResolution(1280, 720, Screen.fullScreen); break;
            case 2: Screen.SetResolution(1920, 1080, Screen.fullScreen); break;
            case 3: Screen.SetResolution(2560, 1440, Screen.fullScreen); break;
        }
        Debug.Log("Risoluzione cambiata all'indice: " + index);
    }
}