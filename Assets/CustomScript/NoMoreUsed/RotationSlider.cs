using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotationSlider : MonoBehaviour
{
    public SelectionMgr mgr;
    Slider slider;
    bool rotationLeft = false;
    bool rotationRight = false;
    public int deltaAlpha = 1;
    // Use this for initialization
    void Start()
    {
        mgr = FindObjectOfType<SelectionMgr>();
		slider = (Slider) FindObjectsOfType(typeof(Slider))[1];
        //slider = (Slider)FindObjectOfType(typeof(Slider));
        //Debug.Log("AAAAAAAAAAAAAAAAAAAAA");
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationRight)
            mgr.selected.transform.Rotate(0, 0, -deltaAlpha);
        if (rotationLeft)
            mgr.selected.transform.Rotate(0, 0, deltaAlpha);
    }

    public void ValueChangeCheck()
    {
        //Debug.Log("ChangeChecked");
       // Debug.Log(slider.value);
		if (slider.value > 0.9)
        {           
            rotationRight = true;
            rotationLeft = false;
            //Debug.Log(">0.9");
        }
        else if (slider.value > 0.1 && slider.value < 0.9)
        {
            rotationLeft = false;
            rotationRight = false;
            //Debug.Log("0.1<value<0.9");
        }
        else
        {
            rotationLeft = true;
            rotationRight = false;
           // Debug.Log("<0.1");
        }
    }

}
