using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProduction : MonoBehaviour
{
    private CinemachineVirtualCamera m_virtualCam;
    private CinemachineBasicMultiChannelPerlin m_noise;
    private float m_shakeTime;

    private IEnumerator m_processShake = null;

    // Start is called before the first frame update
    void Start()
    {
        m_virtualCam = GetComponent<CinemachineVirtualCamera>();
        m_noise = m_virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float amplitude, float frequency, float time)
	{
        if (m_processShake != null)
            StopCoroutine(m_processShake);

        m_processShake = ProcessShakeCamrea(amplitude, frequency, time);
        StartCoroutine(m_processShake);
    }

    private IEnumerator ProcessShakeCamrea(float amplitude, float frequency, float time)
	{
        m_noise.m_AmplitudeGain = amplitude;
        m_noise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(time);
        m_noise.m_FrequencyGain = 0.0f;
        m_noise.m_AmplitudeGain = 0.0f;
        m_processShake = null;
    }
}
