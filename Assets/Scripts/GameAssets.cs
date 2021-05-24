using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public Color color = new Color(0.1495628f, 0f, 0.245283f);

    public AudioMixerGroup masterAudioMIxer;
    public SoundAudioCLip[] soundAudioClipArray;

    public void MusicOn()
    {
        masterAudioMIxer.audioMixer.SetFloat("musicVol", -10f); // initially -15
    }

    public void MusicOff()
    {
        masterAudioMIxer.audioMixer.SetFloat("musicVol", -80f);
    }
    public void sfxOn()
    {
        masterAudioMIxer.audioMixer.SetFloat("sfxVol", -10f); // initially -15
    }
    public void sfxOff()
    {
        masterAudioMIxer.audioMixer.SetFloat("sfxVol", -80f);
    }

    [System.Serializable]
    public class SoundAudioCLip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip; 
    }

}
