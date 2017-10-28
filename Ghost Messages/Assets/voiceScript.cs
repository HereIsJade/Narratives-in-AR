using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class voiceScript : MonoBehaviour,ITrackableEventHandler {
	//	public AudioSource markerAudio;
	public AudioClip clip;
	private ParticleSystem fogPs;
	private ParticleSystem snowPs;
	private SpriteRenderer ghostMask;

	private TrackableBehaviour markerTB;
	// Use this for initialization
	void Start () {

		//remember!!!!!!!!!!gameObject.name+"fogParticle" //otherwise it's finding any gameobj with the name fogparticle
		fogPs = GameObject.Find (gameObject.name+"fogParticle").GetComponent<ParticleSystem> ();
		snowPs = GameObject.Find (gameObject.name+"snow particle").GetComponent<ParticleSystem> ();
		ghostMask=GameObject.Find (gameObject.name+"ghost mask").GetComponent<SpriteRenderer> ();

		markerTB=GetComponent<TrackableBehaviour>();
		if (markerTB) {
			markerTB.RegisterTrackableEventHandler(this);
		}
//		clip = Resources.Load ("sounds/0A") as AudioClip;
//		TrackableList.markerAudio.clip = clip;
	}
	private void PlayFog()
	{
		
		var em=fogPs.emission;
		em.enabled = true;
		fogPs.Play();
	}
	void StopFog()
	{
		var em=fogPs.emission;
		em.enabled = false;
		fogPs.Stop();
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
//			StopFog ();
			var emSnow = snowPs.emission;
			emSnow.enabled = false;
			snowPs.Stop ();
			
			//correct marker found, trigger voice, ghost mask

			if (markerTB.TrackableName == TrackableList.ghostMarker || TrackableList.ghostMarker=="") {
				if (TrackableList.markerAudio != null) {
					TrackableList.markerAudio.Stop ();
				}
				StopFog ();
				ghostMask.enabled = true;

				clip = Resources.Load ("sounds/" + TrackableList.voiceIndex + "A") as AudioClip;
				TrackableList.markerAudio = gameObject.AddComponent < AudioSource > ();
				TrackableList.markerAudio.clip = clip;
				TrackableList.markerAudio.Play ();

				if (TrackableList.voiceIndex >= 3) {			
					
					emSnow.enabled = true;
					snowPs.Play ();
				} else {
					Debug.Log ("TrackableList.voiceIndex=" + TrackableList.voiceIndex);

					emSnow.enabled = false;
					snowPs.Stop ();
					//should be false
					Debug.Log (markerTB.TrackableName+" Snow: " + snowPs+ "isPlaying"+snowPs.isPlaying);
				}

				if (TrackableList.voiceIndex < 5) {			
					TrackableList.voiceIndex++;
				} else {
					TrackableList.voiceIndex = 0;
				}


			} 
			else {
				//incorrect marker detected, trigger fog particle system
				PlayFog();
				ghostMask.enabled = false;


				Debug.Log (markerTB.TrackableName+" Fog: " + fogPs+ "isPlaying"+fogPs.isPlaying);
//				Debug.Log (markerTB.TrackableName+" Snow: " + fogPs+ "isPlaying"+snowPs.isPlaying);

			}



		}
		else
		{
			// Stop audio when target is lost
			//			if (markerAudio != null) {
			//				markerAudio.Stop ();
			//			}
		}
		Debug.Log ("ghostmarker: " + TrackableList.ghostMarker);

	}   
}
