using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSourcePrefab;
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioSource> activeSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LoadAudioClips();
    }

    private void LoadAudioClips()
    {
        // Load audio clips from Resources folder
        audioClips["coin_collect"] = Resources.Load<AudioClip>("Audio/coin_drop");
        audioClips["bonus"] = Resources.Load<AudioClip>("Audio/bonus_sound");
        audioClips["ui_click"] = Resources.Load<AudioClip>("Audio/UI_click");
    }

    public void PlaySound(string soundName, float volume = 1f)
    {
        if (!audioClips.ContainsKey(soundName))
        {
            Debug.LogWarning($"Sound '{soundName}' not found!");
            return;
        }

        AudioSource source = Instantiate(audioSourcePrefab, transform);
        source.clip = audioClips[soundName];
        source.volume = volume;
        source.Play();

        Destroy(source.gameObject, audioClips[soundName].length);
    }
}
