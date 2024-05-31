using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sandbox.Editor.Editors;
using Sandbox.Runtime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sandbox.Editor.Windows
{
    public class SandboxSettingsWindow : UnityEditor.EditorWindow
    {
        private VisualElement root => rootVisualElement;
        private static String windowTitle
        {
            get
            {
                var title = nameof(SandboxSettingsWindow);
                // https://regexr.com/
                // split words
                title = Regex.Replace(title, "([a-z])([A-Z])", "$1 $2");
                return title;
            }
        }

        [MenuItem("Sandbox/Settings")]
        public static void Open()
        {
            var window = GetWindow<SandboxSettingsWindow>();

            window.titleContent = new GUIContent(windowTitle);
            window.Show();
        }

        private void CreateGUI()
        {
            root.Clear();

            var editor = (SandboxSettingsEditor)UnityEditor.Editor.CreateEditor(SandboxSettings.Instance);
            var editorRoot = editor.CreateInspectorGUI();
            editorRoot.Bind(editor.serializedObject);

            root.Add(editorRoot);
        }
    }
}
