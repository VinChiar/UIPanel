using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShakeScript : MonoBehaviour {

	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! ;)
	float shakeDetectionThreshold = 1.5f;
	private float lowPassFilterFactor;
	private Vector3 lowPassValue = Vector3.zero;
	private Vector3 acceleration;
	private Vector3 deltaAcceleration;
	private PointerManager ptrMgr;
	private Button deSelect;

	public void Start()
	{	
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		//shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
		ptrMgr = GameObject.FindObjectOfType<PointerManager> ();
		deSelect = GameObject.Find("Deselect").GetComponent<Button>();

	}

	public void Update()
	{	
		acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		deltaAcceleration = acceleration - lowPassValue;

		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold && !ptrMgr.getSelectable())
		{	

			ptrMgr.deSelect ();
			//Debug.Log("Shake event detected at time "+Time.time);

		}
	}
}