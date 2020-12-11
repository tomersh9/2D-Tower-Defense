using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Options : MonoBehaviour {
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Slider volumeSlider;

    private MusicPlayer _musicPlayer;

    private void Awake() {
        _musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    private void Start() {
        //SFX toggle
        sfxToggle.isOn = Preferences.GetToggleSfx();
        sfxToggle.onValueChanged.AddListener(HandleToggleChange);

        //Volume slider
        volumeSlider.minValue = 0;
        volumeSlider.maxValue = 1;
        volumeSlider.value = Preferences.GetVolume();
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
    }

    private void HandleToggleChange(bool isOn) {
        Preferences.ToggleSfx(isOn);
    }

    private void HandleVolumeChange(float volume) {
        Preferences.SetVolume(volume);
        _musicPlayer.SetMusicPlayerVolume(volume);
    }
}