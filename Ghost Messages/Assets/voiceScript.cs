using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class voiceScript : MonoBehaviour,ITrackableEventHandler {
	//	public AudioSource markerAudio;
	public AudioClip clip;

	private TrackableBehaviour markerTB;
	// Use this for initialization
	void Start () {

		markerTB=GetComponent<TrackableBehaviour>();
		if (markerTB) {
			markerTB.RegisterTrackableEventHandler(this);
		}
		clip = Resources.Load ("sounds/0A") as AudioClip;
		TrackableList.markerAudio.clip = clip;
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
			//			Debug.Log ("Detected marker name: "+markerTB.TrackableName);
			if (markerTB.TrackableName == TrackableList.ghostMarker) {
				if (TrackableList.markerAudio != null) {
					TrackableList.markerAudio.Stop ();
				}

				clip = Resources.Load ("sounds/" + TrackableList.voiceIndex + "A") as AudioClip;
				Debug.Log ("tracked and hasGHost voiceIndex in if : " + TrackableList.voiceIndex);
				TrackableList.markerAudio = gameObject.AddComponent < AudioSource > ();
				TrackableList.markerAudio.clip = clip;
				TrackableList.markerAudio.Play ();

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
			//			if (markerAudio != null) {
			//				markerAudio.Stop ();
			//			}
		}
	}   
}
