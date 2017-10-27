using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedCollectionLayout : ICollectionLayout
{
    // Use this for initialization
    void ICollectionLayout.Start()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void ICollectionLayout.Update(CollectionContainer collectionContainer)
    {
        float spacing = collectionContainer.Spacing;
        float offsetX = collectionContainer.OffsetX;
        float offsetY = collectionContainer.OffsetY;
        float offsetZ = collectionContainer.OffsetZ;
        int size = collectionContainer.Size;
        int columnCount = collectionContainer.ColumnCount;
        int rowCount = collectionContainer.RowCount;
        bool centred = collectionContainer.Centred;
        bool flipped = collectionContainer.Flipped;
        bool reposition = collectionContainer.Reposition;
        Transform transform = collectionContainer.Transform;
        List<Transform> children = collectionContainer.Children;

        Vector3 currentPos, newPos = transform.position;

        for (int i = 0; i < children.Count; i++)
        {
            currentPos = children[i].position;

            if (centred)
            {
                newPos.y = transform.position.y + offsetY + (spacing * (i - ((size - 1) / 2f)));
            }
            else
            {
                newPos.y = transform.position.y + offsetY + (spacing * i);
            }

            if (flipped)
            {
                newPos.y *= -1;
            }

            children[i].position = Vector3.Lerp(currentPos, newPos, (Time.deltaTime * 8f));
        }
    }
}
