using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum CollectionLayout
{
    HorizontalOnly,
    VerticalOnly,
    Stacked,
    Grid
}

public class Collection : NetworkBehaviour
{

    public List<Transform> children;
    public CollectionLayout layout;

    public static ICollectionLayout stackedCollection = new StackedCollectionLayout();
    public ICollectionLayout currentLayout;
    public CollectionContainer collectionContainer = new CollectionContainer();

    private int size;

    // Use this for initialization
    void Start()
    {
        collectionContainer.Transform = this.transform;
        collectionContainer.Children = this.children;
    }

    // Update is called once per frame
    void Update()
    {
        if (layout == CollectionLayout.Stacked)
        {
            stackedCollection.Update(collectionContainer);
        }
    }


    public int Remove(Transform child)
    {
        int index = children.IndexOf(child);
        if (children.Remove(child))
        {
            size--;
        }
        return index;
    }

    public void RemoveAll(List<Transform> children)
    {
        Transform[] remove = children.ToArray();
        int count = children.Count;

        for (int i = 0; i < count; i++)
        {
            Remove(remove[i]);
        }
    }

    public Transform Pop()
    {
        if (children.Count == 0)
        {
            return null;
        }

        Transform child = children[children.Count - 1];
        Remove(child);
        return child;
    }

    public Transform getRandom()
    {
        int count = children.Count;

        if (count == 0)
        {
            return null;
        }

        Transform child = children[Random.Range(0, count)];
        Remove(child);
        return child;
    }

    public virtual bool Add(Transform child)
    {
        if (child == null)
        {
            return false;
        }

        children.Add(child);
        child.parent = transform;
        size++;

        return true;
    }

    public virtual bool AddAll(List<Transform> children)
    {
        foreach (Transform child in children)
        {
            if (Add(child) == false)
            {
                return false;
            }
        }

        return true;
    }

    public void Insert(int index, Transform child)
    {
        children.Insert(index, child);
        size++;
    }
}