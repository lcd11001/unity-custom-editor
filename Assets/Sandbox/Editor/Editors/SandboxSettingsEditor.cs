using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sandbox.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Doozy.Runtime.UIElements.Extensions;


#if USE_DOZZY
using Doozy.Editor.EditorUI.Components;
#endif

namespace Sandbox.Editor.Editors
{
    [CustomEditor(typeof(SandboxSettings), true)]
    public class SandboxSettingsEditor : UnityEditor.Editor
    {
        private VisualElement root { get; set; }
        public override VisualElement CreateInspectorGUI()
        {
            FindSerializedProperties();

#if USE_DOZZY
            InitializeEditorWithDozzy();
            ComposeWithDozzy();
#else
            InitializeEditor();
            Compose();
#endif

            return root;
        }

        #region Serialized Properties
        private SerializedProperty propertyTestString { get; set; }
        private SerializedProperty propertyTestInt { get; set; }
        private SerializedProperty propertyTestFloat { get; set; }


        private void FindSerializedProperties()
        {
            propertyTestString = serializedObject.FindProperty(nameof(SandboxSettings.TestString));
            propertyTestInt = serializedObject.FindProperty(nameof(SandboxSettings.TestInt));
            propertyTestFloat = serializedObject.FindProperty(nameof(SandboxSettings.TestFloat));
        }
        #endregion

        #region Initialize Editor

        private PropertyField fieldTestString { get; set; }
        private PropertyField fieldTestInt { get; set; }
        private PropertyField fieldTestFloat { get; set; }

        private void InitializeEditor()
        {
            root = new VisualElement();

            fieldTestString = new PropertyField(propertyTestString);
            fieldTestInt = new PropertyField(propertyTestInt);
            fieldTestFloat = new PropertyField(propertyTestFloat);
        }
        #endregion

        #region Compose

        private String editorTitle
        {
            get
            {
                var title = nameof(SandboxSettings);
                // https://regexr.com/
                // split words
                title = Regex.Replace(title, "([a-z])([A-Z])", "$1 $2");
                return title;
            }
        }

        private void Compose()
        {
            root.Add(new Label(editorTitle));
            root.Add(fieldTestString);
            root.Add(fieldTestInt);
            root.Add(fieldTestFloat);
        }
        #endregion

        #region Dozzy
#if USE_DOZZY
        // require Doozy package
        private FluidComponentHeader fluidHeader { get; set; }

        private void InitializeEditorWithDozzy()
        {
            root = new VisualElement();

            fluidHeader = FluidComponentHeader.Get()
                .SetComponentNameText(editorTitle);
        }

        private void ComposeWithDozzy()
        {
            root
                .AddChild(fluidHeader);
        }
#endif
        #endregion

    }
}
