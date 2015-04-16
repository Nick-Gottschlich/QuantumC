//Code obtained from https://gist.github.com/ftvs/5822103

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public static float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public static float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	public bool shaking = false;
	
	Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shaking) {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
		}
		if (shake > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
//			camTransform.localPosition = originalPos;
			originalPos = camTransform.localPosition;
		}
	}
}
