using System.Collections;
using UnityEngine;

//TODO: Create a weapon class that this and other weapons can inherit from
public class M4A1Script : MonoBehaviour {


	public float delayTime=0.2f; //To tweak the rate of fire
    public ParticleFactory particleFactory;     // This is not very clean, particleFactory should be globally accessible

	float timer;

	Ray ray;
	Vector3 centerOfScreen;
	[SerializeField] GameObject muzzle_flash;
    [SerializeField] Transform muzzle_flash_spawnPoint;
    [SerializeField] GameObject collision_particle; // TODO: skapa ett abstraktionslager så inte vapnet behöver hantera detta
	[SerializeField] int ammunition_capacity=60; //TODO: -||-

	int currentAmmo;


	void Start () {
		currentAmmo = ammunition_capacity;
		timer = delayTime; //the gun should be ready at start
		centerOfScreen = new Vector3 (0.5f, 0.5f, 0);
		//Make the gun target the middle of the screen (0.5; 0.5) where the crosshair is
		transform.LookAt (Camera.main.ScreenToViewportPoint (centerOfScreen));
		transform.RotateAround (transform.position, Vector3.up, 180f); // FIXME: Change the guns rotation before importing it
	}
		
	bool isReady()
	{
		if (timer >= delayTime && currentAmmo > 0) 
			return true;
		return false;
	}

	void decrementAmmo(){
		currentAmmo--;
	}

	public int getCurrentAmmo(){ //TODO: Maybe not use static methods?
		return currentAmmo;
	}
	public int getAmmoCapacity(){
		return ammunition_capacity;
	}
		
	void Update () {
		//ray cast at the middle of the screen
		ray = Camera.main.ViewportPointToRay (centerOfScreen);
		//TODO: Add some recoil?
		RaycastHit hit;

		if (Input.GetKeyDown ("r")) {
			this.currentAmmo = ammunition_capacity; //TODO: Reload time?
		}

		if (Input.GetMouseButton (0)) {
			timer+=Time.deltaTime; // make the timer independent of frame rate.

			if (isReady ()) {
				//spawn the muzzle flash effect (particle system)
				GameObject flash = (GameObject)Instantiate (muzzle_flash, muzzle_flash_spawnPoint.position, Quaternion.identity);
				decrementAmmo ();
				Destroy (flash, 0.2f);
                if (Physics.Raycast (ray, out hit)) {
                    Debug.Log (hit.transform.name);
                    particleFactory.spawnParticle(hit);
                }

				timer = 0f; //reset the timer
			}

		}
	}
}
