using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour {

	public GameObject []obstacles;
	public bool spawn;



	// Use this for initialization
	void Start () {
		//InvokeRepeating (spawnTime, spawnTime);
		StartCoroutine(Timer());
	}
		



	IEnumerator Timer(){

		int wait = Random.Range (5, 10);
		yield return new WaitForSeconds(wait);
		SpawnFunction ();
		StartCoroutine (Timer ());

	}


	void SpawnFunction(){


		Instantiate (obstacles [Random.Range (0, obstacles.Length)], this.gameObject.transform.position, Quaternion.identity);

	}


}

