using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sandbox.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Sandbox.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;



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

#if USE_DOZZY
        #region Dozzy
        // require Doozy package
        private FluidComponentHeader fluidHeader { get; set; }
        private FluidField fluidTestString { get; set; }
        private FluidField fluidTestInt { get; set; }
        private FluidField fluidTestFloat { get; set; }

        private void InitializeEditorWithDozzy()
        {
            root = DesignUtils.editorRoot;

            fluidHeader = FluidComponentHeader.Get()
                .SetComponentNameText(SandboxUtils.GetWords(nameof(SandboxSettings)))
                .SetIcon(EditorSpriteSheets.EditorUI.Icons.Settings)
                .SetAccentColor(EditorColors.EditorUI.DeepPurple);

            fluidTestString = FluidField.Get()
                .SetLabelText(SandboxUtils.GetWords(nameof(SandboxSettings.TestString)))
                .AddFieldContent(
                    DesignUtils.row
                        .AddChild(DesignUtils
                            .NewTextField(propertyTestString)
                            .SetStyleFlexGrow(1)
                            .SetTooltip($"{propertyTestString.propertyPath} ({propertyTestString.propertyType})")
                        )
                        .AddSpaceBlock(2)
                        .AddChild(GetResetButton(() =>
                            {
                                propertyTestString.stringValue = SandboxSettings.k_TestStringDefaultValue;
                                serializedObject.ApplyModifiedProperties();
                            })
                            .SetTooltip("Reset to default string value")
                        )
                );

            var increaseIntValueButton = GetSmallButton(EditorSpriteSheets.EditorUI.Icons.Plus)
                .SetAccentColor(EditorSelectableColors.Default.Add)
                .SetOnClick(() =>
                {
                    propertyTestInt.intValue++;
                    serializedObject.ApplyModifiedProperties();
                });

            var decreaseIntValueButton = GetSmallButton(EditorSpriteSheets.EditorUI.Icons.Minus)
                .SetAccentColor(EditorSelectableColors.Default.Remove)
                .SetOnClick(() =>
                {
                    propertyTestInt.intValue--;
                    serializedObject.ApplyModifiedProperties();
                });

            fluidTestInt = FluidField.Get()
                .SetLabelText(SandboxUtils.GetWords(nameof(SandboxSettings.TestInt)))
                .SetStyleFlexGrow(1)
                .SetStyleFlexBasis(1)
                .AddFieldContent(
                    DesignUtils.row
                        .AddChild(DesignUtils.NewIntegerField(propertyTestInt)
                            .SetStyleFlexGrow(1)
                            .SetTooltip($"{propertyTestInt.propertyPath} ({propertyTestInt.propertyType})")
                        )
                        .AddSpaceBlock(1)
                        .AddChild(increaseIntValueButton)
                        .AddSpaceBlock(1)
                        .AddChild(decreaseIntValueButton)
                        .AddSpaceBlock(2)
                        .AddChild(GetResetButton(() =>
                            {
                                propertyTestInt.intValue = SandboxSettings.k_TestIntDefaultValue;
                                serializedObject.ApplyModifiedProperties();
                            })
                            .SetTooltip("Reset to default int value")
                        )
                );

            fluidTestFloat = FluidField.Get()
                .SetLabelText(SandboxUtils.GetWords(nameof(SandboxSettings.TestFloat)))
                .SetStyleFlexGrow(1)
                .SetStyleFlexBasis(1)
                .AddFieldContent(
                    DesignUtils.row
                        .AddChild(DesignUtils
                            .NewFloatField(propertyTestFloat)
                            .SetStyleFlexGrow(1)
                            .SetTooltip($"{propertyTestFloat.propertyPath} ({propertyTestFloat.propertyType})")
                        )
                        .AddSpaceBlock(2)
                        .AddChild(GetResetButton(() =>
                            {
                                propertyTestFloat.floatValue = SandboxSettings.k_TestFloatDefaultValue;
                                serializedObject.ApplyModifiedProperties();
                            })
                            .SetTooltip("Reset to default float value")
                        )
                );
        }

        private static FluidButton GetSmallButton(List<Texture2D> icons) => FluidButton
            .Get()
            .SetButtonStyle(ButtonStyle.Contained)
            .SetElementSize(ElementSize.Tiny)
            .SetStyleFlexShrink(0)
            .SetAccentColor(EditorSelectableColors.Default.Action)
            .SetIcon(icons);

        private static FluidButton GetResetButton(UnityAction callback) =>
            GetSmallButton(EditorSpriteSheets.EditorUI.Icons.Reset)
            .SetOnClick(callback);

        private void ComposeWithDozzy()
        {
            root
                .AddChild(fluidHeader)
                .AddSpaceBlock(3)
                .AddChild(fluidTestString)
                .AddSpaceBlock(1)
                .AddChild(
                    DesignUtils.row
                        .AddChild(fluidTestInt)
                        .AddSpaceBlock(1)
                        .AddChild(fluidTestFloat)
                );
        }

        #endregion

        #region Chaining Methods

        public SandboxSettingsEditor HideHeader()
        {
            fluidHeader.Hide();
            return this;
        }

        public SandboxSettingsEditor ShowHeader()
        {
            fluidHeader.Show();
            return this;
        }

        public SandboxSettingsEditor SetRootPadding(float value)
        {
            root.SetStylePadding(value);
            return this;
        }

        #endregion
#endif

    }
}
