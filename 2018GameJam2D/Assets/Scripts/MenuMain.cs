using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour {




	void Start () {
		//FadeEffect.instance.FadeOut (0.5f);
	}
		


	public void NextLevel ()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		FadeEffect.instance.FadeIn (0.5f);
	}
		

	public void Quit ()
	{
		Application.Quit ();
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Quit ();
		}


	}
}
