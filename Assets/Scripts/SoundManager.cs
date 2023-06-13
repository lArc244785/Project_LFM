using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	public Sound[] sounds;
	public AudioMixer audioMixer;
	private Dictionary<SoundID, Sound> m_soundTable = new();

	private void Awake()
	{
		Instance = this;

		var soundTable = new GameObject();
		soundTable.transform.parent = gameObject.transform;
		soundTable.name = "SoundTable";

		foreach (var sound in sounds)
		{
			var newSound = new GameObject();
			newSound.AddComponent<AudioSource>();
			newSound.transform.parent = soundTable.transform;
			newSound.name = "sound_" + sound.id.ToString();

			sound.source = newSound.GetComponent<AudioSource>();
			sound.source.clip = sound.clip;
			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.loop = sound.loop;
			sound.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups(sound.type.ToString())[0];

			m_soundTable.Add(sound.id, sound);
		}
	}

	public void Play(SoundID ID)
	{
		m_soundTable[ID].source.Play();
	}

	public void Stop(SoundID ID)
	{
		m_soundTable[ID].source.Stop();
	}

}
