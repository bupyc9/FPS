using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {
	public void ReactToHit() {
		WanderingAi behavior = GetComponent<WanderingAi> ();
		if (behavior != null) {
			if (behavior.IsAlive()) {
				StartCoroutine (Die());
				Messenger.Broadcast (GameEvent.ENEMY_HIT);
			}
			behavior.SetAlive (false);
		}	
	}

	private IEnumerator Die() {
		this.transform.Rotate (-75, 0, 0);
		yield return new WaitForSeconds (1.5f);
		Destroy (this.gameObject);
	}
}
