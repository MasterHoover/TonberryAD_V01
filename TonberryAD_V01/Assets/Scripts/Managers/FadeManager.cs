using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour 
{
	private static FadeManager instance;
	public Texture2D blackScreen;
	public Shader shader;
	public static float defaultFadeOutInc = 2f;
	public static float defaultFadeInInc = 2f;
	private ScreenOverlay camOverlay;
	private bool fading;
	private float increment;

	private bool queueChangeLevel;
	private int levelToLoad;
	private string levelToLoadName;
	private bool fadeOutAfterLevelChange;
	private FadeProperties fadeProperties;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else 
		{
			Destroy (gameObject);
		}
			
		if(blackScreen == null || shader == null)
		{
			Debug.LogError ("FadeManager/Awake () : Missing propertie(s).");
			enabled = false;
		}
	}

	void Start ()
	{
		RefreshCamOverlay ();
		switch (SceneManager.GetActiveScene ().buildIndex)
		{
		case 1 : 
			FadeManager.Instance.FadeOut (1f, 1f);
			break;
		}
	}

	void OnLevelWasLoaded (int level)
	{
		RefreshCamOverlay ();
		if (fadeOutAfterLevelChange)
		{
			fadeOutAfterLevelChange = false;
			FadeOut (fadeProperties.delay, fadeProperties.increment);
			fadeProperties = null;
		}

		switch (level)
		{
		case 1 : 
			FadeManager.Instance.FadeOut (1f, 1f);
			break;
		}
	}

	void Update ()
	{
		if (fading && Increment ())
		{
			fading = false;
			SendMessage ("FadeOver");
			if (queueChangeLevel)
			{
				queueChangeLevel = false;
				if (levelToLoad > 0)
				{
					GameManager.Instance.LoadLevel (levelToLoad);
				}
				else
				{
					GameManager.Instance.LoadLevel (levelToLoadName);
				}
			}
		}
	}

	/// <summary>
	/// Refresh Cam Overlay.
	/// </summary>
	public void RefreshCamOverlay ()
	{
		fading = false;
		camOverlay = Camera.main.GetComponent <ScreenOverlay> ();
		if (camOverlay == null)
		{
			camOverlay = Camera.main.gameObject.AddComponent <ScreenOverlay> ();
		}
		camOverlay.overlayShader = shader;
		camOverlay.texture = blackScreen;
		camOverlay.blendMode = ScreenOverlay.OverlayBlendMode.AlphaBlend;
		camOverlay.intensity = 0f;
	}

	void StartFading ()
	{
		fading = true;
	}

	public void FadeIn (float delay, float inc)
	{
		CancelInvoke ("StartFadeIn");
		camOverlay.intensity = 0f;
		increment = Mathf.Abs (inc);
		Invoke ("StartFading", delay);
	}

	public void FadeIn (float delay)
	{
		FadeIn (delay, defaultFadeInInc);
	}

	public void FadeIn ()
	{
		FadeIn (0f, defaultFadeInInc);
	}

	public void FadeOut (float delay, float inc)
	{
		CancelInvoke ("StartFadeOut");
		camOverlay.intensity = 1f;
		increment = -Mathf.Abs (inc);
		Invoke ("StartFading", delay);
	}

	public void FadeOut (float delay)
	{
		FadeOut (delay, defaultFadeOutInc);
	}

	public void FadeOut ()
	{
		FadeOut (0f, defaultFadeOutInc);
	}

	public void FadeInAndChangeLevel (float delay, float inc, int level)
	{
		queueChangeLevel = true;
		levelToLoad = level;
		FadeIn (delay, inc);
	}

	public void FadeInAndChangeLevel (float inc, int level)
	{
		FadeInAndChangeLevel (0f, inc, level);	
	}

	public void FadeInAndChangeLevel (float delay, float inc, string levelName)
	{
		queueChangeLevel = true;
		levelToLoad = -1;
		levelToLoadName = levelName;
		FadeIn (delay, inc);
	}

	public void FadeInAndChangeLevel (float inc, string levelName)
	{
		FadeInAndChangeLevel (0f, inc, levelName);
	}

	public void FadeInAndChangeLevel (string levelName)
	{
		FadeInAndChangeLevel (0f, defaultFadeInInc, levelName);
	}

	public void FadeInAndChangeLevel (int level)
	{
		FadeInAndChangeLevel (0f, defaultFadeInInc, level);
	}

	public void FadeOutAfterChangingLevel (float delay, float inc)
	{
		fadeOutAfterLevelChange = true;
		fadeProperties = new FadeProperties (FadeProperties.FadeType.FadeOut ,delay, increment);
	}

	public void FadeOutAfterChangingLevel (float inc)
	{
		FadeOutAfterChangingLevel (0f, inc);
	}

	public void FadeOutAfterChangingLevel ()
	{
		FadeOutAfterChangingLevel (0f, defaultFadeOutInc);
	}

	bool Increment ()
	{
		camOverlay.intensity += increment * Time.deltaTime;
		if (camOverlay.intensity <= 0f && increment < 0f)
		{
			camOverlay.intensity = 0f;
		}
		else if (camOverlay.intensity >= 1f && increment > 0f)
		{
			camOverlay.intensity = 1f;
		}
		return camOverlay.intensity <= 0f && increment < 0f || camOverlay.intensity >= 1f && increment > 0f;
	}
		
	public static FadeManager Instance
	{
		get{return instance;}	
	}

	[System.Serializable]	
	public class FadeProperties
	{
		public enum FadeType
		{
			FadeIn,
			FadeOut
		}

		public FadeType type;
		public float delay;
		public float increment;

		public FadeProperties (FadeType type)
		{
			this.delay = 0f;
			switch (type)
			{
			case FadeType.FadeIn :
				this.increment = defaultFadeInInc;
				break;
			case FadeType.FadeOut :
				this.increment = defaultFadeOutInc;
				break;
			default :
				Debug.LogWarning ("FadeManager/FadeProperties/FadeProperties (FadeType) : fadeType is unknown at variable construct. Increment will be 1f.");
				this.increment = 1f;
				break;	
			}
		}

		public FadeProperties (FadeType type, float delay, float increment)
		{
			this.type = type;
			this.delay = delay;
			this.increment = increment;
		}
	}
}
