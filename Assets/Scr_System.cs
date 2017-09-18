using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_System : MonoBehaviour {

    public Scr_Player PrefabPlayer1, PrefabPlayer2;
    public Scr_Player ActivePlayer;
    public Scr_GenericCollection DiscardPile;

	void awake () {
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;
	}

	// Use this for initialization
	void Start () {
        PrefabPlayer1.EndTurnListener += OnEndTurn;
        PrefabPlayer2.EndTurnListener += OnEndTurn;

        if (Random.Range(0, 100) > 50)
        {
            ActivePlayer = PrefabPlayer1;
        }
        else
        {
            ActivePlayer = PrefabPlayer2;
        }

        ActivePlayer.StartTurn();
	}

    private void OnEndTurn ()
    {
        if (ActivePlayer == PrefabPlayer1)
        {
            ActivePlayer = PrefabPlayer2;
        }
        else
        {
            ActivePlayer = PrefabPlayer1;
        }

        ActivePlayer.StartTurn();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
