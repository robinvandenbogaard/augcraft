using UnityEngine;
using System.Collections;
using System;

public class ToolManager : MonoBehaviour {

    public AbstractTool currentTool;
    public AbstractTool[] tools;

	void Start () {
        Block.OnBlockClicked += BlockGotClicked;
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