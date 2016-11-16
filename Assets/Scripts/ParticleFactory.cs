using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum surface{
    Floor, Cube
};

public class ParticleFactory : MonoBehaviour {

    public GameObject floor_particle;
    public GameObject cube_particle;

    void Start() {

    }

    void Update() {

    }

    public void spawnParticle(RaycastHit pos) {
        GameObject particle;

        switch(pos.collider.gameObject.tag) {
            case "floor":
                particle = GameObject.Instantiate(floor_particle, pos.point, Quaternion.FromToRotation(pos.point, pos.normal));
                particle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);  // TODO: Doesnt seem to work, scale particle somwhere else
                break;
            case "cubes":
                particle = GameObject.Instantiate(cube_particle, pos.point, Quaternion.FromToRotation(pos.point, pos.normal));
                break;
            default: 
                particle = GameObject.Instantiate(floor_particle, pos.point, Quaternion.FromToRotation(pos.point, pos.normal));
                break;
        }

        GameObject.Destroy (particle, 3f);    // When new class for collisions are created use a queue to delete particls when there's too many
    }
	
}
