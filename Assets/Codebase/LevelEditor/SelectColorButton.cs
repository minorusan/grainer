using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class SelectColorButton : MonoBehaviour
    {
        public static event Action<Color> SelectColorBtnClicked;
        
        private Color _color;
        [SerializeField] private Image image;
        private Button myBtn;
        [SerializeField] private Text text;

        public void Init(string name, Color color)
        {
            text.text = name;
            text.color = InvertColor(color);
            image.color = color;
            _color = color;
            gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SelectColorBtnClicked?.Invoke(_color);
        }

        private Color InvertColor(Color color)
        {
            return new Color((float) (1.0 - color.r), (float) (1.0 - color.g), (float) (1.0 - color.b));
        }
    }
}