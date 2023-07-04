using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect), typeof(PooledObject))]
public class EffectAutoRelease : MonoBehaviour
{
    private VisualEffect m_effect;
    private PooledObject m_pooledObject;

    private void Init()
	{
        m_effect = GetComponent<VisualEffect>();
        m_pooledObject = GetComponent<PooledObject>();
    }

	public void Play()
	{
        if (m_effect == null)
            Init();

        m_effect.SendEvent("OnPlay");
        StartCoroutine(OnAutoRelease());
	}

    IEnumerator OnAutoRelease()
	{
        while (m_effect.aliveParticleCount == 0)
            yield return null;
        while (m_effect.aliveParticleCount > 0)
            yield return null;

        m_pooledObject.Release();
	}

}
