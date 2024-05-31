using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Runtime.Utils
{
    public static class SandboxUtils
    {
        public static string GetWords(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
        }
    }
}
