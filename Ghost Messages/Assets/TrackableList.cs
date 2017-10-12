using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableList : MonoBehaviour {
	public GameObject one;
	public bool oneIsActive = false;
//	public AudioSource bark;
	public AudioSource markerAudio;
	public AudioClip bark;


	void Start(){
		bark= Resources.Load("Sounds/Barking", typeof(AudioClip)) as AudioClip;
		markerAudio.GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update () {
		// Get the Vuforia StateManager
		StateManager sm = TrackerManager.Instance.GetStateManager ();

		// Query the StateManager to retrieve the list of
		// currently 'active' trackables 
		//(i.e. the ones currently being tracked by Vuforia)
		IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();

		// Iterate through the list of active trackables
		foreach (TrackableBehaviour tb in activeTrackables) {
//			Debug.Log("Trackable: " + tb.TrackableName);
			if (tb.TrackableName == "1-american-dollar") {
				oneIsActive = true;
				markerAudio.PlayOneShot(bark);

			} else {
				oneIsActive = false;
			}
		}

		Debug.Log ("one dollar marker active = : " + oneIsActive);
	}
}