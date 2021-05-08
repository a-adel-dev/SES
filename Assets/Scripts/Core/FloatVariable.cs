using System.Collections;
using UnityEngine;

namespace SES.Core
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Float Variable")]
    public class FloatVariable : ScriptableObject
    {
        public float value = 0f;
    }
}