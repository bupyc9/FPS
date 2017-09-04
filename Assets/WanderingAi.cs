using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAi : MonoBehaviour {
	public float speed = 3.0f;
	public float obstracleRange = 5.0f;
	private bool alive;

	void Start () {
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			transform.Translate (0, 0, speed * Time.deltaTime);

			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast (ray, 0.75f, out hit)) {
				if (hit.distance < obstracleRange) {
					float angle = Random.Range (-110, 110);
					transform.Rotate (0, angle, 0);
				}
			}
		}
	}

	public void SetAlive (bool alive) {
		this.alive = alive;
	}
}
