using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

	bool allowScale;
	public GameObject selected;
	public Material lineMaterial;
	GameObject selectedLine;
	PointerManager ptrMgr;
	RaycastHit hit;

	// Use this for initialization
	void Start () {

		ptrMgr = GameObject.FindObjectOfType<PointerManager> ();

	}
	
	// Update is called once per frame
	void Update () {	

		Vector3 dest;

		destroyLine (selectedLine);

		if (selected != null) {
			
			dest = selected.transform.position;
			dest.y = 0;

			//if at least a collider is found by the line, set the destination y of the first collider met
			if (Physics.Linecast (selected.transform.position, dest, out hit)) {

				dest.y = hit.collider.transform.position.y;

			} 

			selectedLine = drawLine (selected.transform.position, dest, Color.blue, null);

		}
	
	}

	/*
     *  This function must:
     *   - set current object to the new one
     *   - find the group of the new object (if any)
     *   This function is called by pointerManager
     */
	public void selectObject(GameObject toSelect)
	{
		selected = toSelect;
		selected.GetComponent<Selectable> ().selectObj ();

	}

	public void deSelectObject()
	{

		destroyLine (selectedLine);
		ptrMgr.deSelect ();
		selected.GetComponent<Selectable> ().deSelectObj ();
		selected = null;
		//call pointer manager to reset
		//call gesture manager to reset
		//call UI to reset
		//other stuffs   
	}

	public GameObject getSelectedObject(){

		return this.selected;

	}


	/*
     *  For groups managment
     */
	public void selectNext()
	{

	}

	/*
     *  For groups managment
     */
	public void selectAll()
	{

	}

	public GameObject drawLine(Vector3 origin, Vector3 dest, Color color, Material mat){

		GameObject line = new GameObject ();
		line.transform.position = origin;
		line.AddComponent<LineRenderer> ();
		LineRenderer lineRend = line.GetComponent<LineRenderer> ();
		lineRend.material = (mat == null) ? lineMaterial : mat;
		lineRend.SetColors (color, color);
		lineRend.SetPosition (0, origin);
		lineRend.SetPosition (1, dest);
		lineRend.SetWidth (2f, 0f);

		return line;

	}

	public void destroyLine(GameObject line){

		GameObject.Destroy (line);

	}

	/*
     *  This function must allow to translate single obj or groups
     *      If toAdd= true then xyz is added to the current position
     *      Else xyz override current position
     */
	public void translate (Vector3 xyz, bool toAdd)
	{

		if (selected != null) {
			
			if (toAdd) {

				selected.transform.position += xyz;

			} else {
				
				selected.transform.position = xyz;

			}

		}

	}

	public void scale(float value){

		Vector3 newScale;

		newScale.x = value;
		newScale.y = value;
		newScale.z = value;

		if (value == 1) {
			selected.transform.localScale = newScale;
		} else {
			selected.transform.localScale += newScale;
		}

	}

	public void rotate(Vector3 rotation){

		selected.transform.Rotate(rotation);

	}

}
