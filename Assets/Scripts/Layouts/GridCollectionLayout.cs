using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCollectionLayout : ICollectionLayout{


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

        float maxColumnCount = System.Math.Min(children.Count, columnCount);
        float maxRowCount = System.Math.Min(children.Count / columnCount, rowCount) + 1;
        float posX, posY, posZ;
        float posRow, posColumn;
        int currentRow, currentTier;
        
        for (int i = 0; i < children.Count; i++)
        {
            currentPos = children[i].position;

            currentRow = i / columnCount;
            currentTier = currentRow / rowCount;

            posColumn = i % maxColumnCount;
            posRow = currentRow % maxRowCount;

            if (centred)
            {
                posX = spacing * (posColumn - ((maxColumnCount / 2f) - spacing));
                posZ = spacing * (posRow - ((maxRowCount / 2f) - spacing));
            }
            else
            {
                posX = spacing * (posColumn - ((columnCount / 2f) - spacing));
                posZ = spacing * (posRow - ((rowCount / 2f) - spacing));
            }

            posY = spacing * currentTier;

            newPos.x = transform.position.x + offsetX + posX;
            newPos.y = transform.position.y + offsetY + posY;
            newPos.z = transform.position.z + offsetZ + posZ;

            children[i].position = Vector3.Lerp(currentPos, newPos, (Time.deltaTime * 8f));
        }
    }
}
