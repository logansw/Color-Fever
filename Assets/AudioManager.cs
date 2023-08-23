using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager s_instance;
    [SerializeField] private AudioSource _buttonSource;
    private bool _firstPlayed;
    private int _lastIndexPlayed;
    // private int _tonesPlayedSinceHome;

    private void Awake() {
        s_instance = this;
        _firstPlayed = false;
    }

    public void PlayButtonSound() {
        _buttonSource.Play();
        // if (!_firstPlayed) {
        //     _buttonSource.pitch = Mathf.Pow(1.05946f, -5);
        //     _buttonSource.Play();
        //     _lastIndexPlayed = 0;
        //     _firstPlayed = true;
        // } else {
        //     // int[] shiftings = new int[5] { 0, 2, 4, 5, 7 };
        //     int[] shiftings = new int[4] { 0, 2, 4, 7 };
        //     int index = Random.Range(0, shiftings.Length);
        //     while (index == _lastIndexPlayed) {
        //         index = Random.Range(0, shiftings.Length);
        //     }
        //     // if (index == 0) {
        //     //     _tonesPlayedSinceHome = 0;
        //     // } else {
        //     //     _tonesPlayedSinceHome++;
        //     // }
        //     // if (_tonesPlayedSinceHome >= 5) {
        //     //     index = 0;
        //     //     _tonesPlayedSinceHome = 0;
        //     // }
        //     _lastIndexPlayed = index;
        //     int power = shiftings[index];
        //     _buttonSource.pitch = Mathf.Pow(1.05946f, power - 5);
        //     _buttonSource.Play();
        // }
    }
}
