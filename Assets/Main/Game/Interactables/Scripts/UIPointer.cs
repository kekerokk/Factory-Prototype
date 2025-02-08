using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
public class UIPointer : MonoBehaviour {
    [SerializeField] float _yMove = 1f;
    [SerializeField] int _repeats = 3;
    
    Coroutine _pointMovement;
    RectTransform _rectTransform;
    Vector3 _basePosition;
    
    void Awake() {
        _rectTransform = transform as RectTransform;
        _basePosition = _rectTransform.position;
        // transform.rotation = Camera.main.transform.rotation;
    }

    public void Activate() {
        gameObject.SetActive(true);

        if (_pointMovement.IsUnityNull()) _pointMovement = StartCoroutine(PointMove());
        else {
            StopCoroutine(_pointMovement);
            _pointMovement = StartCoroutine(PointMove());
        }
    }

    void Deactivate() {
        _pointMovement = null;
        gameObject.SetActive(false);
    }
    
    IEnumerator PointMove() {
        _rectTransform.DOKill();
        _rectTransform.transform.position = _basePosition;
        int count = 0;
        
        while (count < _repeats) {
            yield return new DOTweenCYInstruction.WaitForCompletion(_rectTransform.DOLocalMoveY(_yMove, 1).SetRelative());
            yield return new DOTweenCYInstruction.WaitForCompletion(_rectTransform.DOLocalMoveY(-_yMove, 1).SetRelative());
            count++;
        }
        
        Deactivate();
    }
}
