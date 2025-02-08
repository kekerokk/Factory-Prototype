using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectionArea : MonoBehaviour {
    [SerializeField] Factory _factory;
    [SerializeField] Knopka _getFactoryResources, _deloveryRequest;
    [SerializeField] List<Transform> _placeHolders;

    public IButton deliveryRequest => _deloveryRequest;
    public Transform takePlace;
    
    Queue<Resource> _resources = new();

    public bool haveResource => _resources.Count > 0;
    
    void Awake() {
        _getFactoryResources.OnPress += GetResources;
    }

    void GetResources() {
        int resourcesCount = _resources.Count;
        var resources = _factory.GetResources(_placeHolders.Count - resourcesCount);

        foreach (Resource resource in resources) {
            resource.gameObject.SetActive(true);
            resource.transform.parent = transform;
            resource.transform.position = _placeHolders[resourcesCount].position;
            resourcesCount++;
            _resources.Enqueue(resource);
        }
    }
    
    void RefreshResourcesPosition() {
        List<Resource> listedResourced = _resources.ToList();
        for (int i = 0; i < _resources.Count; i++) {
            listedResourced[i].transform.position = _placeHolders[i].position;
        }
    }
    
    public Resource TakeResource() {
        var resource = _resources.Dequeue();
        RefreshResourcesPosition();
        
        return resource;
    }
}