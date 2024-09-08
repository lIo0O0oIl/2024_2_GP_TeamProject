using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] bgmSounds;           // BGM ���� ����
    public Sound[] effectSounds;        // SFX ���� ����

    public AudioSource audioSourceBgmPlayers;           // BGM�� ����� ����� �ҽ�
    public AudioSource audioSourceEffectsPlayers;     // SFX�� ����� ����� �ҽ�

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBGM(string name) // BGM ����
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (name == bgmSounds[i].soundName)
            {
                audioSourceBgmPlayers.clip = bgmSounds[i].clip;
                audioSourceBgmPlayers.loop = true;
                audioSourceBgmPlayers.Play();

                return;
            }
        }
    }

    public void PlaySFX(string name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (name == effectSounds[i].soundName)
            {
                audioSourceEffectsPlayers.PlayOneShot(effectSounds[i].clip);
                return;
            }
        }
    }

    public void StopBGM()
    {
        audioSourceBgmPlayers.Stop();
    }

    public void StopSFX()
    {
        audioSourceEffectsPlayers.Stop();
    }
}