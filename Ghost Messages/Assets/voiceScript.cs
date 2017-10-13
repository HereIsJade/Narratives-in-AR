using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class voiceScript : MonoBehaviour,ITrackableEventHandler {
	public AudioSource markerAudio;
	public AudioClip clip;
	public bool voiceOn = false;
	private TrackableBehaviour markerTB;
	// Use this for initialization
	void Start () {
		
		markerTB=GetComponent<TrackableBehaviour>();
		if (markerTB) {
			markerTB.RegisterTrackableEventHandler(this);
		}

	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		Debug.Log ("voiceindex: "+TrackableList.voiceIndex);
		Debug.Log ("ghostmarker: " + TrackableList.ghostMarker);
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			Debug.Log ("Detected marker name: "+markerTB.TrackableName);
			if (markerTB.TrackableName == TrackableList.ghostMarker) {
				
				clip = Resources.Load ("sounds/" + TrackableList.voiceIndex + "A") as AudioClip;
				Debug.Log ("tracked and hasGHost voiceIndex in if : " + TrackableList.voiceIndex);
				markerAudio = gameObject.AddComponent < AudioSource > ();
				markerAudio.clip = clip;
				markerAudio.Play ();

				if (TrackableList.voiceIndex < 5) {			
					TrackableList.voiceIndex++;
				} else {
					TrackableList.voiceIndex = 0;
				}
			}
		}
		else
		{
			// Stop audio when target is lost
			markerAudio.Stop ();
		}
	}   
}
