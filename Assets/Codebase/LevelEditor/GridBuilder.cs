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
            for (int j = 0; j < levelMap.height; j++)
            {
                for (int i = 0; i < levelMap.width; i++)
                {
                        var pixelColor = levelMap.GetPixel(/*Mathf.Abs(levelMap.width-1-*/i/*)*/, Mathf.Abs(levelMap.height-1-j));
                        var cell = Instantiate(cellTemplate, _gridLayoutGroup.transform).GetComponent<Cell>();
                        cell.Init(Mathf.Abs(levelMap.height-1-j), i, pixelColor);
                }
            }

            gridScalar.CalculateSize(levelMap.height, levelMap.width);
        }
    }
}