using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sandbox.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Sandbox.Runtime.Utils;

#if USE_DOZZY
using Doozy.Runtime.UIElements.Extensions;
using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Doozy.Editor.EditorUI.Utils;
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

        private void Compose()
        {
            root.Add(new Label(SandboxUtils.GetWords(nameof(SandboxSettings))));
            root.Add(fieldTestString);
            root.Add(fieldTestInt);
            root.Add(fieldTestFloat);
        }
        #endregion

        #region Dozzy
#if USE_DOZZY
        // require Doozy package
        private FluidComponentHeader fluidHeader { get; set; }
        private FluidField fluidTestString { get; set; }
        private FluidField fluidTestInt { get; set; }
        private FluidField fluidTestFloat { get; set; }

        private void InitializeEditorWithDozzy()
        {
            root = new VisualElement();

            fluidHeader = FluidComponentHeader.Get()
                .SetComponentNameText(SandboxUtils.GetWords(nameof(SandboxSettings)))
                .SetIcon(EditorSpriteSheets.EditorUI.Icons.Settings)
                .SetAccentColor(EditorColors.EditorUI.DeepPurple);

            fluidTestString = FluidField.Get()
                .SetLabelText(SandboxUtils.GetWords(nameof(SandboxSettings.TestString)))
                .AddFieldContent(DesignUtils.NewTextField(propertyTestString)
                    .SetStyleFlexGrow(1)
                    .SetTooltip($"{propertyTestString.propertyPath} ({propertyTestString.propertyType})")
                );

            fluidTestInt = FluidField.Get()
                .SetLabelText(SandboxUtils.GetWords(nameof(SandboxSettings.TestInt)))
                .AddFieldContent(DesignUtils.NewIntegerField(propertyTestInt)
                    .SetStyleFlexGrow(1)
                    .SetTooltip($"{propertyTestInt.propertyPath} ({propertyTestInt.propertyType})")
                );

            fluidTestFloat = FluidField.Get()
                .SetLabelText(SandboxUtils.GetWords(nameof(SandboxSettings.TestFloat)))
                .AddFieldContent(DesignUtils.NewFloatField(propertyTestFloat)
                    .SetStyleFlexGrow(1)
                    .SetTooltip($"{propertyTestFloat.propertyPath} ({propertyTestFloat.propertyType})")
                );
        }

        private void ComposeWithDozzy()
        {
            root
                .AddChild(fluidHeader)
                .AddSpaceBlock(3)
                .AddChild(fluidTestString)
                .AddSpaceBlock(1)
                .AddChild(fluidTestInt)
                .AddSpaceBlock(1)
                .AddChild(fluidTestFloat);
        }
#endif
        #endregion

    }
}
