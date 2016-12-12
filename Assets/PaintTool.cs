using UnityEngine;
using System.Collections;
using System;

public class PaintTool : AbstractTool
{
    public Texture texture;

    protected override void Use(Block blockToUseToolOn, MCFace face)
    {
        blockToUseToolOn.UpdateTextureTo(texture);
        Debug.Log("adjusted texture from block");
    }
}
