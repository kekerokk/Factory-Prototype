using System;
using UnityEngine;
[Serializable]
public class ResourceConfig {
    [field: SerializeField] public GameObject gameObject {get; private set; }
    [field: SerializeField] public Resource resource { get; private set; }
    [field: SerializeField] public float createTime {get; private set; }
}
