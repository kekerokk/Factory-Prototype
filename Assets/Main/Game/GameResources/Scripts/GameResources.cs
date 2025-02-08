using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameResources", menuName = "Scriptable Objects/Game Resources")]
public class GameResources : ScriptableObject {
    [field: SerializeField] public List<ResourceConfig> resources { get; private set; } 
}