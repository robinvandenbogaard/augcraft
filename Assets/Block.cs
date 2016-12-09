using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public delegate void ClickAction(Block block, MCFace face);
    public static event ClickAction OnBlockClicked;
    
	void Update () {
        DetectHit();
    }

    private void DoHit(RaycastHit hit) {
        MCFace face = GetHitFace(hit);
        if (OnBlockClicked != null)
            OnBlockClicked(this, face);
    }

    private void DetectHit()
    {
        RaycastHit hit;
        Ray ray;

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {

                // Construct a ray from the current touch coordinates
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                
                if (Physics.Raycast(ray, out hit))
                    if (hit.transform.gameObject == this.gameObject)
                    DoHit(hit);
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) { 
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == this.gameObject)
                    DoHit(hit);
            }
        }

#endif
    }



    public MCFace GetHitFace(RaycastHit hit)
    {
        Vector3 incomingVec = hit.normal - Vector3.up;

        if (incomingVec == new Vector3(0, -1, -1))
            return MCFace.South;

        if (incomingVec == new Vector3(0, -1, 1))
            return MCFace.North;

        if (incomingVec == new Vector3(0, 0, 0))
            return MCFace.Up;

        if (incomingVec == new Vector3(1, 1, 1))
            return MCFace.Down;

        if (incomingVec == new Vector3(-1, -1, 0))
            return MCFace.West;

        if (incomingVec == new Vector3(1, -1, 0))
            return MCFace.East;

        return MCFace.None;
    }
}
