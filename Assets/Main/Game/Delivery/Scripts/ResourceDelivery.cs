using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceDelivery : MonoBehaviour {
    [SerializeField] CollectionArea _collectionArea;
    [SerializeField] Warehouse _warehouse;
    [SerializeField] Carrier _carrierPrefab;
    [SerializeField] Transform _spawnPlace;

    [SerializeField] List<ResourceStorage> _storages;

    bool _isTrackingChose;
    
    public void Initialize() {
        _storages = _warehouse.GetStorages().ToList();
        _collectionArea.deliveryRequest.OnPress += TrackStorageChoose;
        _collectionArea.deliveryRequest.OnPress += OneTimeDesignation;
    }

    void OneTimeDesignation() {
        _storages.ForEach(x=>x.button.Designate());
        _collectionArea.deliveryRequest.OnPress -= OneTimeDesignation;
    }
    
    void TrackStorageChoose() {
        if (!_collectionArea.haveResource) return;
        if (_isTrackingChose) return;

        _isTrackingChose = true;
        _storages.ForEach(x => x.OnStorageChose += Delivery);
    }
    
    void Delivery(ResourceStorage storage) {
        Debug.Log("Starting Delivery");
        
        _isTrackingChose = false;
        _storages.ForEach(x => x.OnStorageChose -= Delivery);

        Carrier carrier = Instantiate(_carrierPrefab);
        carrier.transform.position = _spawnPlace.position + new Vector3(0, 0.5f, 0);
        carrier.StartWork(storage, _collectionArea);
    }
    
}
