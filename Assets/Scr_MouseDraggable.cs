using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_MouseDraggable : MonoBehaviour {

    public bool Enabled = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDrag()
    {
        if (Enabled)
        {
            float distanceToScreen = Camera.main.WorldToScreenPoint(transform.position).z;

            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
        }
    }
}
