using UnityEngine;
using System.Collections;

public class SelectionMgr : MonoBehaviour {

	public GameObject selected;
	public bool allowSelect = true;
	public Vector3 newScale;

	public Material m1, m2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if ((Input.touchCount == 1 || Input.GetMouseButtonDown(0)) && allowSelect) {

			Touch touch = Input.touches [0];
			Ray ray = Camera.main.ScreenPointToRay (touch.position);
			//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {

				if (hit.transform.gameObject.tag != "UI") {
					if (selected != null)
						selected.GetComponent<Renderer> ().material = m2;
					
					selected = null;
                    selected = hit.transform.gameObject;
					m2 = selected.GetComponent<Renderer> ().material;
					selected.GetComponent<Renderer>().material = m1;
				}

			}

		} else if (Input.touchCount == 2) {
			allowSelect = false;
		} else if (Input.touchCount == 0) {
			allowSelect = true;
		}	
	
	}

	public void scale(float value){

		newScale.x = value;
		newScale.y = value;
		newScale.z = value;

		if (value == 1) {
			selected.transform.localScale = newScale;
		} else {
			selected.transform.localScale += newScale;
		}

	}

    public void XYTranslate (Vector3 xyt)
    {
        if(selected != null)
            selected.transform.position += xyt;
    }

}
