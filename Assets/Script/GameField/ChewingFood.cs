using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChewingFood : MonoBehaviour {

	public AudioClip chew;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	private void OnCollision(Collider2D other)
	{
		audioSource.PlayOneShot(chew, 1f);
		Debug.Log ("chew");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
