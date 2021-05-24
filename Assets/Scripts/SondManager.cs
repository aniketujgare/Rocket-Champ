using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{
    public enum Sound
    {
        Explode,
        WinSound,
        LoseSound,
        ShipSetSound,
        Button_Pop,
        Rocket_Thrust,
        Firework_Sound,
        Diamond_Pickup,
        Poerup_Activate_Button,
        Cracker_Explosion,
        Star_animation,
        Eror_Buzzer
    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.Rocket_Thrust] = 0f;
    }
    public static void PlaySound (Sound sound)
    {

        if(CanPlaySound(sound))
        {
            GameObject soundGameobject = new GameObject("sound");
            AudioSource audiosource = soundGameobject.AddComponent<AudioSource>();
            audiosource.PlayOneShot(GetAudioClip(sound));
            audiosource.outputAudioMixerGroup = GameAssets.i.masterAudioMIxer;

        }

    }
    private static bool CanPlaySound(Sound sound)
        {
            switch (sound)
            {
                default:
                    return true;
                case Sound.Rocket_Thrust:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimerDictionary[sound];
                        float rocketThrustTimerMax = 0.05f;
                        if (lastTimePlayed + rocketThrustTimerMax < Time.time)
                        {
                            soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }

            }
        }
        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (GameAssets.SoundAudioCLip soundAudioClip in GameAssets.i.soundAudioClipArray)
            {
                if (soundAudioClip.sound == sound)
                {
                    return soundAudioClip.audioClip;
                }
            }
            Debug.LogError("Sound" + sound + "not found");
            return null;
        }
    

}
