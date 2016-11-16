using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Kaya : MonoBehaviour {



	public Transform target;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if(Physics.Raycast (transform.position, transform.forward, out hit)){
			if (hit.collider.name=="Mads") {
				Debug.Log ("hit");
				agent.Resume ();
				agent.SetDestination (target.position);
			} else
				agent.Stop ();
		}

	}

}
