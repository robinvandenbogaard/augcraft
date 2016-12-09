using UnityEngine;
using System.Collections;
using System;

public class DestroyTool : AbstractTool {


    protected override void Use(Block blockToUseToolOn, MCFace face)
    {
        Destroy(blockToUseToolOn.gameObject);
        Debug.Log("detroyed a block");
    }
}
