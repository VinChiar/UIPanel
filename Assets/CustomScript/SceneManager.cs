using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	bool allowScale;
	public GameObject selected;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {			
	
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
	}

	public void deSelectObject()
	{
		
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

	/*
     *  This function must allow to translate single obj or groups
     *      If toAdd= true then xyz is added to the current position
     *      Else xyz override current position
     */
	public void translate (Vector3 xyz, bool toAdd)
	{
		if (selected != null)
		{
			if (toAdd)
			{
				selected.transform.position += xyz;
			}
			else
			{
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
