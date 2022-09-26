using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Services.IAP
{
    [Serializable]
    internal class Product
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public ProductType ProductType { get; private set; }
    }
}
