using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider, bgmSlider, sfxSlider;

    private void Start()
    {
        masterSlider.onValueChanged.AddListener(delegate { Master(); });
        bgmSlider.onValueChanged.AddListener(delegate { BGM(); });
        sfxSlider.onValueChanged.AddListener(delegate { SFX(); });
    }

    public void Master()
    {
        mixer.SetFloat("Master", masterSlider.value);
    }

    public void BGM()
    {
        mixer.SetFloat("BGM", bgmSlider.value); 
    }

    public void SFX()
    {
        mixer.SetFloat("SFX", sfxSlider.value);
    }
}
