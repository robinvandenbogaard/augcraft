using UnityEngine;
using System.Collections;
using System;

public class ToolManager : MonoBehaviour {

    public AbstractTool currentTool;
    public AbstractTool[] tools;

    public string rootTexturePath;

    void Start () {
        Block.OnBlockClicked += BlockGotClicked;
        BlockSelectionManager.OnBuildToolSelected += SelectBuildTool;
        BlockSelectionManager.OnDestroyToolSelected += SelectDestroyTool;
        BlockSelectionManager.OnTextureChanged += SelectOtherTexture;

    }

    private void SelectOtherTexture(SupportedTexture newTextureToBuildWith)
    {
        BuildTool buildTool = tools[0] as BuildTool;
        PaintTool paintTool = tools[2] as PaintTool;
        string textureName;
        switch (newTextureToBuildWith)
        {
            case SupportedTexture.DIRT:
                textureName = "blocks/dirt";
                break;
            case SupportedTexture.SAND:
                textureName = "blocks/sand";
                break;
            case SupportedTexture.STONE:
                textureName = "blocks/stone";
                break;
            case SupportedTexture.COBBLESTONE:
                textureName = "blocks/cobblestone";
                break;
            case SupportedTexture.PLANKS_OAK:
            default:
                textureName = "blocks/planks_oak";
                break;
        }
        //FIXME: instantiate texture
        buildTool.currentTexture = Resources.Load(rootTexturePath + textureName) as Texture;
        paintTool.texture = Resources.Load(rootTexturePath + textureName) as Texture;
        Debug.Log("Selecting new texture: " + textureName);
    }

    private void SelectDestroyTool()
    {
        currentTool = tools[1];
    }

    private void SelectBuildTool()
    {
        if (currentTool != tools[0])
            currentTool = tools[0];
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