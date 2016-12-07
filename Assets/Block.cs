using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public GameObject cubePrefab;

    public float buildCoolDown = 1f;
    float coolDownRemaining = 0f;

    ArrayList occupiedFaces = new ArrayList();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (coolDownRemaining < 0 && DetectHit())
        {
            coolDownRemaining = buildCoolDown;
        }
        else
        {
            coolDownRemaining -= Time.deltaTime;
        }

    }

    private bool DoHit(RaycastHit hit) {
        MCFace face = GetHitFace(hit);
        bool success = true;
        Vector3 newPosition = transform.position;
        float scale = (float)0.3;

        if (occupiedFaces.Contains(face))
            return false;

        Debug.Log("Face " + face+ " is available");
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
                Debug.Log("Won't place a block on face "+face);
                success = false;
                break;
        }

        if (success)
        {
            occupiedFaces.Add(face);
            Debug.Log("Placing Block on "+ face);
            Instantiate(cubePrefab, newPosition, Quaternion.identity);
        }
        return success;
    }

    private bool DetectHit()
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
                    return DoHit(hit);
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) { 
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == this.gameObject)
                    return DoHit(hit);
            }
        }

#endif
        return false;
    }

    public enum MCFace
    {
        None,
        Up,
        Down,
        East,
        West,
        North,
        South
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
