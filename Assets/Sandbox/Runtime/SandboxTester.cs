using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Runtime
{
    public class SandboxTester : MonoBehaviour
    {
        private static SandboxSettings settings => SandboxSettings.Instance;
        private void Awake()
        {
            Debug.Log(settings.TestString);
            Debug.Log(settings.TestInt);
            Debug.Log(settings.TestFloat);
        }
    }
}