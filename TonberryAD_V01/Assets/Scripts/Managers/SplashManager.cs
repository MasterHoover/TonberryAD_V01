using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent (typeof (ScreenOverlay))]
public class SplashManager : MonoBehaviour 
{
	public SplashImage[] images;
	public Shader overlayShader;
	private ScreenOverlay overlay;
	private int index;

	private bool fadeIn;
	private bool fadeOut;

	void Awake ()
	{
		foreach (SplashImage image in images)
		{
			image.fadeInIncrement = image.fadeInIncrement > 0f ? image.fadeInIncrement : -image.fadeInIncrement;
			image.fadeOutIncrement = image.fadeOutIncrement > 0f ? -image.fadeOutIncrement : image.fadeOutIncrement;
		}
	}

	void Start ()
	{
		overlay = Camera.main.gameObject.AddComponent <ScreenOverlay> ();
		overlay.blendMode = ScreenOverlay.OverlayBlendMode.Additive;
		overlay.overlayShader = overlayShader;
		if(index < images.Length)
		{
			Go ();
		}
		else
		{
			GameManager.Instance.LoadLevel ("WorldMap", true);
		}
	}

	void Update ()
	{
		if(fadeIn && IncrementAlpha (images[index].fadeInIncrement))
		{
			fadeIn = false;
			Invoke ("StartFadeOut", images[index].fadeOutDelay);
		}
		else if (fadeOut && IncrementAlpha (images[index].fadeOutIncrement))
		{
			fadeOut = false;
			Next ();
		}
	}

	void Go ()
	{
		if (index < images.Length)
		{
			if(images[index].image != null)
			{
				overlay.texture = images[index].image;
				overlay.intensity = 0f;
				Invoke ("StartFadeIn", images[index].fadeInDelay);
			}
			else
			{
				Next ();
			}
		}
		else
		{
			enabled = false;
			Destroy (overlay);
			overlay = null;
			GameManager.Instance.LoadNextLevel ();
			Debug.Log ("Splash Done.");
		}
	}

	void Next ()
	{
		index++;
		Go ();
	}

	void StartFadeIn ()
	{
		fadeIn = true;
	}

	void StartFadeOut ()
	{
		fadeOut = true;
	}

	bool IncrementAlpha (float increment)
	{
		float newValue = overlay.intensity + increment * Time.deltaTime;
		newValue = newValue > 1f && increment > 0f ? 1f : newValue < 0f && increment < 0f ? 0f : newValue;
		overlay.intensity = newValue;
		return newValue == 0f || newValue == 1f;
	}

	[System.Serializable]
	public class SplashImage 
	{
		public Texture2D image;
		public float fadeInDelay = 1f;
		public float fadeInIncrement = 0.5f;
		public float fadeOutDelay = 3f;
		public float fadeOutIncrement = 0.5f;
	}
}
