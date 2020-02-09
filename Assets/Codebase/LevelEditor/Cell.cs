using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelEditor
{
    public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        public Color cellColor;
        public int cellPosX;
        public int cellPosY;
        [SerializeField] private Image image;

        public void OnPointerDown(PointerEventData eventData)
        {
            image.color = ColorPicker.Instance.currentColor;
            cellColor = ColorPicker.Instance.currentColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
            {
                image.color = ColorPicker.Instance.currentColor;
                cellColor = ColorPicker.Instance.currentColor;
            }
        }

        public void Init(int posX, int posY, Color myColor)
        {
            cellPosX = posX;
            cellPosY = posY;
            cellColor = myColor;
            image.color = cellColor;
        }
    }
}