using System.Collections;
using UnityEngine;

public class M4A1Script : MonoBehaviour {

	public float delayTime=0.2f; //To tweak the rate of fire

	float timer;

	Camera camera;
	Ray ray;
	Vector3 centerOfScreen;
	[SerializeField] GameObject muzzle_flash;
	[SerializeField] Transform muzzle_flash_spawnPoint;

	void Start () {
		timer = delayTime; //the gun should be ready at start
		camera = Camera.main;
		centerOfScreen = new Vector3 (0.5f, 0.5f, 0);
		//Make the gun target the middle of the screen (0.5; 0.5) where the crosshair is
		transform.LookAt (camera.ScreenToViewportPoint (centerOfScreen));
		transform.RotateAround (transform.position, Vector3.up, 180f); // FIXME: Change the guns rotation before importing it
	}
		
	bool isReady()
	{
		if (timer >= delayTime) 
			return true;
		return false;
	}

	void Update () {
		//ray cast at the middle of the screen
		ray = camera.ViewportPointToRay (centerOfScreen);
		//TODO: Add some recoil?
		RaycastHit hit;
	

		if (Input.GetMouseButton (0)) {
			
			timer+=Time.deltaTime; // make the timer independent of frame rate.
			Debug.Log (isReady());
			if (isReady ()) {
				//spawn the muzzle flash effect (particle system)
				GameObject flash = (GameObject)Instantiate (muzzle_flash, muzzle_flash_spawnPoint.position, Quaternion.identity);
				Destroy (flash, 0.2f);
				if (Physics.Raycast (ray, out hit))
					Debug.Log (hit.transform.name);
				timer = 0f; //reset the timer
			}

		}
	}
}
