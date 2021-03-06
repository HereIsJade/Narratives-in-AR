﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System.Linq;


public class TrackableList : MonoBehaviour {

	public static int voiceIndex = 0;
	public static AudioSource markerAudio;
	public bool firstActiveMarker = false;

	private int ghostMarkerIndex=0;
	private string[] unvisitedMarkers = { "the-fool", "Priestess", "Hermit", "WheelFortune", "World" };

	public static string ghostMarker="";

	public Canvas endingCanvas;

	private void removeMarker(string marker){
		if (unvisitedMarkers.Length == 5) {
			string markerToRemove = marker;
			unvisitedMarkers = unvisitedMarkers.Where (val => val != markerToRemove).ToArray ();

			//after removing, randomly select an unvisited marker as the ghost marker
			ghostMarkerIndex = Random.Range (0, unvisitedMarkers.Length);
			ghostMarker = unvisitedMarkers [ghostMarkerIndex];
		} else {
			string markerToRemove = marker;
			unvisitedMarkers = unvisitedMarkers.Where (val => val != markerToRemove).ToArray ();
			//after removing, randomly select an unvisited marker as the ghost marker
			if (unvisitedMarkers.Length != 0) {
				ghostMarkerIndex = Random.Range (0, unvisitedMarkers.Length);
				ghostMarker = unvisitedMarkers [ghostMarkerIndex];
			} else {

			}
		}

	}

	void Start(){
		endingCanvas = GameObject.Find("endingCanvas").GetComponent<Canvas>();
		endingCanvas.enabled = false;
	}
	// Update is called once per frame
	void Update () {

		if (unvisitedMarkers.Length <= 0) {
			endingCanvas.enabled = true;
		}
		// Get the Vuforia StateManager
		StateManager sm = TrackerManager.Instance.GetStateManager ();

		// Query the StateManager to retrieve the list of
		// currently 'active' trackables 
		//(i.e. the ones currently being tracked by Vuforia)
		IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();

		// Iterate through the list of active trackables
		foreach (TrackableBehaviour tb in activeTrackables) {
			if (firstActiveMarker == false) {
				
				ghostMarker = tb.TrackableName;
				firstActiveMarker = true;
				removeMarker (ghostMarker);
			} else {
				if (tb.TrackableName == "the-fool" && ghostMarker == "the-fool") {
					removeMarker ("the-fool");
				}
				if (tb.TrackableName == "Priestess" && ghostMarker == "Priestess") {
					removeMarker ("Priestess");
				}
				if (tb.TrackableName == "Hermit" && ghostMarker == "Hermit") {
					removeMarker ("Hermit");
				}
				if (tb.TrackableName == "WheelFortune" && ghostMarker == "WheelFortune") {
					removeMarker ("WheelFortune");
				}
				if (tb.TrackableName == "World" && ghostMarker == "World") {
					removeMarker ("World");
				}
			}
		}

	}
}