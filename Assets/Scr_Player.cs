using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Scr_Player : MonoBehaviour {

    public Scr_Hand Hand;
    public Scr_TokenCollection TokenCollection;
    public Material MaterialDefault, MaterialActive;

    public event Action EndTurnListener;

	// Use this for initialization
	void Start () {
        Hand.OnTurnEnd += EndTurn;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartTurn()
    {
        transform.Find("Pre_Hand").GetComponentInChildren<Renderer>().material = MaterialActive;
        Hand.setActive(true);
    }

    public void EndTurn()
    {
        transform.Find("Pre_Hand").GetComponentInChildren<Renderer>().material = MaterialDefault;

        if (EndTurnListener != null)
        {
            EndTurnListener();
        }
    }
}
