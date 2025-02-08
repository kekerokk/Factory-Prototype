using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class ResourceCreator {
    [SerializeField] ResourceConfig _resourceConfig;
    [SerializeField] string _state;

    MonoBehaviour _mono;

    public event Action<Resource> OnResourceCreated = delegate { };
    public event Action OnCreated = delegate { };
    public event Action<float> OnProgress = delegate { };

    public Resource createableResource => _resourceConfig.resource;
    
    public ResourceCreator(ResourceConfig resourceConfig, MonoBehaviour mono) {
        _resourceConfig = resourceConfig;
        _mono = mono;
    }

    public void StartCreating() {
        _mono.StartCoroutine(CreateHandler());
    }

    IEnumerator CreateHandler() {
        while (true) {
            yield return ProgressTracker(_resourceConfig.createTime);

            GameObject resource = Object.Instantiate(_resourceConfig.gameObject);
            OnResourceCreated.Invoke(resource.GetComponent<Resource>());
            OnCreated.Invoke();
        }
    }

    IEnumerator ProgressTracker(float duration) {
        float currentDuration = 0f;
        
        while (currentDuration < duration) {
            OnProgress.Invoke(currentDuration / duration);
            currentDuration += Time.deltaTime;

            yield return null;
        }
    }
}
