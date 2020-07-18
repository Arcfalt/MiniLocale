using UnityEngine.UI;

namespace MiniLocale.Components
{
    public class LocalizeUguiText : LocalizeComponent<Text>
    {
        public override void SetText(string text)
        {
            target.text = text;
        }
    }
}