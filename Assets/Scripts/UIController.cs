using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	[SerializeField] private Text scoreLabel;
	[SerializeField] private SettingsPopup settingsPopup;

	private int _score;
		
	void Awake () {
		Messenger.AddListener (GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void OnDestroy () {
		Messenger.RemoveListener (GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void Start() {
		settingsPopup.Close ();
		scoreLabel.text = _score.ToString ();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnOpenSettings() {
		settingsPopup.Open ();
	}

	private void OnEnemyHit() {
		_score++;
		scoreLabel.text = _score.ToString ();
	}

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(settingsPopup.IsShowing()) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                settingsPopup.Close();
            } else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                settingsPopup.Open();
            }
        }
    }
}
