using UnityEngine;
using System.Collections;

public enum MovementState { MOVEXZ, MOVEY, NO_MOVE};

public class PointerManager : MonoBehaviour {

	public SceneManager sceneMgr;
    public GameObject cursor;
    public float TIME_FOR_SELECTION = 1f;

    private MovementState ms = MovementState.NO_MOVE;
    private bool selectable;
    private Vector3 fixedPointer;
    private Camera cam;

    //for all
    private Ray ray;
    float rayDist;
    //for No_move
    private GameObject focused;
    private GameObject selected;
    private RaycastHit hit;
    private float time;
    //for Move_XZ
    private Plane planeXZ;
    //for Move_Y
    private Plane planeY;
    private Vector3 direction;

    

    // Use this for initialization
    void Start () {

		sceneMgr = GameObject.FindObjectOfType<SceneManager> ();
        selectable = true;
        cam = FindObjectOfType<Camera>();
        fixedPointer = new Vector3(0.5f*cam.pixelWidth, 0.5f*cam.pixelHeight, cam.nearClipPlane);

    }
	


	// Update is called once per frame
	void Update () {
        if(cursor == null)
        {
            cursor = GameObject.FindGameObjectWithTag("Player");
        }
        switch (ms)
        {
            case MovementState.NO_MOVE:
                //here pointer on collision with all object
                ray = cam.ScreenPointToRay(fixedPointer);
                Physics.Raycast(ray, out hit);

                if (hit.collider != null)
                {
                    cursor.transform.position = hit.point;
                }
                if (selectable && hit.collider != null){
                    //if on a obj for 1 sec, take that as selected
                    if (hit.collider.gameObject.GetComponent<Selectable>() != null)
                    {
                        if(hit.collider.gameObject.Equals(focused))
                        {
                            time += Time.deltaTime;
                            if(time >= TIME_FOR_SELECTION)
                            {
                                //this object must be selected
								sceneMgr.selectObject(focused);
                                selectable = false;
                                time = 0;
                                selected = focused;
                            }
                        }
                        else
                        {
                            focused = hit.collider.gameObject;
                        }
                    }
                    else
                    {
                        time = 0;
                    }
                }
                break;

            case MovementState.MOVEXZ:
                //here pointer on collison with plane
                if (selected != null)
                {
                    ray = cam.ScreenPointToRay(fixedPointer);
                    if(planeXZ.Raycast(ray, out rayDist))
                    {
                        cursor.transform.position = ray.GetPoint(rayDist);
						sceneMgr.translate (cursor.transform.position, false);
                    }
                }
                else
                    ms = MovementState.NO_MOVE;
                break;

            case MovementState.MOVEY:
                //here pointer on collision with perpendicular plane
                if( selected != null)
                {
                    direction = -cam.transform.position + selected.transform.position;
                    direction.y = 0;
                    planeY.SetNormalAndPosition(direction.normalized, selected.transform.position);
                    ray = cam.ScreenPointToRay(fixedPointer);
                    if (planeY.Raycast(ray, out rayDist))
                    {
                        Debug.Log(ray.GetPoint(rayDist));
                        cursor.transform.position = ray.GetPoint(rayDist);
						sceneMgr.translate (cursor.transform.position, false);
                    }
                }
                else
                    ms = MovementState.NO_MOVE;
                break;
        }
    }


    public void changeMovState(int ms)
    {
		if (ms == 0)
        {
            this.ms = MovementState.NO_MOVE;
        }
        else if(selected == null && ms == 1)
        {
            this.ms = MovementState.NO_MOVE;
        }
        else if(ms == 1 && selected != null)
        {
            this.ms = MovementState.MOVEXZ;
            planeXZ = new Plane(Vector3.up, selected.transform.position);
        }
        else if(ms == 2)
        {
            this.ms = MovementState.MOVEY;
        }
    }

    public void deSelect()
    {
		sceneMgr.deSelectObject();
        selectable = true;
        focused = null;
        selected = null;
    }

	public bool getMovementState(){

		if (ms == MovementState.NO_MOVE)
			return true;

		return false;

	}

}
