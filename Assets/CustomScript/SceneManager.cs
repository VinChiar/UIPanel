using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Axe { X,Y,Z};

public class SceneManager : MonoBehaviour {

    public GameObject rootScene;
    public GameObject selected;
	public Material lineMaterial;
	GameObject selectedLine;

	PointerManager ptrMgr;
    UIPanel panel;
    FineTuningController fiTuCo;


    RaycastHit hit;
    private bool fineTuningActive;

    // Use this for initialization
    void Start () {

		ptrMgr = FindObjectOfType<PointerManager> ();
        panel = FindObjectOfType<UIPanel>();
        fiTuCo = FindObjectOfType<FineTuningController>();
        fineTuningActive = false;

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
     *  This function is called by pointerManager
     */
	public void selectObject(GameObject toSelect)
	{
		selected = toSelect;
		selected.GetComponent<Selectable>().selectObj ();
        if (fineTuningActive)
        {
            fiTuCo.setActive();
        }
        else
        {
            panel.setActive();
        }

    }
	public void deSelectObject()
	{

        destroyLine(selectedLine);

        ptrMgr.deSelect ();
        panel.setInactive();
        fiTuCo.setInactive();
        selected.GetComponent<Selectable>().deSelectObj();
         
        selected = null;
    }

	public GameObject getSelectedObject(){

		return this.selected;

	}

    /* CHANGE OPERATIVE WAY FUNCT
     * True -> active fine tuning
     * False -> active normal way
     */ 
    public void changeOperativeWay(bool activeFineTuning)
    {
        this.fineTuningActive = activeFineTuning;
        if (activeFineTuning)
        {
            panel.setInactive();
            fiTuCo.setActive();
        }
        else
        {
            fiTuCo.setInactive();
            panel.setActive();
        }
    }


    /*support region */

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

    public void scaleScene( float value )
    {
        rootScene.transform.localScale *= value;
    }

	public void rotate(Vector3 rotation){
		selected.transform.Rotate(rotation,Space.World);
	}

    /***FUNCTION FOR 7GoF */

    public void translateSingleAxe(float value, Axe a)
    {
        Vector3 v3;
        switch (a)
        {   
            case Axe.X:
                v3 = new Vector3(value, 0, 0);
                break;
            case Axe.Y:
                v3 = new Vector3(0, value, 0);
                break;
            case Axe.Z:
                v3 = new Vector3(0, 0, value);
                break;
            default:
                v3 = Vector3.zero;
                break;
        }
        if(selected!= null)
        {
            selected.transform.position += v3;
        }
    }

    public void rotateSingleAxe(float value, Axe a)
    {
        Vector3 v3;
        switch (a)
        {
            case Axe.X:
                v3 = new Vector3(value, 0, 0);
                break;
            case Axe.Y:
                v3 = new Vector3(0, value, 0);
                break;
            case Axe.Z:
                v3 = new Vector3(0, 0, value);
                break;
            default:
                v3 = Vector3.zero;
                break;
        }
        if (selected != null)
        {
            selected.transform.Rotate(v3, Space.World);
        }
    }


}
