using UnityEngine;

public static class Extentions
{
    public static AudioClip GetAudioClip(this Sound[] sound, AudioClipManager action)
    {
        foreach (var currentSound in sound)
        {
            if (currentSound.Action == action)
            {
                return currentSound.Audio;
            }
        }

        return default;
    }
}