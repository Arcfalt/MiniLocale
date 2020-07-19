using UnityEngine.UI;

namespace MiniLocale.Components
{
    public class LocalizeUguiText : LocalizeComponent<Text>
    {
	    protected override void SetText(string text)
        {
            target.text = text;
        }

	    public override string GetDisplayedText()
	    {
		    return target.text;
	    }
    }
}