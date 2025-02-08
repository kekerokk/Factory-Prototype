using System;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
    [SerializeField] GameResources _resources;
    [SerializeField] List<ResourceCreator> _creators = new();
    [SerializeField] CreatorInfo _creatorInfoPrefab;
    [SerializeField] Transform _creatorsInfoSpawnPlace;
    [SerializeField] Vector3 _spawnStep = new Vector3(2, 0, 0);

    Queue<Resource> _createdResources = new();
    Vector3 _currentSpawnStep;

    public event Action<Resource> OnResourceTaken = delegate { }; 

    public void Initialize() {
        InstantiateCreators();
        _creators.ForEach(x => x.StartCreating());
    }

    public IEnumerable<Resource> GetResources(int requiredCount) {
        int transferCount = requiredCount > _createdResources.Count ? _createdResources.Count : requiredCount;
        List<Resource> forTransfer = new(transferCount);

        for (int i = 0; i < transferCount; i++) {
            Resource resource = _createdResources.Dequeue();
            forTransfer.Add(resource);
            OnResourceTaken.Invoke(resource);
        }

        return forTransfer;
    }

    void InstantiateCreators() {
        foreach (ResourceConfig resource in _resources.resources) {
            ResourceCreator creator = new(resource, this);
            InstantiateCreatorInfo(creator);
            creator.OnResourceCreated += createdResource => _createdResources.Enqueue(createdResource);
            _creators.Add(creator);
        }
    }

    void InstantiateCreatorInfo(ResourceCreator creator) {
        CreatorInfo info = Instantiate(_creatorInfoPrefab,
            _creatorsInfoSpawnPlace.position + _currentSpawnStep,
            _creatorsInfoSpawnPlace.rotation,
            transform);
        info.gameObject.SetActive(true);
        info.Initialize(creator, this);

        _currentSpawnStep += _spawnStep;
    }
}