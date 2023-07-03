using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : Singleton<LevelGrid>
{
    public float SquareSize => squareSize;
    [SerializeField] protected float squareSize;

    public int LevelWidth => levelWidth;
    [SerializeField] protected int levelWidth;

    public int LevelHeight => levelHeight;
    [SerializeField] protected int levelHeight;

    public float GridWidth => (squareSize * levelWidth);
    public float GridHeight => (squareSize * levelHeight);

    private List<List<Vector3>> gridPositions = new List<List<Vector3>>();

    private void Awake()
    {
        float gridHalfWidth = GridWidth / 2f;
        float gridHalfHeight = GridHeight / 2f;
        float halfSquareSize = squareSize / 2f;

        for (int i = 0; i < levelHeight; i++)
        {
            List<Vector3> gridRow = new List<Vector3>();
            for(int j = 0; j < levelWidth; j++)
            {
                Vector3 newGridPos = new Vector3(
                    (j * squareSize) - gridHalfWidth + halfSquareSize,
                    0,
                    (i * squareSize) - gridHalfHeight + halfSquareSize
                    );
                gridRow.Add(newGridPos);
            }
            gridPositions.Add(gridRow);
        }
    }

    public Vector3 GetGridPos(int row, int column)
    {
        return gridPositions[row][column];
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        float gridHalfWidth = GridWidth / 2f;
        float gridHalfHeight = GridHeight / 2f;
        float halfSquareSize = squareSize / 2f;
        float radius = squareSize / 10f;

        //Draw the grid positions
        if (gridPositions.Count > 0) 
        { 
            for (int i = 0; i < gridPositions.Count; i++)
            {
                for (int j = 0; j < gridPositions[0].Count; j++)
                {
                    Gizmos.DrawSphere(gridPositions[j][i], radius);
                }
            }
        };

        for (int i = 0; i <= levelWidth; i++)
        {
            float xPos = gridHalfWidth - (i * squareSize);
            Vector3 top = new Vector3(xPos, 0, gridHalfHeight);
            Vector3 bottom = new Vector3(xPos, 0, -gridHalfHeight);
            Gizmos.DrawLine(top, bottom);
        }

        for (int i = 0; i <= levelHeight; i++)
        {
            float zPos = gridHalfHeight - (i * squareSize);
            Vector3 right = new Vector3(gridHalfWidth, 0, zPos);
            Vector3 left = new Vector3(-gridHalfWidth, 0, zPos);
            Gizmos.DrawLine(right, left);
        }
    }
}
