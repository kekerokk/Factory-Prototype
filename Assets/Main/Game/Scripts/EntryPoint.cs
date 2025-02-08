using UnityEngine;

public class EntryPoint : MonoBehaviour {
    [SerializeField] Factory _factory;
    [SerializeField] Warehouse _warehouse;
    [SerializeField] ResourceDelivery _resourceDelivery;
    
    void Awake() {
        _factory.Initialize();
        _warehouse.Initialize();
        _resourceDelivery.Initialize();
    }
}