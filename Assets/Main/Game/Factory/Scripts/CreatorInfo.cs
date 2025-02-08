using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatorInfo : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _resourceName, _countText;
    [SerializeField] Slider _progress;
    
    ResourceCreator _creator;
    int resourceCount = 0;
    
    public void Initialize(ResourceCreator creator, Factory factory) {
        _creator = creator;
        _creator.OnProgress += UpdateProgress;
        _creator.OnCreated += Increase;
        factory.OnResourceTaken += TryDecrease;
        _resourceName.text = _creator.createableResource.Name;
    }
    
    void Increase() {
        resourceCount++;
        _countText.text = $"{resourceCount}";
    }
    
    void TryDecrease(Resource resource) {
        if (resource.Name != _creator.createableResource.Name) return;
        
        resourceCount--;
        _countText.text = $"{resourceCount}";
    }
    
    void UpdateProgress(float progress) {
        _progress.value = progress;
    }

    
}
