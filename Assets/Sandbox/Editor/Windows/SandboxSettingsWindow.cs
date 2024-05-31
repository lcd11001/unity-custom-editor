using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sandbox.Editor.Editors;
using Sandbox.Runtime;
using Sandbox.Runtime.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


#if USE_DOZZY
using Doozy.Editor.EditorUI.Windows.Internal;
using Doozy.Editor.UIElements;
using Doozy.Editor.EditorUI.Utils;
#endif

namespace Sandbox.Editor.Windows
{
#if USE_DOZZY
    public class SandboxSettingsWindow : FluidWindow<SandboxSettingsWindow>
    {
        [MenuItem("Sandbox/Settings")]
        public static void Open() => InternalOpenWindow(SandboxUtils.GetWords(nameof(SandboxSettingsWindow)));

        protected override void CreateGUI()
        {
            root.RecycleAndClear();

            var editor = (SandboxSettingsEditor)UnityEditor.Editor.CreateEditor(SandboxSettings.Instance);
            var editorRoot = editor.CreateInspectorGUI();
            editorRoot.Bind(editor.serializedObject);

            editor
                .HideHeader()
                .SetRootPadding(DesignUtils.k_Spacing2X);

            root.Add(editorRoot);
        }
    }
#else
    public class SandboxSettingsWindow : UnityEditor.EditorWindow
    {
        private VisualElement root => rootVisualElement;

        [MenuItem("Sandbox/Settings")]
        public static void Open()
        {
            var window = GetWindow<SandboxSettingsWindow>();

            window.titleContent = new GUIContent(SandboxUtils.GetWords(nameof(SandboxSettingsWindow)));
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
#endif
}
