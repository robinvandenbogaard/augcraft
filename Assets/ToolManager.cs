using UnityEngine;
using System.Collections;
using System;

public class ToolManager : MonoBehaviour {

    public AbstractTool currentTool;
    public AbstractTool[] tools;
    public Texture[] textures = new Texture[5];

    public string rootTexturePath;

    void Start () {
        Block.OnBlockClicked += BlockGotClicked;
        BlockSelectionManager.OnBuildToolSelected += SelectBuildTool;
        BlockSelectionManager.OnDestroyToolSelected += SelectDestroyTool;
        BlockSelectionManager.OnTextureChanged += SelectOtherTexture;
        BlockSelectionManager.OnSelectingTool += DisableTool;
    }

    private void DisableTool()
    {
        currentTool = null;
    }

    private void SelectOtherTexture(SupportedTexture newTextureToBuildWith)
    {
        BuildTool buildTool = tools[0] as BuildTool;
        PaintTool paintTool = tools[2] as PaintTool;
        Texture newTexture;
        switch (newTextureToBuildWith)
        {
            case SupportedTexture.DIRT:
                newTexture = textures[0];
                break;
            case SupportedTexture.SAND:
                newTexture = textures[1];
                break;
            case SupportedTexture.STONE:
                newTexture = textures[2];
                break;
            case SupportedTexture.COBBLESTONE:
                newTexture = textures[3];
                break;
            case SupportedTexture.PLANKS_OAK:
            default:
                newTexture = textures[4];
                break;
        }
        buildTool.currentTexture = newTexture;
        paintTool.texture = newTexture;
        Debug.Log("Selecting new texture: " + newTexture);
    }

    private void SelectBuildTool()
    {
        if (currentTool != tools[0])
            currentTool = tools[0];
    }

    private void SelectDestroyTool()
    {
        currentTool = tools[1];
    }

    void BlockGotClicked(Block block, MCFace face)
    {
        if (HasToolSelected())
        {
            currentTool.UseTool(block, face);
        }
    }

    private bool HasToolSelected()
    {
        return currentTool != null;
    }
}