using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class Kaya : MonoBehaviour {


	public float speed;
	public Transform target;
	NavMeshAgent agent;
	Vector3 velocity;

	public float timeToStop=5f; //After how long time after the last ray-cast hit should the agent stop following?
	float timer; //We need a timer for this

	Vector3 lookDirection;

	// Use this for initialization
	void Start () {
		velocity = Vector3.one * speed;
		timer = 0f;
		agent = GetComponent<NavMeshAgent> ();

	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

			if (Physics.Raycast (transform.position, transform.forward, out hit)) {
				if (hit.collider.name == "Mads") {
					timer = 0f;
					agent.SetDestination (target.position);
					agent.velocity = this.velocity;

				}
			}
		if (timer > timeToStop) {
			agent.Stop ();

		} else {
			agent.Resume ();
		}
		if (timer <= timeToStop + float.Epsilon) { //no need to keep counting the time if this is not true 
				timer += Time.deltaTime; 
		}
		}

}
