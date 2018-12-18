using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

	public Transform[] ToyPos;
	int moveToy = -1;
	public GameObject[] Toys;
	int randomToy;
	public float conveyorSpeed;
	private Material Belt;
	private Vector2 offSet;
	public float Xvel,Yvel;
	public AudioClip ConveyorMove;

	// Use this for initialization
	void Start () {
		Belt = GetComponent<Renderer> ().material;
		//ToyFilled = new int[ToyPos.Length];
		for (int i = 0; i < ToyPos.Length; i++) {
			//ToyFilled [i] = i + 1;
			randomToy = Random.Range (0, Toys.Length - 1);
			Instantiate (Toys [randomToy], ToyPos [i].position, ToyPos[i].rotation, ToyPos [i]);
		}
	}


	IEnumerator moveBelt(){
		
			if (moveToy != -1) {
				this.GetComponent<AudioSource>().clip = ConveyorMove;
				this.GetComponent<AudioSource> ().Play ();

				offSet = new Vector2 (Xvel*0.2f, Yvel);
				
				for (int j = 0; j <= moveToy; j++) { //2
					
					if (ToyPos [0].childCount != 0) {
					GameObject toyChild= ToyPos [0].GetChild (0).gameObject;
						Destroy (toyChild);
					}
					for (int k = 0; k < ToyPos.Length-1; k++) {
					if (ToyPos [k+1].childCount != 0) {
						//Debug.Log (ToyPos [k + 1].GetChild (0).tag);
						ToyPos [k + 1].GetChild (0).transform.position = ToyPos [k].position;
						ToyPos [k + 1].GetChild (0).GetComponent<ToyScript> ().toyWalkPos = ToyPos [k + 1].GetChild (0).transform.position;
						ToyPos [k + 1].GetChild (0).transform.parent = ToyPos [k];

						//ToyPos[k].GetChild(0).transform.position =  ToyPos[k].position;

					} 
					}
					randomToy = Random.Range (0, Toys.Length - 1);
				Instantiate (Toys [randomToy], ToyPos [ToyPos.Length - 1].position, ToyPos[ToyPos.Length - 1].rotation, ToyPos [ToyPos.Length - 1]);
					yield return new WaitForSeconds (conveyorSpeed);
				}
				offSet = new Vector2 (0, 0);
				moveToy = -1;
				this.GetComponent<AudioSource> ().Stop();
			}
			//Belt.mainTextureOffset += offSet * Time.deltaTime;

	}
	// Update is called once per frame
	void Update () {
		Belt.mainTextureOffset += offSet * Time.deltaTime;
		if (moveToy == -1) {
			for (int i = 0; i < ToyPos.Length; i++) {
				//ToyFilled [i] = i + 1;
				if (ToyPos [i].childCount == 0) {
					moveToy = i;

				}
			}
			if (moveToy != -1) {
				StartCoroutine ("moveBelt");
			}
		}
	}

}
