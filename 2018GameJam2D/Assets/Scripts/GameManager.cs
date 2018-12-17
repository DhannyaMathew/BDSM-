using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	private bool GamePaused;
	public GameObject PauseScreen;
	public static GameManager instance = null;
	public bool endLevel;
	public GameObject Fail, Pass;
	public AudioClip pass,fail;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
	}

	void Start () {
		FadeEffect.instance.FadeIn (0.5f);
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
		FadeEffect.instance.FadeOut (0.5f);
	}

	public void winLevel(){
		Pause();
		//Pass Stamp
		SoundController.instance.Playone(pass);
		Pass.SetActive(true);
	}
	public void loseLevel(){
		Pause();
		//Fail Stamp
		SoundController.instance.Playone(fail);
		Fail.SetActive(true);
	}

	public void Quit ()
	{
		Application.Quit ();
	}
	void Update () {
		if ((Input.GetKeyDown (KeyCode.Escape) && GamePaused) || endLevel) {
			Quit ();
		}

		if (Input.GetKeyDown (KeyCode.Escape) && !GamePaused && !endLevel) {
			Pause ();
		}

		if (Input.GetKeyDown (KeyCode.P) && !endLevel) {
			if (GamePaused == true) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}
}
