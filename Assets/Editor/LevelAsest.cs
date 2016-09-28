using UnityEngine;
using UnityEditor;
using System;

public class LevelAssets {
	[MenuItem("Assets/Create/GameLevel")]
	public static void CreateAsset () {
		CustomAssetUtility.CreateAsset<GameLevel>();
	}
}