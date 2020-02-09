using UnityEngine;

namespace LevelEditor
{
    public class ColorPicker : MonoBehaviour
    {
        public static ColorPicker Instance;
        
        [SerializeField] private GameObject buttonTemplate;
        public Color currentColor;
        [SerializeField] private Transform parent;

        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            var colorMap = Resources.Load<ColorMap>("Settings/Colors/Color Map");
            currentColor = colorMap.Colors[0].Color;
            foreach (var color in colorMap.Colors)
            {
                var selectColorButton = Instantiate(buttonTemplate, parent).GetComponent<SelectColorButton>();
                selectColorButton.Init(color.name, color.Color);
            }
        }

        private void OnEnable()
        {
            SelectColorButton.SelectColorBtnClicked += SetCurrentColor;
        }

        private void OnDisable()
        {
            SelectColorButton.SelectColorBtnClicked -= SetCurrentColor;
        }

        private void SetCurrentColor(Color obj)
        {
            currentColor = obj;
        }
    }
}