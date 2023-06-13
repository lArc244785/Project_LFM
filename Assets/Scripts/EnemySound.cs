using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var health = GetComponent<Health>();
        health.OnDead += OnDead;
    }

    public void OnDead()
	{
        SoundManager.Instance.Play(SoundID.Enemy_Dead);
	}

}
