using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] Text _scoreText;
	private Ball _ball;
	[SerializeField] private GameObject _completePanel;

	private void Start()
	{
		_ball = FindObjectOfType<Ball>();
		UpdateScore();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}
	}

	public void UpdateScore()
	{
		_scoreText.text = _ball.Score.ToString();
		_scoreText.rectTransform.DOScale(Vector3.one, .2f).From(Vector3.one * 1.2f);

		StartCoroutine(CheckWinRoutine());

		
	}

	private IEnumerator CheckWinRoutine()
	{
		yield return new WaitForSeconds(.2f);
		var collectibles = GameObject.FindGameObjectsWithTag("Collectible");
		if (collectibles.Length > 0)
		{

		}
		else
		{
			CompleteGame();
		}

	}

	public void CompleteGame()
	{
		_completePanel.SetActive(true);
		Time.timeScale = 0;
	}
}
