using UnityEngine;
using System.Collections;

public enum RotateState { NO_ROT, ROT_X, ROT_Y, ROT_Z };

public class GestureMgr : MonoBehaviour {

	SceneManager sceneMgr;
	GameObject selected;
	private RotateState rs;
    private bool allowScale;
	private bool allowRotate;

	// Use this for initialization
	void Start () {
	
		sceneMgr = GameObject.FindObjectOfType<SceneManager> ();
		allowScale = false;
		rs = RotateState.NO_ROT;
		allowRotate = false;

	}
	
	// Update is called once per frame
	void Update () {

		selected = sceneMgr.getSelectedObject ();

		if (allowScale)
			pinchToScale();

		if (allowRotate)
			pinchToRotate();
	
	}

    public void allowScaling ()
    {
        allowScale = !allowScale;
    }

	public void disableScaling(){
		
		allowScale = false;

	}

	public void allowRotating (RotateState rs)
	{
		if(rs == this.rs || this.rs == RotateState.NO_ROT)
			allowRotate = !allowRotate;
	
		if (allowRotate)
			this.rs = rs;
		
    }

	public void disableRotating(){

		allowRotate = false;

	}

	public void pinchToScale(){

		int speed = 10;

		if(Input.touchCount == 2){

			Touch touch1 = Input.GetTouch (0);
			Touch touch2 = Input.GetTouch (1);

			Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
			Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

			float prevDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
			float touchDeltaMag = (touch1.position - touch2.position).magnitude;

			float deltaMagnitudeDiff = (touchDeltaMag - prevDeltaMag)*Time.deltaTime*speed;

			if (selected.transform.localScale.x >= 1) {

				sceneMgr.scale(deltaMagnitudeDiff);

			} else {

				sceneMgr.scale(1f);

			}		

		}

	}

	public void pinchToRotate(){

		int speed = 2;
		if (Input.touchCount == 2){

			Vector3 factors = Vector3.zero;

			switch (rs) {

			case RotateState.ROT_X:
				factors.x = 1;
				break;
			case RotateState.ROT_Y:
				factors.y = 1;
				break;
			case RotateState.ROT_Z:
			default:
				factors.z = 1;
				break;

			}			

			Touch touch1 = Input.GetTouch(0);
			Touch touch2 = Input.GetTouch(1);
			float y1 = touch1.position.y;
			float y2 = touch2.position.y;
			float delta1 = touch1.deltaPosition.x;
			float delta2 = touch2.deltaPosition.x;
			float alpha = (Mathf.Abs (delta1) + Mathf.Abs (delta2));//*speed;

			Vector3 rotateVector = new Vector3 (factors.x * alpha, factors.y * alpha, factors.z * alpha);

			if (y1 > y2){

				if (delta1 > 0 && delta2 < 0)
					sceneMgr.rotate(rotateVector);
				else if (delta1 < 0 && delta2 > 0)
					sceneMgr.rotate(-1*rotateVector);

			} else if (y1 < y2) {

				if(delta2>0 && delta1<0)
					sceneMgr.rotate(rotateVector);
				else if (delta2 < 0 && delta1 > 0)
					sceneMgr.rotate(-1*rotateVector);

			}

		}

	}

}
