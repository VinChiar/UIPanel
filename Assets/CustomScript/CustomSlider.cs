using UnityEngine;
using System.Collections;

public class CustomSlider : MonoBehaviour {
    SelectionMgr mgr;
    public int speed = 2;
    // Use this for initialization
    void Start () {
        mgr = FindObjectOfType<SelectionMgr>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ValueChangeCheck()
    {
        Debug.Log("pippo!");
    }
}
