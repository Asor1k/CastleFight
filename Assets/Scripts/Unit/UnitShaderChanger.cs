using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShaderChanger : MonoBehaviour
{
    [SerializeField] List<SkinnedMeshRenderer> renderers;
    private void OnMouseEnter()
    {
        foreach(SkinnedMeshRenderer rend in renderers)
        {
            rend.material.shader = Shader.Find("Outlined/Custom");
        }

    }
    private void OnMouseExit()
    {
        foreach (SkinnedMeshRenderer rend in renderers)
        {
            rend.material.shader = Shader.Find("Standard");
            
        }

    }
}
