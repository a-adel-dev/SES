using System.Collections;
using UnityEngine;

namespace SES.Core
{
    [CreateAssetMenu (fileName ="IntVariable", menuName ="Int Variable")]
    public class IntVariable : ScriptableObject
    {
        public int value;
    }
}