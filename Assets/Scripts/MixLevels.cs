using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void setSFXLvl (float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLvl (float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
    }
}
