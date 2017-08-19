using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GenericCollection : MonoBehaviour
{
    private float _spacing = 2.5f, _offsetX, _offsetY;
    private Transform _transform;
    private int _size;
    private bool _centered;
    private bool _flipped;
    private bool _reposition;
    private CollectionLayout _layout;

    private List<Transform> _children;

    public enum CollectionLayout
    {
        HorizontalOnly,
        VerticalOnly,
        Stacked,
        Grid
    }

    protected void setReposition (bool reposition)
    {
        _reposition = reposition;
    }

    protected void setFlipped (bool flipped)
    {
        _flipped = flipped;
    }

    protected void setCentered (bool centered)
    {
        _centered = centered;
    }

    protected void setOffsetX (float offset)
    {
        _offsetX = offset;
    }

    protected void setOffsetY (float offset)
    {
        _offsetY = offset;
    }

    protected void setTransform (Transform transform)
    {
        _transform = transform;
    }

    protected void setLayout (CollectionLayout layout)
    {
        _layout = layout;
    }

    protected void setSpacing (float spacing)
    {
        _spacing = spacing;
    }

    // Use this for initialization
    protected virtual void Start()
    {
        _children = new List<Transform>();
        _transform = transform;
        _reposition = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_reposition)
        {
            if (_layout == CollectionLayout.HorizontalOnly)
            {
                PositionChildrenHorizontally();
            }
            else if (_layout == CollectionLayout.Stacked)
            {
                PositionChildrenStacked();
            }
        }
    }

    void PositionChildrenHorizontally()
    {
        Vector3 currentPos, newPos = _transform.position;

        for (int i = 0; i < _children.Count; i++)
        {
            currentPos = _children[i].position;
            newPos.x = _offsetX + (_spacing * i);
            if (_flipped)
            {
                newPos.x *= -1;
            }
            if (_centered)
            {
                //newPos.x -= (_size - 1);
                newPos.x = _offsetX + (_spacing * (i - ((_size - 1) / 2f)));
            }

            newPos.y = _offsetY + this.transform.position.y;
            _children[i].position = Vector3.Lerp(currentPos, newPos, (Time.deltaTime * 8f));
        }
    }

    void PositionChildrenStacked()
    {
        Vector3 currentPos, newPos = _transform.position;

        for (int i = 0; i < _children.Count; i++)
        {
            currentPos = _children[i].position;

            if (_centered)
            {
                newPos.y = _offsetY + (_spacing * (i - ((_size - 1) / 2f)));
            }
            else
            {
                newPos.y = _offsetY + (_spacing * i);
            }

            if (_flipped)
            {
                newPos.y *= -1;
            }

            _children[i].position = Vector3.Lerp(currentPos, newPos, (Time.deltaTime * 8f));
        }
    }

    public int Remove(Transform child)
    {
		int index = _children.IndexOf(child);
        if (_children.Remove(child))
        {
            _size--;
        }
		return index;
    }

    public Transform Pop ()
    {
        Transform child = _children[_children.Count - 1];
        Remove(child);
        return child;
    }

    public void Add(Transform child)
    {
        _children.Add(child);
        child.parent = transform;
        _size++;
    }

	public void Insert(int index, Transform child) {
		_children.Insert (index, child);
		_size++;
	}
}
