﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    private int score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public GameObject sensor;
    public List<GameObject> targets;
    public bool isGameActive;
    public Button restartButton;


    public void StartGame(int difficulty)
	{
        isGameActive = true;
        titleScreen.SetActive(false);
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
	}

    public void UpdateScore(int scoreToAdd)
	{
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score >= 100)
		{
            Debug.Log("You win!");
            GameOver();
        }
        else if (score <= -100)
		{
            Debug.Log("You lose, but not loser)");
            GameOver();
		}
    }

    public void GameOver()
	{
        isGameActive = false;
        sensor.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

    public void ReloadGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
    	
	IEnumerator SpawnTarget()
	{
        while (isGameActive)
		{
            yield return new WaitForSeconds(spawnRate);
            int targetIndex = Random.Range(0, targets.Count);
            Instantiate(targets[targetIndex]);
		}
	}
}
