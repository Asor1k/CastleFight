using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public abstract class VisualEffect : MonoBehaviour
    {
        [SerializeField]
        protected ParticleSystem particleSystem;
        protected EffectConfig config;

        public void Init(EffectConfig config)
        {
            this.config = config;
        }

        public void StartEffect()
        {
            particleSystem.gameObject.SetActive(true);
            particleSystem.time = 0;
            particleSystem.Play();
        }

        public void StopEffect()
        {
            particleSystem.gameObject.SetActive(false);
            particleSystem.Stop();
        }

        protected abstract void OnEnd();

        private IEnumerator StartDurationTimer()
        {
            yield return new WaitForSeconds(config.Duration);

            StopEffect();
            OnEnd();
        }
    }
}