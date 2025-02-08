using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour {
    [SerializeField] GameResources _resources;
    [SerializeField] ResourceStorage _storagePrefab;
    [SerializeField] Transform _spawnPlace;
    [SerializeField] float _distance = 4f;
    
    List<ResourceType> _requiredTypes = new();
    List<ResourceStorage> _storages = new();
    
    public void Initialize() {
        foreach (ResourceConfig config in _resources.resources) {
            if (_requiredTypes.Contains(config.resource.type)) continue;
            _requiredTypes.Add(config.resource.type);
        }

        int count = 0;
        foreach (ResourceType resourceType in _requiredTypes) {
            Vector3 spawnPos = _spawnPlace.position + (-transform.right * (_distance * count));
            var resourceStorage = Instantiate(_storagePrefab, spawnPos, transform.rotation, transform);
            resourceStorage.Initialize(resourceType);
            _storages.Add(resourceStorage);
            
            count++;
        }
    }

    public IEnumerable<ResourceStorage> GetStorages() => _storages;
}