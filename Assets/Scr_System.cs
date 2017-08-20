using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_System : MonoBehaviour {

    public Scr_Player PrefabPlayer1, PrefabPlayer2;
    public Scr_Player ActivePlayer;
    public Scr_GenericCollection DiscardPile;

	void awake () {
		Application.targetFrameRate = 120;
		QualitySettings.vSyncCount = 0;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
