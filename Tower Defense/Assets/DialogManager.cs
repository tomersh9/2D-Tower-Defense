using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour {
    [Header("Dialogs")] 
    [SerializeField] private GameObject looseLabel;
    [SerializeField] private GameObject winLabel;
    [SerializeField] private GameObject waveFinishLabel;
    [SerializeField] private GameObject pauseLabel;

    private bool _isPaused = false;

    private void Awake() {
        looseLabel.SetActive(false);
        winLabel.SetActive(false);
        waveFinishLabel.SetActive(false);
        pauseLabel.SetActive(false);
    }

    public void ActivateWinLabel() => winLabel.SetActive(true);

    public void ActivateLooseLabel() => looseLabel.SetActive(true);


    public void TogglePauseLabel(float currGameSpeed) {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : currGameSpeed;
        pauseLabel.SetActive(_isPaused);
    }

    public void ActivateWaveFinishLabel() => StartCoroutine(PlayAnimation());

    private IEnumerator PlayAnimation() {
        waveFinishLabel.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        waveFinishLabel.SetActive(false);
    }
}