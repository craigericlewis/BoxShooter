using UnityEngine;
using System.Collections;

public class TargetBadBehaviour : MonoBehaviour
{

	// target impact on game
	public int scoreAmount = 0;
	public float timeAmount = 0.0f;

	public SpawnGameObjects spw;
	// explosion when hit?
	public GameObject explosionPrefab;
	public SpawnGameObjects TargetHit;

	// when collided with another gameObject
	void OnCollisionEnter (Collision newCollision)
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}
			GameObject Spawner = GameObject.Find("Spawner");
			Spawner.GetComponent<SpawnGameObjects>().TargetHit();
			if (GameManager.gm) {
				GameManager.gm.targetHit (scoreAmount, timeAmount);
			}

			// if game manager exists, make adjustments based on target properties
			//SpawnGameObjects.sp.TargetHit();
			//spw.TargetHit();
			// destroy the projectile

			Destroy (newCollision.gameObject);

			// destroy self
			Destroy (gameObject);
		}
	}
}
