using UnityEngine;
public class Resource : MonoBehaviour {
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public ResourceType type { get; private set; }
}
