using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Canvas))]
public class DialogCanvas : MonoBehaviour
{
	public Text nameText;
	public Text dialogText;
	private static DialogCanvas instance;
	private Canvas canvas;
	private const RenderMode RENDER_MODE = RenderMode.ScreenSpaceCamera;
	private const float PLANE_DISTANCE = 1.69f;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
			canvas = GetComponent<Canvas> ();
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	void Start ()
	{
		InitializeDialogCanvas ();
	}

	void OnLevelWasLoaded (int level)
	{
		InitializeDialogCanvas ();
	}

	void InitializeDialogCanvas ()
	{
		canvas.renderMode = RENDER_MODE;
		canvas.worldCamera = Camera.main;
		canvas.planeDistance = PLANE_DISTANCE;
	}
}
