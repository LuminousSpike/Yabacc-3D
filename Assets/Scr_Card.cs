using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Scr_Card : MonoBehaviour
{
    public int number;
    public Suite Suite;
    public event Action<Scr_Card> OnPlayed;

    bool _played;
    Transform _other;
	int _lastIndex;
    TextMesh[] _numberText;
    Scr_MouseDraggable _draggable;
    bool _active;

    public void setActive (bool active)
    {
        _active = active;
    }

    // Use this for initialization
    void Start()
    {
        _numberText = GetComponentsInChildren<TextMesh>();
        set_number_meshes();
        _draggable = GetComponent<Scr_MouseDraggable>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        rise_card(1f);
    }

    private void OnMouseDown()
    {
        _draggable.Enabled = _active;
        if (_active)
        {
            Scr_Hand hand = transform.parent.GetComponent<Scr_Hand>();
            if (hand != null)
            {
                _lastIndex = transform.parent.GetComponent<Scr_GenericCollection>().Remove(transform);
                _draggable.Enabled = hand.Active;
            }
        }
    }

    private void OnMouseUp()
    {
        Scr_Hand hand = transform.parent.GetComponent<Scr_Hand>();

        if (_other != null)
        {
            if (_other.GetComponent<Scr_TileSide>().Add(transform))
            {
                hand.PickupCard();
                _played = true;
                if (OnPlayed != null)
                {
                    OnPlayed(this);
                }
                return;
            }
        }

        if (_active)
            hand.Insert(_lastIndex, transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TileSide" && _played == false)
        {
            _other = other.transform;
            rise_card(0.4f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TileSide")
        {
            _other = null; 
        }
    }

    private void rise_card (float height)
    {
        if (_active)
        {
            Vector3 newPos = transform.position;
            newPos.y = height;
            transform.position = Vector3.Lerp(transform.position, newPos, 8f * Time.deltaTime);
        }
    }

    private void set_number_meshes()
    {
        foreach (TextMesh textMesh in _numberText)
        {
            textMesh.text = number.ToString();
        }
    }
}
