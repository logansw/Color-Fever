using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager s_instance;
    [SerializeField] private AudioSource _buttonSource;

    private void Awake() {
        s_instance = this;
    }

    public void PlayButtonSound() {
        int shift = Random.Range(-2, 3);
        _buttonSource.pitch = 1 + (shift * 0.1f);
        _buttonSource.Play();
    }
}
