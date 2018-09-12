using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration;
	private float countDownOfShake;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	public bool shakeHasBeenCalled=false; //wenn es bereits shaked verhindern dass es es nochmals tut

	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
			countDownOfShake=shakeDuration;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeHasBeenCalled) {

			MakeCameraShake ();

		}
	}


	void MakeCameraShake(){
		Debug.Log ("made call to shake");

			
			if (countDownOfShake > 0) {
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

				countDownOfShake -= Time.deltaTime * decreaseFactor;
			} 
		else {
				countDownOfShake = 0f;
				camTransform.localPosition = originalPos;
				countDownOfShake = shakeDuration;
				shakeHasBeenCalled = false;
				
			}

	}



	}
