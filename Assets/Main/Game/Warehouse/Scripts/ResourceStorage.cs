using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ResourceStorage : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _resourceTypeText;
    [SerializeField] List<Transform> _placeHolders;
    [SerializeField] List<Resource> _resources = new();
    [SerializeField] Knopka _knopka;
    [SerializeField] Transform _putPlace;

    ResourceType _resourceType;
    
    public event Action<ResourceStorage> OnStorageChose = delegate { };
    public IButton button => _knopka;
    public Transform putPlace => _putPlace;

    public void Initialize(ResourceType resourceType) {
        _resourceType = resourceType;
        _resourceTypeText.text = resourceType.title;
        _knopka.OnPress += () => OnStorageChose.Invoke(this);
    }
    
    public void PutResource(Resource resource) {
        resource.transform.parent = transform;
        _resources.Add(resource);
        RefreshView();
        
        if (_resourceType != resource.type) {
            DestroyResource(resource);
        }
    }
    
    void RefreshView() {
        int count = _resources.Count > _placeHolders.Count ? _placeHolders.Count : _resources.Count;
        for (int i = 0; i < count; i++) {
            _resources[i].transform.position = _placeHolders[i].position;
            _resources[i].gameObject.SetActive(true);
        }
    }
    
    void DestroyResource(Resource resource) {
        StartCoroutine(DestroyEffect(resource));
    }

    IEnumerator DestroyEffect(Resource resource) {
        yield return new DOTweenCYInstruction.WaitForCompletion(resource.transform.DOScale(Vector3.zero, 1));
        
        _resources.Remove(resource);
        Destroy(resource.gameObject);
    }
}
