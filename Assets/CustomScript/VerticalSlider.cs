using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VerticalSlider : MonoBehaviour
{

    public SelectionMgr mgr;
    Slider slider;
    bool increaseZ = false;
    bool decreaseZ = false;
    public int deltaZ = 1;
    // Use this for initialization
    void Start()
    {
        mgr = FindObjectOfType<SelectionMgr>();
        slider = (Slider)FindObjectsOfType(typeof(Slider))[0];
	}


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ChangeCheckedVERTICAL");
        //Debug.Log(slider.value+" VERTICAL");
        if (increaseZ) {
            //Debug.Log("aumenta Z");
            mgr.selected.transform.Translate(0, 0, +deltaZ);
        }
        if (decreaseZ)
        {
           // Debug.Log("diminuisce Z");
            mgr.selected.transform.Translate(0, 0, -deltaZ);
        }
    }

    public void ValueChangeCheck()
    {
        if (slider.value > 0.9)
        {
            increaseZ = true;
            decreaseZ = false;
        }
        else if (slider.value > 0.1 && slider.value < 0.9)
        {
            increaseZ = false;
            decreaseZ = false;
        }
        else
        {
            decreaseZ = true;
            increaseZ = false;
        }
    }

}
