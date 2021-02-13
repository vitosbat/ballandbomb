﻿//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 
// Destroy and points logic
// Victory and defeat logic
// Object pooling

public class LevelManager : Singleton<LevelManager>
{
	GameManager gameManager;

	public LevelDataSO levelData;

	private List<GameObject> targets;


	void Start()
	{
		gameManager = GameManager.Instance;

		targets = levelData.Targets;

		StartCoroutine(SpawnTarget());
	}

	IEnumerator SpawnTarget()
	{
		while (gameManager.CurrentGameState == GameManager.GameState.GAMEPLAY)
		{
			yield return new WaitForSeconds(levelData.SpawnRate);
			
			int targetIndex = Random.Range(0, targets.Count);
			CreateTarget(targets[targetIndex]);
		}
	}

	void Update()
	{
		// Victory condition
		if (Input.GetKeyDown(KeyCode.W))
		{
			Debug.Log("You win!");
			gameManager.UpdateState(GameManager.GameState.ENDLEVEL);
		}

		// Defeat condition
		if (Input.GetKeyDown(KeyCode.L))
		{
			Debug.Log("You lost.");
			gameManager.UpdateState(GameManager.GameState.ENDLEVEL);
		}
	}

	void CreateTarget(GameObject targetObject)
	{
		Vector3 position = new Vector3(Random.Range(levelData.MinXSpawnPosition, levelData.MaxXSpawnPosition),
									   Random.Range(levelData.MinYSpawnPosition, levelData.MaxYSpawnPosition),
									   0);

		GameObject target = Instantiate(targetObject, position, transform.rotation);
		
		Rigidbody targetRb = target.GetComponent<Rigidbody>();

		targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

		targetRb.AddForce(RandomForce(), ForceMode.Impulse);

	}

	Vector3 RandomForce()
	{
		Vector3 xVector = Vector3.right * Random.Range(levelData.MinXForceValue, levelData.MaxXForceValue);
		Vector3 yVector = Vector3.up * Random.Range(levelData.MinYForceValue, levelData.MaxYForceValue);

		return xVector + yVector;
	}

	float RandomTorque()
	{
		return Random.Range(-levelData.TorqueRange, levelData.TorqueRange);
	}

	public void UpdateScore(int score)
	{
		Debug.Log("Points update: " + score);
		
		// TODO: invoke the event 
	}

}
