using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioManager))]

public class Managers : MonoBehaviour {
    public static AudioManager Audio { get; private set; }

    private List<IGameManager> _startSequence;

    private void Awake() {
        Audio = GetComponent<AudioManager>();

        _startSequence = new List<IGameManager>() { Audio };
        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers() {
        var network = new NetworkService();

        foreach(IGameManager manager in _startSequence) {
            manager.Startup(network);
        }

        yield return null;

        var numModels = _startSequence.Count;
        var numReady = 0;

        while(numReady < numModels) {
            var lastReady = numReady;
            numReady = 0;

            foreach(IGameManager manager in _startSequence) {
                if(manager.status == ManagerStatus.Started) {
                    numReady++;
                }
            }

            if(numReady > lastReady) {
                Debug.Log($"Progress: {numReady} / {numModels}");
            }

            yield return null;         
        }

        Debug.Log("All managers started up");
    }
}
