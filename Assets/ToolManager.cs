using UnityEngine;
using System.Collections;
using System;

public class ToolManager : MonoBehaviour {

    public AbstractTool currentTool;
    public AbstractTool[] tools;

	void Start () {
        Block.OnBlockClicked += BlockGotClicked;
        BlockSelectionManager.OnBuildToolSelected += SelectBuildTool;
        BlockSelectionManager.OnDestroyToolSelected += SelectDestroyTool;
        BlockSelectionManager.OnTextureChanged += SelectOtherTexture;

    }

    private void SelectOtherTexture(string newTextureToBuildWith)
    {
        //TODO implement
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