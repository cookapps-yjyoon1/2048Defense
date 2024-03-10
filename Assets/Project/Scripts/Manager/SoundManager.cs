using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AudioClipSlot
{
    public string name;
    public List<AudioClip> clip;
}

public enum Enum_Sound
{
	Bgm,
	Effect,
	Speech,
	Max,
}

public class SoundManager : SingletonBehaviour<SoundManager>
{
    private AudioSource[] _audioSources = new AudioSource[(int)Enum_Sound.Max];
    [SerializeField] private AudioClipSlot[] slots;
    private Dictionary<string, List<AudioClip>> _audioClips = new Dictionary<string, List<AudioClip>>();
    private List<AudioSource> _effectAudioSources = new List<AudioSource>();

    protected override void Awake()
    { 
        base.Awake();
        string[] soundTypeNames = System.Enum.GetNames(typeof(Enum_Sound));
        for (int count = 0; count < soundTypeNames.Length - 1; count++)
        {
            GameObject go = new GameObject { name = soundTypeNames[count] };
            if (count == 1)
            {
                _effectAudioSources.Add(CreateNewAudioSource(Enum_Sound.Effect));
            }
            else
            {
                _audioSources[count] = CreateNewAudioSource((Enum_Sound)count);
            }

            go.transform.parent = this.transform;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            _audioClips.Add(slots[i].name, slots[i].clip);
        }

        _audioSources[(int)Enum_Sound.Bgm].loop = true;
    }

    public AudioSource CreateNewAudioSource(Enum_Sound name)
    {
        GameObject go = new GameObject { name = name.ToString() };
        go.transform.parent = this.transform;
        return go.AddComponent<AudioSource>();
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();
        _audioClips.Clear();
    }

    public void SetPitch(Enum_Sound type, float pitch = 1.0f)
	{
		AudioSource audioSource = _audioSources[(int)type];
        if (audioSource == null)
            return;

        audioSource.pitch = pitch;
	}

    public bool Play(Enum_Sound type, string name, int index = 0,float volume = 1.0f, float pitch = 1.0f)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        AudioSource audioSource = _audioSources[(int)type];
        
        if (type == Enum_Sound.Bgm)
        {
            AudioClip audioClip = GetAudioClip(name,index);
            if (audioClip == null)
                return false;

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.Play();
            return true;
        }
        if (type == Enum_Sound.Effect)
        {
            AudioSource availableSource = _effectAudioSources.Find(source => !source.isPlaying);
            if (availableSource == null)
            {
                availableSource = CreateNewAudioSource(Enum_Sound.Effect);
                _effectAudioSources.Add(availableSource);
            }
        
            // 효과음 재생
            AudioClip audioClip = GetAudioClip(name, index);
            if (audioClip == null)
                return false;
        
            availableSource.volume = volume;
            availableSource.pitch = pitch;
            availableSource.PlayOneShot(audioClip);
            return true;
        }
        else if (type == Enum_Sound.Speech)
		{
			AudioClip audioClip = GetAudioClip(name,index);
			if (audioClip == null)
				return false;

			if (audioSource.isPlaying)
				audioSource.Stop();
            
            audioSource.volume = volume;
            audioSource.pitch = pitch;
			audioSource.clip = audioClip;
            audioSource.Play();
			return true;
		}

        return false;
    }

    public void Stop(Enum_Sound type)
	{
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Stop();
    }

	public float GetAudioClipLength(string name)
    {
        AudioClip audioClip = GetAudioClip(name);
        if (audioClip == null)
            return 0.0f;
        return audioClip.length;
    }

    private AudioClip GetAudioClip(string audioName,int index = 0)
    {
        if (!_audioClips.TryGetValue(audioName, out var audioClips))
        {
            Debug.Log("[Error] No Audio Clip: " + audioName);
            return null;
        }

        return audioClips[index];
    }

    public void PlayRandomIndex(string name,float volume = 0.7f, float pitch = 1f)
    {
        var clip = _audioClips.TryGetValue(name , out var sound) ? Random.Range(0, sound.Count) : -1;

        if (clip < 0)
        {
            return;
        }

        Play(Enum_Sound.Effect, name, clip, volume, pitch);
    }
}
