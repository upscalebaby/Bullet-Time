using System.Collections;
using UnityEngine;

public class M4A1Script : MonoBehaviour {

	public float delayTime=0.2f; //To tweak the rate of fire

	float timer;

	Camera camera;  //kanske bara använd main.camera ist?
	Ray ray;
	Vector3 centerOfScreen;
	[SerializeField] GameObject muzzle_flash;
    [SerializeField] Transform muzzle_flash_spawnPoint;
    [SerializeField] GameObject collision_particle; // TODO: skapa ett abstraktionslager så inte vapnet behöver hantera detta

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

			if (isReady ()) {
				//spawn the muzzle flash effect (particle system)
				GameObject flash = (GameObject)Instantiate (muzzle_flash, muzzle_flash_spawnPoint.position, Quaternion.identity);
				Destroy (flash, 0.2f);
                if (Physics.Raycast (ray, out hit)) {   // Nice syntaktiskt socker med 'out'
                    Debug.Log (hit.transform.name);
                    GameObject collision = Instantiate (collision_particle, hit.point, Quaternion.identity);
                    Destroy (collision, 3f);    // When new class for collisions are created use a queue to delete particls when there's too many
                }

				timer = 0f; //reset the timer
			}

		}
	}
}
