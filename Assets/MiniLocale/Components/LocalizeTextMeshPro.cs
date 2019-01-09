using TMPro;
using UnityEngine;

namespace MiniLocale.Components
{
	public class LocalizeTextMeshPro : LocalizeComponent
	{
		[SerializeField]
		private TMP_Text _textMesh;

		public override void SetText(string text)
		{
			_textMesh.text = text;
		}

		private void Reset()
		{
			if (_textMesh == null) _textMesh = GetComponent<TMP_Text>();
		}
	}
}