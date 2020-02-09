using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class GridBuilder : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private GameObject cellTemplate;
        [SerializeField] private GridScalar gridScalar;
        [SerializeField] private Texture2D levelMap;

        public void BuildLevel(Texture2D levelMap)
        {
            _gridLayoutGroup.constraintCount = levelMap.width;

            for (var j = 0; j < levelMap.height; j++)
            {
                for (var i = 0; i < levelMap.width; i++)
                {
                    var pixelColor = levelMap.GetPixel(i, j);
                    var cell = Instantiate(cellTemplate, _gridLayoutGroup.transform).GetComponent<Cell>();
                    cell.Init(i, j, pixelColor);
                }
            }

            gridScalar.CalculateSize(levelMap.width, levelMap.height);
        }
    }
}