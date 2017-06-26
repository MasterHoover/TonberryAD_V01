using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
	private CameraViewType cameraType;
	private float camDistance;
	private static CameraManager instance;

	// for traveling anim
	private bool travelingAnim;
	private Vector3 fromTravel;
	private Vector3 toTravel;
	private float travelSpeed;
	private float camDistOffset;

	public enum CameraViewType
	{
		Fix,
		FullTraveling,
		SemiTraveling
	}

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	void LateUpdate ()
	{
		if (cameraType == CameraViewType.FullTraveling)
		{
			if (!travelingAnim)
			{
				Camera.main.transform.position = GameManager.Instance.Player.transform.position - Camera.main.transform.forward * camDistance;
			}
			else
			{
				float moveDist = travelSpeed * Time.deltaTime;
				if (moveDist <= Vector3.Distance (Camera.main.transform.position, toTravel))
				{
					Camera.main.transform.position = toTravel;
					travelingAnim = false;
					FixCamera ();
				}
				else
				{
					Camera.main.transform.Translate ((toTravel - fromTravel).normalized * moveDist);
				}
			}
		}
		else if (cameraType == CameraViewType.SemiTraveling)
		{
			float angleDeg = Vector3.Angle (Camera.main.transform.right, Vector3.forward);
			float angleRad = angleDeg * Mathf.Deg2Rad;
			float dist = Mathf.Sin (angleDeg) * (GameManager.Instance.Player.transform.position.z + camDistOffset);
			camDistance = dist;
			Camera.main.transform.position = GameManager.Instance.Player.transform.position - Camera.main.transform.forward * camDistance;
		}
	}

	float GetCamDistanceFromPlane (Vector3 point)
	{
		return GetCamDistanceFromPlane (point.y);
	}

	float GetCamDistanceFromPlane (float planeYPos)
	{
		float heightOffset = Camera.main.transform.position.y - planeYPos;
		float angleInDegree = Vector3.Angle (Vector3.down, Camera.main.transform.forward);
		float angleInRad = angleInDegree * Mathf.Deg2Rad;
		float cosValue = Mathf.Cos (angleInRad);
		float hyp = heightOffset / cosValue;
		return hyp;
	}

	public void CenterCameraToPlayer ()
	{
		if (GameManager.Instance.Player != null)
		{
			float distanceFromPlayer = GetCamDistanceFromPlane (GameManager.Instance.Player.transform.position.y);
			Vector3 targetSpot = GameManager.Instance.Player.transform.position - Camera.main.transform.forward * distanceFromPlayer;
			Camera.main.transform.position = targetSpot;
		}
		else
		{
			Debug.LogWarning ("CameraManager/CenterCameraToPlayer () : Player doesn't exists.");
			CameraManager.Instance.FixCamera ();
		}
	}

	public void FixCamera ()
	{
		cameraType = CameraViewType.Fix;
	}

	public void FixCamera (Transform camReference)
	{
		if (camReference != null)
		{
			FixCamera (camReference.position, camReference.rotation);
		}
		else
		{
			Debug.LogWarning ("CameraManager/FixCamera (Transform) : CamReference is null.");
		}
	}
		
	public void FixCamera (Vector3 camPos, Quaternion camRot)
	{
		FixCamera ();
		ChangeCameraView (camPos, camRot);
	}

	public void ChangeCameraView (Transform posReference)
	{
		if (posReference != null)
		{
			ChangeCameraView (posReference.position, posReference.rotation);
		}
		else
		{
			Debug.LogWarning ("CameraManager/ChangeCameraView (Transform) : posReference is null");
		}
	}

	public void ChangeCameraView (Vector3 position, Quaternion rotation)
	{
		ChangeCameraView (position);
		ChangeCameraView (rotation);
	}

	public void ChangeCameraView (Vector3 position)
	{
		Camera.main.transform.position = position;
	}

	public void ChangeCameraView (Quaternion rotation)
	{
		Camera.main.transform.rotation = rotation;
	}

	public void ChangeCameraView (CameraViewType type, Transform camPos, float camDistance)
	{
		if (camPos != null)
		{
			if (type == CameraViewType.Fix)
			{
				FixCamera (camPos);
			}
			else if (type == CameraViewType.FullTraveling)
			{
				TravelCamera (camPos, camDistance);
			}
			else if (type == CameraViewType.SemiTraveling)
			{
				SemiTravelCamera (camPos);
			}
			else
			{
				Debug.LogWarning ("CameraManager/ChangeCameraView (CameraViewType, Transform) : camViewType is unknown.");
			}
		}
		else
		{
			Debug.LogWarning ("CameraManager/ChangeCameraView (CameraViewType, Transform) : camPos is null");
		}
	}

	public void ChangeCameraView (CameraViewType type, Vector3 camPos, Quaternion camRot)
	{
		if (type == CameraViewType.Fix)
		{
			FixCamera (camPos, camRot);
		}
		else if (type == CameraViewType.FullTraveling)
		{
			TravelCamera (camPos, camRot);
		}
		else
		{
			Debug.LogWarning ("CameraManager/ChangeCameraView (CameraViewType, Vector3, Quaternion) : camViewType is unknown.");
		}
	}

	public void ChangeCameraView (CameraViewType type, Transform camReference, Transform orientation, float camDistance)
	{
		ChangeCameraView (type, camReference, camDistance);
		ChangeOrientation (orientation);
	}

	public void ChangeCameraView (CameraView info)
	{
		ChangeCameraView (info.type, info.cameraPosition, info.orientation, info.camDistance);
	}

	public void TravelCamera ()
	{
		cameraType = CameraViewType.FullTraveling;
	}

	public void TravelCamera (Transform camPos, float camDistance)
	{
		TravelCamera ();
		this.camDistance = camDistance;
		if (camPos != null)
		{
			TravelCamera (camPos.position, camPos.rotation);
		}
		else
		{
			Debug.Log ("CameraManager/TravelCamera (Transform) : camPos is null.");
		}
	}

	public void TravelCamera (Vector3 position, Quaternion rotation)
	{
		TravelCamera ();
		Camera.main.transform.position = position;
		Camera.main.transform.rotation = rotation;
	}

	public void SemiTravelCamera (Transform camPos)
	{
		if (camPos != null)
		{
			cameraType = CameraViewType.SemiTraveling;
			Camera.main.transform.position = camPos.position;
			Camera.main.transform.rotation = camPos.rotation;
			camDistOffset = Vector3.Distance (Vector3.zero, camPos.position);
		}
		else
		{
			Debug.LogWarning ("CameraManager/SemiTravelCamera (Transform) : camPos reference is null.");
		}
	}

	public void ChangeOrientation (Transform newOrientation)
	{
		if (newOrientation != null)
		{
			GameManager.Instance.ChangeCardinals (newOrientation);
		}
		else
		{
			Debug.LogWarning ("CameraManager/ChangeOrientation (Transform) : Orientation is null.");
		}
	}

	public CameraViewInfo CurrentView
	{
		get
		{
			if (GameManager.Instance.CardinalPoints != null)
			{
				return new CameraViewInfo (cameraType, Camera.main.transform.position, Camera.main.transform.rotation, GameManager.Instance.CardinalPoints.position, GameManager.Instance.CardinalPoints.rotation);
			}
			return new CameraViewInfo (cameraType, Camera.main.transform.position, Camera.main.transform.rotation);
		}
	}
		
	public static CameraManager Instance
	{
		get{return instance;}
	}
}
