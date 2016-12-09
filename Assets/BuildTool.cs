using UnityEngine;
using System.Collections;
using System;

public class BuildTool : AbstractTool {

    public GameObject blockPrefab;

    protected override void Use(Block blockToUseToolOn, MCFace face)
    {

        bool success = true;
        Vector3 newPosition = blockToUseToolOn.transform.position;
        float scale = 0.3f;

        Debug.Log("Face " + face + " is available");
        switch (face)
        {
            case MCFace.Up:
                newPosition.y += scale;
                break;
            case MCFace.East:
                newPosition.x += scale;
                break;
            case MCFace.West:
                newPosition.x -= scale;
                break;
            case MCFace.North:
                newPosition.z += scale;
                break;
            case MCFace.South:
                newPosition.z -= scale;
                break;
            case MCFace.Down:
            case MCFace.None:
            default:
                Debug.Log("Won't place a block on face " + face);
                success = false;
                break;
        }

        if (success)
        {
            Debug.Log("Placing Block on " + face);
            Instantiate(blockPrefab, newPosition, Quaternion.identity);
        }
    }
}
