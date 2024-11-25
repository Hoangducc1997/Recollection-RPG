using UnityEngine;
using UnityEngine.UI;

public class SliderSetting : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button sfxToggleButton;

    [SerializeField] private Sprite musicOnIcon;
    [SerializeField] private Sprite musicOffIcon;
    [SerializeField] private Sprite sfxOnIcon;
    [SerializeField] private Sprite sfxOffIcon;

    private void Start()
    {
        // Gán giá trị ban đầu
        musicSlider.value = MusicSettings.Instance.MusicVolume;
        sfxSlider.value = MusicSettings.Instance.SFXVolume;
        UpdateButtonIcons();

        // Thêm sự kiện cho slider
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);

        // Thêm sự kiện cho nút tắt/mở
        musicToggleButton.onClick.AddListener(ToggleMusicMute);
        sfxToggleButton.onClick.AddListener(ToggleSFXMute);
    }

    private void OnMusicSliderChanged(float value)
    {
        MusicSettings.Instance.SetMusicVolume(value);
        AudioManager.Instance.GetMusicAudioSource().volume = value;
        UpdateButtonIcons();
    }

    private void OnSFXSliderChanged(float value)
    {
        MusicSettings.Instance.SetSFXVolume(value);
        AudioManager.Instance.GetVFXAudioSource().volume = value;
        UpdateButtonIcons();
    }

    private void ToggleMusicMute()
    {
        MusicSettings.Instance.ToggleMusicMute();
        musicSlider.value = MusicSettings.Instance.MusicVolume;
        AudioManager.Instance.GetMusicAudioSource().volume = MusicSettings.Instance.MusicVolume;
        UpdateButtonIcons();
    }

    private void ToggleSFXMute()
    {
        MusicSettings.Instance.ToggleSFXMute();
        sfxSlider.value = MusicSettings.Instance.SFXVolume;
        AudioManager.Instance.GetVFXAudioSource().volume = MusicSettings.Instance.SFXVolume;
        UpdateButtonIcons();
    }

    private void UpdateButtonIcons()
    {
        musicToggleButton.image.sprite = MusicSettings.Instance.IsMusicMuted ? musicOffIcon : musicOnIcon;
        sfxToggleButton.image.sprite = MusicSettings.Instance.IsSFXMuted ? sfxOffIcon : sfxOnIcon;
    }
}
