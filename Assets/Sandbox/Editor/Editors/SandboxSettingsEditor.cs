using System.Collections;
using System.Collections.Generic;
using Sandbox.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sandbox.Editor.Editors
{
    [CustomEditor(typeof(SandboxSettings), true)]
    public class SandboxSettingsEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();



            return root;
        }
    }
}
