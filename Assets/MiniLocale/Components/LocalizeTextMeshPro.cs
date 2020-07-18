using TMPro;

namespace MiniLocale.Components
{
	public class LocalizeTextMeshPro : LocalizeComponent<TMP_Text>
	{
		protected override void SetText(string text)
		{
			target.text = text;
		}
	}
}