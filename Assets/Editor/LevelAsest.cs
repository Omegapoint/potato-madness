using UnityEngine;
using UnityEditor;
using System;

public class LevelAsest
{
	[MenuItem("Assets/Create/GameLevel")]
	public static void CreateAsset ()
	{
		CustomAssetUtility.CreateAsset<GameLevel>();
	}
}