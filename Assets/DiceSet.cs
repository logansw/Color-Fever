using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSet : MonoBehaviour
{
    [SerializeField] private Transform _positionOne;
    [SerializeField] private Transform _positionTwo;
    [SerializeField] private GameObject _diceAndRoll;
    [SerializeField] private GameObject _newTileButton;
    private bool _isPositionOne;

    private void Start() {
        _isPositionOne = true;
    }

    public void TogglePosition() {
        if (_isPositionOne) {
            _diceAndRoll.transform.position = _positionTwo.position;
            _diceAndRoll.transform.rotation = _positionTwo.rotation;
            _newTileButton.transform.position = _positionOne.position;
            _newTileButton.transform.rotation = _positionOne.rotation;
            _isPositionOne = false;
        } else {
            _diceAndRoll.transform.position = _positionOne.position;
            _diceAndRoll.transform.rotation = _positionOne.rotation;
            _newTileButton.transform.position = _positionTwo.position;
            _newTileButton.transform.rotation = _positionTwo.rotation;
            _isPositionOne = true;
        }
    }
}
