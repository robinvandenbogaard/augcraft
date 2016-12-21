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

    public void ShowBlockSelection()
    {
        selectionPanel.SetActive(true);
        selectionBlock.SetActive(false);
        Debug.Log("ShowBlockSelection");
    }

    public void BlockSelect(Sprite selectedImage)
    {
        selectionBlock.GetComponent<Image>().sprite = selectedImage;

        selectionPanel.SetActive(false);
        selectionBlock.SetActive(true);

        bool isRemovalTexture = false; //TODO: determine this value based on Sprite
        SupportedTexture newTextureToBuildWith = SupportedTexture.DIRT; //TODO: map Sprites to Enum values for supported textures

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
}
