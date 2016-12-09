using UnityEngine;
using System.Collections;

public abstract class AbstractTool : MonoBehaviour {
    
    public float coolDown = 0.25f;
    float coolDownRemaining = 0f;
    
    public void UseTool(Block blockToUseToolOn, MCFace face)
    {
        if (coolDownRemaining <= 0)
        {
            Use(blockToUseToolOn, face);
            coolDownRemaining = coolDown;
        }
    }

    protected abstract void Use(Block blockToUseToolOn, MCFace face);

    void Update () {
        coolDownRemaining -= Time.deltaTime;
    }
}
