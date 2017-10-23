using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TouchScript : MonoBehaviour {
	void Update() {
		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++;

		}
		if (fingerCount > 0)
			SceneManager.LoadScene ("basic");

	}
}