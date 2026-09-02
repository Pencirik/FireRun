using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    public AudioMixer audioMixer;

    [Header("Volume Toggles")]
    [SerializeField] private Toggle masterMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;
    [SerializeField] private Toggle musicMuteToggle;

    // Variabili per ricordare il volume prima di mutare
    private float lastMasterVolume = 0f;
    private float lastSfxVolume = 0f;
    private float lastMusicVolume = 0f;

    [Header("Grafica")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle; 


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

        // Caricamento dati grafici
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

        // Caricamento volumi
        float savedMaster = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float savedSfx = PlayerPrefs.GetFloat("SfxVolume", 0f);
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0f);

        // Impostiamo lo stato iniziale dei toggle e dei volumi
        bool isMasterMuted = (savedMaster <= -80f);
        bool isSfxMuted = (savedSfx <= -80f);
        bool isMusicMuted = (savedMusic <= -80f);

        if (masterMuteToggle != null) masterMuteToggle.SetIsOnWithoutNotify(isMasterMuted);
        if (sfxMuteToggle != null) sfxMuteToggle.SetIsOnWithoutNotify(isSfxMuted);
        if (musicMuteToggle != null) musicMuteToggle.SetIsOnWithoutNotify(isMusicMuted);

        if (masterSlider != null) 
        {
            masterSlider.value = isMasterMuted ? 0f : savedMaster; // Se è mutato, teniamo lo slider a un valore sensato
            if (isMasterMuted) lastMasterVolume = savedMaster == -80f ? 0f : savedMaster;
            else lastMasterVolume = savedMaster;
        }

        if (sfxSlider != null) 
        {
            sfxSlider.value = isSfxMuted ? 0f : savedSfx;
            if (isSfxMuted) lastSfxVolume = savedSfx == -80f ? 0f : savedSfx;
            else lastSfxVolume = savedSfx;
        }

        if (musicSlider != null) 
        {
            musicSlider.value = isMusicMuted ? 0f : savedMusic;
            if (isMusicMuted) lastMusicVolume = savedMusic == -80f ? 0f : savedMusic;
            else lastMusicVolume = savedMusic;
        }

        ApplyMasterVolume(savedMaster);
        ApplySfxVolume(savedSfx);
        ApplyMusicVolume(savedMusic);
    }

    // MASTER VOLUME 
    public void OnMasterSliderChanged(float volume)
    {
        // Se muoviamo lo slider, disattiviamo il muto automaticamente
        if (masterMuteToggle != null && masterMuteToggle.isOn)
        {
            masterMuteToggle.SetIsOnWithoutNotify(false);
        }

        lastMasterVolume = volume;
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

    // Master mute
    public void OnMasterMuteToggled(bool isMuted)
    {
        if (isMuted)
        {
            if (masterSlider != null && masterSlider.value > -80f)
            {
                lastMasterVolume = masterSlider.value;
            }

            ApplyMasterVolume(-80f);
            PlayerPrefs.SetFloat("MasterVolume", -80f);
            PlayerPrefs.Save();
        }
        else
        {
            ApplyMasterVolume(lastMasterVolume);
            PlayerPrefs.SetFloat("MasterVolume", lastMasterVolume);
            PlayerPrefs.Save();
        }
    }

    // SFX VOLUME
    public void OnSfxSliderChanged(float volume)
    {
        if (sfxMuteToggle != null && sfxMuteToggle.isOn)
        {
            sfxMuteToggle.SetIsOnWithoutNotify(false);
        }

        lastSfxVolume = volume;
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

    // SFX mute
    public void OnSfxMuteToggled(bool isMuted)
    {
        if (isMuted)
        {
            if (sfxSlider != null && sfxSlider.value > -80f)
            {
                lastSfxVolume = sfxSlider.value;
            }

            ApplySfxVolume(-80f);
            PlayerPrefs.SetFloat("SfxVolume", -80f);
            PlayerPrefs.Save();
        }
        else
        {
            ApplySfxVolume(lastSfxVolume);
            PlayerPrefs.SetFloat("SfxVolume", lastSfxVolume);
            PlayerPrefs.Save();
        }
    }

    // Music volume
    public void OnMusicSliderChanged(float volume)
    {
        if (musicMuteToggle != null && musicMuteToggle.isOn)
        {
            musicMuteToggle.SetIsOnWithoutNotify(false);
        }

        lastMusicVolume = volume;
        ApplyMusicVolume(volume);

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        
        Debug.Log("Music Volume salvato a: " + volume);
    }

    private void ApplyMusicVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("MusicVolume", volume);
        }
    }

    // Music mute
    public void OnMusicMuteToggled(bool isMuted)
    {
        if (isMuted)
        {
            if (musicSlider != null && musicSlider.value > -80f)
            {
                lastMusicVolume = musicSlider.value;
            }

            ApplyMusicVolume(-80f);
            PlayerPrefs.SetFloat("MusicVolume", -80f);
            PlayerPrefs.Save();
        }
        else
        {
            ApplyMusicVolume(lastMusicVolume);
            PlayerPrefs.SetFloat("MusicVolume", lastMusicVolume);
            PlayerPrefs.Save();
        }
    }

    // GRAFICA
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

    // FULLSCREEN
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Fullscreen salvato: " + isFullscreen);
    }
}