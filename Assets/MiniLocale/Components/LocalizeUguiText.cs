using UnityEngine;
using UnityEngine.UI;

namespace MiniLocale.Components
{
    public class LocalizeUguiText : LocalizeComponent
    {
        [SerializeField]
        private Text _text;

        public override void SetText(string text)
        {
            _text.text = text;
        }

        private void Reset()
        {
            if (_text == null) _text = GetComponent<Text>();
        }
    }
}