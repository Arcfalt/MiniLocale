using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MiniLocale.Serialization;
using UnityEditor;
using UnityEngine;

namespace MiniLocale.Components.Editor
{
	[CustomEditor(typeof(LocalizeComponent<>), true)]
	public class LocalizeComponentEditor : UnityEditor.Editor
	{
		private Dictionary<string, LanguageData> banks;
		private Dictionary<string, string> texts;
		private SerializedProperty textTag;
		private string lastTag = "";
		
		private static bool showTags = true;
		
		private void OnEnable()
		{
			textTag = serializedObject.FindProperty("textTag");
			var guids = AssetDatabase.FindAssets($"t:{nameof(LanguageData)}");
			banks = new Dictionary<string, LanguageData>(guids.Length);
			texts = new Dictionary<string, string>(guids.Length);
			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				var data = AssetDatabase.LoadAssetAtPath<LanguageData>(path);
				if (data == null) continue;
				banks[data.tag] = data;
			}
			if (showTags) RefreshTexts(true);
		}

		private void RefreshTexts(bool force = false)
		{
			var tag = textTag.stringValue;
			if (!force && tag.Equals(lastTag)) return;
			lastTag = tag;
			texts.Clear();
			foreach (var bank in banks)
			{
				string found = null;
				foreach (var s in bank.Value.strings)
				{
					if (s.tag != tag) continue;
					found = s.text;
					break;
				}
				texts[bank.Key] = found;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			bool wasShow = showTags;
			showTags = EditorGUILayout.BeginFoldoutHeaderGroup(showTags, "Translated Texts");
			if (!showTags)
			{
				EditorGUILayout.EndFoldoutHeaderGroup();
				return;
			}
			RefreshTexts(!wasShow && showTags);
			bool endRefresh = false;
			foreach (var t in texts)
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label(t.Key, EditorStyles.miniBoldLabel, GUILayout.ExpandWidth(false));
				if (t.Value == null)
				{
					if (GUILayout.Button("Add Blank"))
					{
						AddToBank(banks[t.Key], textTag.stringValue, "");
						endRefresh = true;
					}
					if (GUILayout.Button("Add Current"))
					{
						Type type = target.GetType();
						var method = type.GetMethod("GetDisplayedText");
						if (method != null)
						{
							var o = method.Invoke(target, null);
							var op = o as string;
							AddToBank(banks[t.Key], textTag.stringValue, op ?? "");
						}
						else AddToBank(banks[t.Key], textTag.stringValue, "");
						endRefresh = true;
					}
				}
				else
				{
					EditorGUILayout.BeginVertical(EditorStyles.helpBox);
					GUILayout.Label(t.Value, GUILayout.ExpandWidth(true));
					EditorGUILayout.EndVertical();
				}
				EditorGUILayout.EndHorizontal();
			}
			if (endRefresh) RefreshTexts(true);
			EditorGUILayout.EndFoldoutHeaderGroup();
		}

		private void AddToBank(LanguageData data, string tag, string text)
		{
			var so = new SerializedObject(data);
			so.Update();
			var prop = so.FindProperty("strings");
			int index = prop.arraySize;
			prop.arraySize++;
			var i = prop.GetArrayElementAtIndex(index);
			var iTag = i.FindPropertyRelative("tag");
			var iText = i.FindPropertyRelative("text");
			iTag.stringValue = tag;
			iText.stringValue = text;
			so.ApplyModifiedProperties();
		}
	}
}