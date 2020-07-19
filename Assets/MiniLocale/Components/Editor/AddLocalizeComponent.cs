using TMPro;
using UnityEditor;
using UnityEngine.UI;

namespace MiniLocale.Components.Editor
{
	public static class AddLocalizeComponent
	{
		[MenuItem("CONTEXT/Text/Add Localizer")]
		private static void AddUguiLocalizer(MenuCommand mc)
		{
			var text = mc.context as Text;
			if (text == null) return;
			var go = text.gameObject;
			go.AddComponent<LocalizeUguiText>();
		}
		
		[MenuItem("CONTEXT/TMP_Text/Add Localizer")]
		private static void AddTextMeshLocalizer(MenuCommand mc)
		{
			var text = mc.context as TMP_Text;
			if (text == null) return;
			var go = text.gameObject;
			go.AddComponent<LocalizeTextMeshPro>();
		}
	}
}