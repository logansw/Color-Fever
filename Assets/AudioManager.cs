using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager s_instance;
    [SerializeField] private AudioSource _buttonSource;
    [SerializeField] private AudioSource _smallScoreAudioSource;
    [SerializeField] private AudioSource _bigScoreAudioSource;
    [SerializeField] private AudioSource _undoAudioSource;
    [SerializeField] private AudioSource _diceAudioSource;
    [SerializeField] private Slider _volumeSlider;

    public float ButtonVolume;
    public float SmallScoreVolume;
    public float BigScoreVolume;
    public float UndoVolume;
    public float DiceVolume;

    private void Awake() {
        s_instance = this;
        if (_volumeSlider != null) {
            _volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
        }
    }

    public void PlayButtonSound() {
        _buttonSource.Play();
    }

    public void PlayScoreSmall() {
        _smallScoreAudioSource.Play();
    }

    public void PlayScoreBig() {
        _bigScoreAudioSource.Play();
    }

    public void PlayUndoSound() {
        _undoAudioSource.Play();
    }

    public void PlayDiceSound() {
        _diceAudioSource.Play();
    }

    public void SetVolume(float value) {
        _buttonSource.volume = ButtonVolume * value;
        _smallScoreAudioSource.volume = SmallScoreVolume * value;
        _bigScoreAudioSource.volume = BigScoreVolume * value;
        _undoAudioSource.volume = UndoVolume * value;
        _diceAudioSource.volume = DiceVolume * value;
        PlayerPrefs.SetFloat("Volume", value);
    }
}
