using System;
using System.Collections;
using System.Collections.Generic;
using Sandbox.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Sandbox.Editor.Editors
{
    [CustomEditor(typeof(SandboxSettings), true)]
    public class SandboxSettingsEditor : UnityEditor.Editor
    {
        private VisualElement root { get; set; }
        public override VisualElement CreateInspectorGUI()
        {
            FindSerializedProperties();
            InitializeEditor();
            Compose();

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
        private void Compose()
        {
            root.Add(new Label("Sandbox Settings"));
            root.Add(fieldTestString);
            root.Add(fieldTestInt);
            root.Add(fieldTestFloat);
        }
        #endregion

    }
}
