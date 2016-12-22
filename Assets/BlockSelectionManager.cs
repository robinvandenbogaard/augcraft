using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSelectionManager : MonoBehaviour
{
    public GameObject selectionPanel;
    public GameObject selectionBlock;

    //three events that other scripts can statically hook on to.
    public delegate void TextureChangeAction(SupportedTexture newTextureToBuildWith);
    public static event TextureChangeAction OnTextureChanged;
    
    public delegate void SelectDestroyToolAction();
    public static event SelectDestroyToolAction OnDestroyToolSelected;

    public delegate void SelectBuildToolAction();
    public static event SelectBuildToolAction OnBuildToolSelected;

    public delegate void SelectingToolAction();
    public static event SelectingToolAction OnSelectingTool;

    public void ShowBlockSelection()
    {
        selectionPanel.SetActive(true);
        selectionBlock.SetActive(false);
        Debug.Log("ShowBlockSelection");
        if (OnSelectingTool != null)
        {
            OnSelectingTool();
        }
    }

    public void BlockSelect(Sprite selectedImage)
    {
        selectionBlock.GetComponent<Image>().sprite = selectedImage;

        selectionPanel.SetActive(false);
        selectionBlock.SetActive(true);

        bool isRemovalTexture = selectedImage.name.Equals("cube remove"); //TODO: determine this value based on Sprite
        SupportedTexture newTextureToBuildWith = GetSupportedTexture(selectedImage.name); //TODO: map Sprites to Enum values for supported textures

        if (isRemovalTexture)
        {
            if (OnDestroyToolSelected != null)
            { //check for null, if no one registered to this event its null.
                OnDestroyToolSelected();
            }
        } else
        {
            if (OnBuildToolSelected != null)
            {  //check for null, if no one registered to this event its null.
                OnBuildToolSelected();
            }

            if (OnTextureChanged != null)
            { //check for null, if no one registered to this event its null.
                OnTextureChanged(newTextureToBuildWith);
            }
        }
        Debug.Log("ShowBlockSelection");
    }

    private SupportedTexture GetSupportedTexture(string name)
    {
        SupportedTexture st;
        switch (name)
        {
            case "cube cobblestone":
                st = SupportedTexture.COBBLESTONE;
                break;
            case "cube sand":
                st = SupportedTexture.SAND;
                break;
            case "cube dirt":
                st = SupportedTexture.DIRT;
                break;
            case "cube stone":
                st = SupportedTexture.STONE;
                break;
            case "cube planks oak":
            default:
                st = SupportedTexture.PLANKS_OAK;
                break;
        }
        return st;
    }
}
