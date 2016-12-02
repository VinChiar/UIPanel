using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

	public Material newM, oldM;
	// Use this for initialization
	void Start () {
		oldM = GetComponent<Renderer>().material;

	}

	public void selectObj()
	{
		GetComponent<Renderer>().material = newM;
	}

	public void deSelectObj()
	{
		GetComponent<Renderer>().material = oldM;
	}


}