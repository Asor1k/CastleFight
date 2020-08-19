using CastleFight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "Vampirism effect", menuName = "Visual effects/Vampirism", order = 0)]
    public class VampirismEffectConfig : EffectConfig
    {
        [SerializeField]
        private VampirismEffect prefab;
  
        public override VisualEffect Create()
        {
            var effect = Pool.I.Get<VampirismEffect>();

            if (effect == null)
            {
                effect = Instantiate(prefab) as VampirismEffect;
                effect.Init(this);
            }

            effect.gameObject.SetActive(true);

            return effect;
        }
    }
}