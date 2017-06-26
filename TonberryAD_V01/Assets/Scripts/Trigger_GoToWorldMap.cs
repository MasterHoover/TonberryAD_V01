using UnityEngine;
using System.Collections;

public class Trigger_GoToWorldMap : EventTrigger 
{
	public WorldMap.Map fromWhichLevel;
	public const string LIDIKO_NAME = "Lidiko";
	public const string FOREST_NAME = "Forest";
	public const string MONTBLANC_NAME = "Montblanc";
	public const string CAPITALE_NAME = "Capitale";
	public const string OLD_ACADEMY_NAME = "OldAcademy";
	public const string VILLE_PORTUAIRE_NAME = "VillePortuaire";
	public const string TOWER_NAME = "Tower";

	protected override void LaunchEnterEvent (Collider col)
	{
		string levelName = "";
		switch (fromWhichLevel)
		{
		case WorldMap.Map.Lidiko : 
			levelName = LIDIKO_NAME;
			break;
		case WorldMap.Map.Forest:
			levelName = FOREST_NAME;
			break;
		case WorldMap.Map.Montblanc:
			levelName = MONTBLANC_NAME;
			break;
		case WorldMap.Map.Capitale:
			levelName = CAPITALE_NAME;
			break;
		case WorldMap.Map.OldAcademy:
			levelName = OLD_ACADEMY_NAME;
			break;
		case WorldMap.Map.VillePortuaire:
			levelName = VILLE_PORTUAIRE_NAME;
			break;
		case WorldMap.Map.Tower:
			levelName = TOWER_NAME;
			break;
		}
		GameManager.Instance.LoadWorldMap (levelName);
	}
}
