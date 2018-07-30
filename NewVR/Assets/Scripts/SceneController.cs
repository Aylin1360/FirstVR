using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneController : MonoBehaviour {
	RaycastHit hit;
	public float timer;
	public Image crosshairfill;
	public GameObject pause,play,reset,next,popupButton, popupVideo;
	public VideoPlayer videoPlayer;
	bool isPause= false;

	public int videoClipındex;
	public VideoClip[] videoClips;

	// Use this for initialization
	void Start () {
		play.SetActive (false);
		popupVideo.SetActive (false);
		popupButton.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		Buttonınteractions ();
		AutoPlay ();
	}
	void Vid2Check(){
		if (videoClipındex == 1) {
			popupButton.SetActive(true);
		}
	}
	void AutoPlay(){
		if (!videoPlayer.isPlaying) {
			if (isPause == false) {
				videoClipındex++;
				if (videoClipındex == 3)
					videoClipındex = 0;

				Vid2Check ();
				videoPlayer.clip = videoClips [videoClipındex];
				videoPlayer.Play ();
			}
		}
	}
	void Buttonınteractions(){
		crosshairfill.fillAmount = timer / 2;
		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit, 1000f)) {	
			if (timer < 2)
				timer += Time.deltaTime;
			else if (timer >= 2) {
				if (hit.collider.gameObject.name == "Pause") {
					pause.SetActive (false);
					play.SetActive (true);
					Debug.Log ("Play activated");
					videoPlayer.Pause ();
					isPause = true;
				}
				if (hit.collider.gameObject.name == "Play") {
					play.SetActive (false);
					pause.SetActive (true);
					Debug.Log ("Pause Actived");
					videoPlayer.Play ();
					isPause = false;
				}
				if (hit.collider.gameObject.name == "Reset") {
					videoPlayer.Stop ();
					videoClipındex = 0;
					videoPlayer.clip = videoClips[videoClipındex];
					videoPlayer.Play ();
					isPause = false;
					Vid2Check ();
				}
				if (hit.collider.gameObject.name == "Next") {
					videoPlayer.Stop ();
					videoClipındex++;
					if (videoClipındex == 3)
						videoClipındex = 0;
					videoPlayer.clip = videoClips [videoClipındex];
					videoPlayer.Play ();
					isPause = false;
					Vid2Check ();
				}
				if (hit.collider.gameObject.name == "Pop") {
					popupVideo.SetActive (true);
				}
				timer = 0;
			}
		} else {
			if(timer != 0)
				timer = 0;
		}
	}
		
}
