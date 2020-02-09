using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class GridScalar : MonoBehaviour
    {
        private GridLayoutGroup grid;
        private RectOffset gridPadding;
        private RectTransform parent;
        public float spacing = 10;

        public void CalculateSize(int rows, int cols)
        {
            grid = GetComponent<GridLayoutGroup>();
            grid.spacing = new Vector2(spacing, spacing);
            parent = GetComponent<RectTransform>();
            gridPadding = grid.padding;

            var height = parent.rect.height - gridPadding.right;
            var width = parent.rect.width - gridPadding.right;
            var elementsCount = parent.childCount;

            var rowCount = elementsCount / rows;
            var colCount = elementsCount / cols;

            var newHeight = width / colCount;
            var newWidth = height / rowCount;

            float newSize = 0;
            newSize = newHeight > newWidth ? newWidth : newHeight;
            grid.cellSize = new Vector2(newSize, newSize);
        }
    }
}