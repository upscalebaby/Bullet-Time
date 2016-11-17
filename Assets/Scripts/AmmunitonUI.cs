using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class AmmunitonUI : MonoBehaviour {

	Text text;

	string maxAmmo;
	string currentAmmo;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("M4A1") != null) {
			maxAmmo = GameObject.Find ("M4A1").GetComponent<M4A1Script> ().getAmmoCapacity ().ToString ();
			currentAmmo = maxAmmo;
		}
		text = GetComponent<Text> ();


	}
	
	// Update is called once per frame
	void Update () {
		currentAmmo = GameObject.Find ("M4A1").GetComponent<M4A1Script> ().getCurrentAmmo ().ToString ();
		text.text = currentAmmo + "/" + maxAmmo;
	}
}
