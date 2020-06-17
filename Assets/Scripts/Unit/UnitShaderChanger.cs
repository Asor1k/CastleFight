using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class UnitShaderChanger : MonoBehaviour
    {
        [SerializeField] List<SkinnedMeshRenderer> renderers;
        public void Start()
        {
            foreach (SkinnedMeshRenderer skinnedMesh in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                renderers.Add(skinnedMesh);
            }
        }
        private void OnMouseEnter()
        {
            foreach (SkinnedMeshRenderer rend in renderers)
            {
                rend.material.shader = Shader.Find("Outlined/Custom");
            }

        }
        public void OnDestroy()
        {
            renderers.Clear();
        }
        private void OnMouseExit()
        {
            foreach (SkinnedMeshRenderer rend in renderers)
            {
                rend.material.shader = Shader.Find("Standard");

            }

        }
    }
}