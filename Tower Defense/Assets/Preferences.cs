using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : MonoBehaviour {

    private const string SFX = "sfx";
    private const string VOLUME = "volume";

    public static void ToggleSfx(bool toggle) {
        int res = 0;
        if (toggle) {
            res = 1;
        }
        PlayerPrefs.SetInt(SFX,res);
    }

    public static bool GetToggleSfx() => PlayerPrefs.GetInt(SFX) == 1;

    public static void SetVolume(float volume) => PlayerPrefs.SetFloat(VOLUME,volume);

    public static float GetVolume() => PlayerPrefs.GetFloat(VOLUME);
}