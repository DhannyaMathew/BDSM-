using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	private bool GamePaused;
	private GameObject PauseScreen;
	public static GameManager instance = null;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
	}

	void Start () {
		PauseScreen = GameObject.FindGameObjectWithTag ("PauseScreen");
	}
	
	void Pause ()
	{
		PauseScreen.SetActive (true);
		Time.timeScale = 0f;				//freezes the game
		GamePaused = true;
	}

	public void Resume ()
	{
		PauseScreen.SetActive (false);
		Time.timeScale = 1f;				//resumes the game
		GamePaused = false;
	}

	public void Restart ()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);		
	}
	public void Menu ()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Intro");

	}
	public void NextLevel ()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

	}
	public void Quit ()
	{
		Application.Quit ();
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Quit ();
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			if (GamePaused == true) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}
}
