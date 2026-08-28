using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    public AudioMixer audioMixer;

    [Header("Grafica")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle; // Assicurati di collegarlo nell'Inspector


    void Awake()
    {
        // Controllo di sicurezza per evitare doppi AudioListener tra le scene
        AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
        if (listeners.Length > 1)
        {
            for (int i = 1; i < listeners.Length; i++)
            {
                listeners[i].enabled = false;
            }
        }
    }

    void Start()
    {
        Time.timeScale = 1f;

        // --- 1. CARICAMENTO O DEFAULT RISOLUZIONE E FULLSCREEN ---
        // Se non esiste il salvataggio, usa default: Indice 2 (1920x1080) e Fullscreen attivo (1)
        int savedResIndex = PlayerPrefs.GetInt("ResolutionIndex", 2);
        int savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1);
        bool isFullscreen = savedFullscreen == 1;

        Screen.fullScreen = isFullscreen;
        ApplyResolution(savedResIndex, isFullscreen);

        if (resolutionDropdown != null)
        {
            resolutionDropdown.SetValueWithoutNotify(savedResIndex);
        }
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = isFullscreen;
        }

        // --- 2. CARICAMENTO O DEFAULT VOLUMI ---
        // Se non esiste il salvataggio, usa default: 0 (o il valore massimo che preferisci)
        float savedMaster = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float savedSfx = PlayerPrefs.GetFloat("SfxVolume", 0f);

        if (masterSlider != null) masterSlider.value = savedMaster;
        if (sfxSlider != null) sfxSlider.value = savedSfx;

        ApplyMasterVolume(savedMaster);
        ApplySfxVolume(savedSfx);
    }

    // --- MASTER VOLUME ---
    public void OnMasterSliderChanged(float volume)
    {
        ApplyMasterVolume(volume);

        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
        
        Debug.Log("Master Volume salvato a: " + volume);
    }

    private void ApplyMasterVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MasterVolume", volume);
        }
    }

    // --- SFX VOLUME ---
    public void OnSfxSliderChanged(float volume)
    {
        ApplySfxVolume(volume);

        PlayerPrefs.SetFloat("SfxVolume", volume);
        PlayerPrefs.Save();
        
        Debug.Log("SFX Volume salvato a: " + volume);
    }

    private void ApplySfxVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SfxVolume", volume);
        }
    }

    // --- GRAFICA (RISOLUZIONE) ---
    public void SetResolution(int index)
    {
        bool isFull = Screen.fullScreen;
        ApplyResolution(index, isFull);

        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();

        Debug.Log("Risoluzione salvata all'indice: " + index);
    }

    private void ApplyResolution(int index, bool isFull)
    {
        switch (index)
        {
            case 0: Screen.SetResolution(854, 480, isFull); break;
            case 1: Screen.SetResolution(1280, 720, isFull); break;
            case 2: Screen.SetResolution(1920, 1080, isFull); break;
            case 3: Screen.SetResolution(2560, 1440, isFull); break;
        }
    }

    // --- FULLSCREEN ---
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Fullscreen salvato: " + isFullscreen);
    }
}