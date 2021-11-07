using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CardGame.Data;

namespace CardGame.Objects
{
    [RequireComponent(typeof(Collider2D))]
    public class GameGrid : MonoBehaviour 
    {
        private GridData gridData;

        private Bounds boundary;

        private float cellWidth = 3f;
        private float cellHeight = 3f;

        // Start is called before the first frame update
        void Awake()
        {
            boundary = GetComponent<Collider2D>().bounds;
        }

        public void Fill(List<Card> cards)
        {
            if (cards.Count == 0) return;

            var gridSize = new Vector2()
            {
                x = gridData.Columns * cellWidth,
                y = gridData.Rows * cellHeight,
            };
            var cellScale = Mathf.Min(1f, boundary.size.x / gridSize.x, boundary.size.y / gridSize.y);

            var offset = (gridSize - new Vector2(cellWidth, cellHeight)) / 2;

            for (int i = 0; i < gridData.Rows; i++)
            {
                for (int j = 0; j < gridData.Columns; j++)
                {
                    var card = cards[i * gridData.Columns + j].transform;
                    card.position = cellScale * new Vector3()
                    {
                        x = j * cellWidth - offset.x,
                        y = i * cellHeight - offset.y,
                        z = card.position.z
                    };
                    card.localScale *= cellScale;
                    card.SetParent(transform);
                }
            }
        }

        public void SetGridData(GridData gridData)
        {
            this.gridData = gridData;
        }
        public void SetCellSize(float width, float height)
        {
            cellWidth = width;
            cellHeight = height;
        }
    }
}
