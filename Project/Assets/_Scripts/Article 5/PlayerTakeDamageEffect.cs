using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerTakeDamageEffect : MonoBehaviour
{
    [SerializeField]
    Volume m_volume;
    Vignette m_vignette;

    [SerializeField]
    private float m_damageDuration = 0.5f;
    private float m_currentTime = 0;
    [SerializeField]
    private AnimationCurve m_curve;

    private void Start()
    {
        m_volume.profile.TryGet<Vignette>(out m_vignette);
    }

    public void TriggerHitEffect()
    {
        m_currentTime = 0;
        m_vignette.intensity.Override(0f);
        StartCoroutine(HitCoroutine());
    }

    private IEnumerator HitCoroutine()
    {
        while (m_currentTime <= m_damageDuration)
        {
            m_currentTime += Time.deltaTime;
            float currentValue = Mathf.Clamp01(m_currentTime / m_damageDuration);
            m_vignette.intensity.Override(m_curve.Evaluate(currentValue));
            yield return null;
        }
        m_vignette.intensity.Override(0f);
    }
}
