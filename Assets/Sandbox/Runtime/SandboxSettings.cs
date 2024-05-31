using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Runtime
{
    public class SandboxSettings : ScriptableObject
    {
        private static string assetName => nameof(SandboxSettings);

        public const string k_TestStringDefaultValue = "Hello World";
        public const int k_TestIntDefaultValue = 42;
        public const float k_TestFloatDefaultValue = 3.14f;

        #region Singleton
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
#if UNITY_EDITOR
                        UnityEditor.AssetDatabase.CreateAsset(_instance, $"Assets/Sandbox/Data/Resources/{assetName}.asset");
#endif
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region PlayerPrefs

        private static string GetKey(string key) => $"{nameof(SandboxSettings)}_{key}";

        private static string GetString(string key) => PlayerPrefs.GetString(GetKey(key), k_TestStringDefaultValue);
        private static void SetString(string key, string value) => PlayerPrefs.SetString(GetKey(key), value);

        private static int GetInt(string key) => PlayerPrefs.GetInt(GetKey(key), k_TestIntDefaultValue);
        private static void SetInt(string key, int value) => PlayerPrefs.SetInt(GetKey(key), value);

        private static float GetFloat(string key) => PlayerPrefs.GetFloat(GetKey(key), k_TestFloatDefaultValue);
        private static void SetFloat(string key, float value) => PlayerPrefs.SetFloat(GetKey(key), value);

        #endregion

        #region Save/Load

        public static void Save()
        {
            SetString(nameof(TestString), _instance.TestString);
            SetInt(nameof(TestInt), _instance.TestInt);
            SetFloat(nameof(TestFloat), _instance.TestFloat);
            PlayerPrefs.Save();
        }

        public static void Load()
        {
            _instance.TestString = GetString(nameof(TestString));
            _instance.TestInt = GetInt(nameof(TestInt));
            _instance.TestFloat = GetFloat(nameof(TestFloat));

        }

        #endregion

        public string TestString = k_TestStringDefaultValue;
        public int TestInt = k_TestIntDefaultValue;
        public float TestFloat = k_TestFloatDefaultValue;
    }
}
