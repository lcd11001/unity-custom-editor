using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Runtime
{
    public class SandboxSettings : ScriptableObject
    {
        private static string assetName => nameof(SandboxSettings);

        private static SandboxSettings _instance;
        public static SandboxSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<SandboxSettings>(assetName);
                    if (_instance == null)
                    {
                        _instance = CreateInstance<SandboxSettings>();
                    }
#if UNITY_EDITOR
                    UnityEditor.AssetDatabase.CreateAsset(_instance, $"Assets/Sandbox/Data/Resources/{assetName}.asset");
#endif
                }
                return _instance;
            }
        }

        public string TestString = "Hello World";
        public int TestInt = 42;
        public float TestFloat = 3.14f;
    }
}
