using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSet : MonoBehaviour
{
    [SerializeField] private Transform _positionOne;
    [SerializeField] private Transform _positionTwo;
    private bool _isPositionOne;

    private void Start() {
        _isPositionOne = true;
    }

    public void TogglePosition() {
        if (_isPositionOne) {
            transform.position = _positionTwo.position;
            transform.rotation = _positionTwo.rotation;
            _isPositionOne = false;
        } else {
            transform.position = _positionOne.position;
            transform.rotation = _positionOne.rotation;
            _isPositionOne = true;
        }
    }
}
