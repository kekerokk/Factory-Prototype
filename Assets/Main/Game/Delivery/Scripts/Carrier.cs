using System.Collections;
using UnityEngine;

public class Carrier : MonoBehaviour {
    [SerializeField] float _speed;

    [SerializeField] Resource _takenResource;
    ResourceStorage _targetStorage;
    CollectionArea _giver;
    
    public void StartWork(ResourceStorage targetStorage, CollectionArea resourceGiver) {
        _targetStorage = targetStorage;
        _giver = resourceGiver;
        
        StartCoroutine(WorkHandler());
    }
    
    IEnumerator WorkHandler() {
        while (true) {
            yield return GoTo(_giver.takePlace);

            _takenResource = _giver.TakeResource();
            _takenResource.gameObject.SetActive(false);

            yield return GoTo(_targetStorage.putPlace);

            _targetStorage.PutResource(_takenResource);
            Destroy(gameObject);
            
            yield break;
        }
    }

    IEnumerator GoTo(Transform target) {
        Vector3 direction = ((target.position + new Vector3(0, 0.5f, 0)) - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        
        while ((target.position - transform.position).sqrMagnitude > 1f) {
            transform.position += direction * (_speed * Time.deltaTime);

            yield return null;
        }
    }
}
