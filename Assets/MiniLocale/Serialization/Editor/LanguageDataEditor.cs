using System;
using UnityEditor;
using UnityEngine;

namespace MiniLocale.Serialization.Editor
{
	[CustomEditor(typeof(LanguageData))]
	public class LanguageDataEditor : UnityEditor.Editor
	{
		private const int PER_PAGE = 10;
		private int page = 0;

		private SerializedProperty strings;

		private void OnEnable()
		{
			strings = serializedObject.FindProperty("strings");
		}

		private void DrawCsvIo()
		{
			// Get data target as the correct object
			var data = target as LanguageData;
			if (data == null) return;
			
			// CSV import/export buttons
			GUILayout.Label("CSV Spreadsheet Management", EditorStyles.boldLabel);
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("Import"))
			{
				data.LoadFromCsv();
			}
			if (GUILayout.Button("Export"))
			{
				data.SaveToCsv();
			}
			EditorGUILayout.EndHorizontal();
		}

		private void DrawDuplicates()
		{
			// Get data target as the correct object
			var data = target as LanguageData;
			if (data == null) return;

			// Extra options
			GUILayout.Label("Extra Functions", EditorStyles.boldLabel);
			if (GUILayout.Button("Remove Duplicate Tags"))
			{
				data.StripDuplicates();
			}
		}

		private void DrawPagination()
		{
			// Page buttons
			int prevPage = page;
			int highest = (strings.arraySize) / PER_PAGE;
			EditorGUILayout.BeginHorizontal();
			GUI.enabled = page > 0;
			if (GUILayout.Button("<")) page--;
			GUI.enabled = true;
			page = EditorGUILayout.IntSlider(page + 1, 1, highest + 1) - 1;
			GUILayout.Label("OF " + (highest + 1));
			GUI.enabled = page < highest;
			if (GUILayout.Button(">")) page++;
			GUI.enabled = true;
			EditorGUILayout.EndHorizontal();
			if (page != prevPage)
			{
				GUI.FocusControl(null);
			}
		}

		private void DrawStrings()
		{
			// Draw the strings in the language
			GUILayout.Label("Language Strings", EditorStyles.boldLabel);

			DrawPagination();
			
			// Strings box
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			int start = page * PER_PAGE;
			int end = Mathf.Min((page + 1) * PER_PAGE, strings.arraySize + 1);
			for (int i = start; i < end; i++)
			{
				bool remove = false;
				if (i == strings.arraySize)
				{
					if (GUILayout.Button("Add New String"))
					{
						strings.InsertArrayElementAtIndex(strings.arraySize);
					}
					break;
				}
				EditorGUILayout.BeginVertical(EditorStyles.helpBox);
				var s = strings.GetArrayElementAtIndex(i);
				var tag = s.FindPropertyRelative("tag");
				var text = s.FindPropertyRelative("text");
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label($"Index {i}", EditorStyles.centeredGreyMiniLabel);
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("Delete")) remove = true;
				EditorGUILayout.EndHorizontal();
				tag.stringValue = EditorGUILayout.TextArea(tag.stringValue);
				text.stringValue = EditorGUILayout.TextArea(text.stringValue);
				EditorGUILayout.EndVertical();
				if (!remove) continue;
				strings.DeleteArrayElementAtIndex(i);
				i--;
				end--;
			}
			EditorGUILayout.EndVertical();
			DrawPagination();
		}

		public override void OnInspectorGUI()
		{
			// Get and update the base serialized object
			serializedObject.Update();
			
			// Draw editor widgets
			DrawCsvIo();
			DrawDuplicates();
			DrawStrings();

			// Make sure the serialized object is updated if it was changed
			serializedObject.ApplyModifiedProperties();
		}
	}
}