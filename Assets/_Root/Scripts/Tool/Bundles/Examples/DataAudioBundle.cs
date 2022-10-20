using System;
using UnityEngine;

namespace Tool.Bundles.Examples
{
    [Serializable]
    internal class DataAudioBundle
    {
        [field: SerializeField] public string NameAssetBundle { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
    }
}
