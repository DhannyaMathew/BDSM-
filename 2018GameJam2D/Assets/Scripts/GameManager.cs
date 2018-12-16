using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	private bool GamePaused;
	public GameObject PauseScreen;
	public static GameManager instance = null;
	public bool endLevel;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
	}

	void Start () {
		
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
		SceneManager.LoadScene ("Menu");

	}
	public void NextLevel ()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

	}

	public void winLevel(){
	}
	public void loseLevel(){
	}

	public void Quit ()
	{
		Application.Quit ();
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && GamePaused) {
			Quit ();
		}

		if (Input.GetKeyDown (KeyCode.Escape) && !GamePaused) {
			Pause ();
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
