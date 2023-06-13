using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType
{ 
	BGM,
	SFX,
}

public enum SoundID 
{
	PlayerRun,
	PlayerHit,

	BaseMG_Fire,
	BaseMG_Reload,
	BaseMG_Fire_Tail,
	BaseMG_Cock,
	BaseMG_Dry_Fire,

	ShotGun_Fire,
	ShotGun_Reload,
	ShotGun_Cock,
	ShotGun_Dry_Fire,
	ShotGun_PickUp,

	Item_PickUp_Heal,

	Enemy_Dead,

	BGM_0,
}


[Serializable]
public class Sound
{
	public AudioClip clip;
	public SoundID id;
	[Range(0f, 1f)]
	public float volume;
	[Range(.1f, 3f)]
	public float pitch;
	public bool loop;
	public SoundType type;

	[HideInInspector]
	public AudioSource source;
}
