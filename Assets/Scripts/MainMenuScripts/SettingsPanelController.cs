using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject settingsButton;

    [SerializeField]
    private MusicController musicControllerAux;

    [SerializeField]
    private Animator settingsAnim;

    [SerializeField]
    private Slider slider;

    private void Start()
    {
        settingsPanel.GetComponent<Animator>().Play("SlideOut");
    }

    public void OpenSettingsPanel()
    {
        slider.value = musicControllerAux.GetMusicVolume();
        settingsPanel.SetActive(true);
        settingsButton.gameObject.SetActive(false);
        settingsAnim.Play("SlideIn");
    }

    public void CloseSettingsPanel()
    {
        StartCoroutine(CloseSettings());
    }

    IEnumerator CloseSettings()
    {
        settingsAnim.Play("SlideOut");
        yield return new WaitForSeconds(1f);
        settingsPanel.SetActive(false);
        settingsButton.gameObject.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        musicControllerAux.SetMusicVolume(volume);
    }

    public void SetSounds(float volume)
    {
        AudioEffectsManager.Instance.audioSource.volume = volume;
    }

}
