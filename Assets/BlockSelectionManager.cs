using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSelectionManager : MonoBehaviour
{
    public GameObject selectionPanel;
    public GameObject selectionBlock;


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
        Debug.Log("ShowBlockSelection");
    }
}
