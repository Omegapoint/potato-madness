using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace UnityTest
{
	[TestFixture]
	[Category("GameManager Tests")]
	internal class GameManagerTest
	{
		[Test]
		[Category("Level Handling")]
		public void ShouldDetermineThatWeaAreAtTheFinalLevelWithOnlyOneLevelInTotal()
		{
			// given
			GameObject go = new GameObject();
			go.AddComponent<GameManager>();

			GameLevel[] gameLevels = new GameLevel[1] {GameLevel.CreateInstance<GameLevel>()};

			GameManager gameManager = go.GetComponent<GameManager> ();
			gameManager.levels = gameLevels;
			// when
			bool isFinalLevel = gameManager.IsFinalLevel();
			// then
			Assert.IsTrue(isFinalLevel);
		}
			
	}
}
