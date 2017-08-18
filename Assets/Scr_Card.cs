﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Card : MonoBehaviour
{
    public int number;
    bool _played;
    Transform _other;
	int _lastIndex;
    TextMesh[] _numberText;

    // Use this for initialization
    void Start()
    {
        _numberText = GetComponentsInChildren<TextMesh>();
        set_number_meshes();
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
        _lastIndex = transform.parent.GetComponent<Scr_GenericCollection>().Remove(transform);
    }

    private void OnMouseUp()
    {
        if (_other != null)
        {
            transform.parent.GetComponent<Scr_GenericCollection>().Remove(transform);
            _played = true;
            _other.GetComponent<Scr_TileSide>().Add(transform);
        }
        else
        {
            transform.parent.GetComponent<Scr_GenericCollection>().Insert(_lastIndex, transform);
        }
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
        Vector3 newPos = transform.position;
        newPos.y = height;
        transform.position = Vector3.Lerp(transform.position, newPos, 8f * Time.deltaTime);
    }

    private void set_number_meshes()
    {
        foreach (TextMesh textMesh in _numberText)
        {
            textMesh.text = number.ToString();
        }
    }
}
