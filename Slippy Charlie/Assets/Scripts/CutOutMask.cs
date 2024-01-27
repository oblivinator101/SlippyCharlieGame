using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CutOutMask : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material material = new Material(base.materialForRendering);
            material.SetFloat("_StencilComp", (float)CompareFunction.NotEqual);
            return material;
        }
    }
    // protected override void Awake()
    // {
    //     base.Awake();
    //     Material material = new Material(base.materialForRendering);
    //     material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
    //     this.material = material;
    // }
}
