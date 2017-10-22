using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAi : MonoBehaviour {
	public float speed = 3.0f;
	public float obstracleRange = 5.0f;
	private bool alive;
	public const float baseSpeed = 3.0f;

	[SerializeField] private GameObject fireballPrefab;
	private GameObject fireball;

	void Awake () {
		Messenger<float>.AddListener (GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}
		
	void Destroy () {
		Messenger<float>.RemoveListener (GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnSpeedChanged(float value) {
        Debug.Log($"speed: {value}");
		speed = baseSpeed * value;
	}

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
				GameObject hitObject = hit.transform.gameObject;
				if (hitObject.GetComponent<PlayerCharacter> ()) {
					if (fireball == null) {
						fireball = Instantiate (fireballPrefab) as GameObject;
						fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
						fireball.transform.rotation = transform.rotation;
					}
				} else if (hit.distance < obstracleRange) {
					float angle = Random.Range (-110, 110);
					transform.Rotate (0, angle, 0);
				}
			}
		}
	}

	public void SetAlive (bool alive) {
		this.alive = alive;
	}

	public bool IsAlive() {
		return alive;
	}
}
