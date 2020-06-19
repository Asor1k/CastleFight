using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ContextMenuAdditions 
{
    private static Shader shaderToChange;
    [MenuItem("CONTEXT/Shader/Set shader to change into")]
    static void SetShaderTOChange(MenuCommand command)
    {
        shaderToChange = (Shader)command.context;
        Debug.Log("Set shader " + shaderToChange.name + " to change into");
    }
    [MenuItem("CONTEXT/Transform/ChangeShaderInChildren(NOT WORKING!!)")]
    static void ChangeShaderInChildren(MenuCommand command)
    {
        Transform tr = (Transform)command.context;
        foreach(SkinnedMeshRenderer rend in tr.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            rend.material.shader = Shader.Find("Standart");
        }
    }

}
