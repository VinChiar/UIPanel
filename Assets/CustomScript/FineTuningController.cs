using UnityEngine;
using System.Collections;

public class FineTuningController : MonoBehaviour {

    private bool active;
    private SceneManager scnMgr;


    public void setActive()
    {
        active = true;
    }
    public void setInactive()
    {
        active = false;
    }

    
    void Start () {
        scnMgr = FindObjectOfType<SceneManager>();
	}
	
	void Update () {
        if (active)
        {
            Debug.Log("FITUCO: Dovrei fare qualcosa ma sto aspettando di essere implementato.");
        }
	}
}
