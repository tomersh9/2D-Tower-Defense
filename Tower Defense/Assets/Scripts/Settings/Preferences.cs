using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : MonoBehaviour {

    private const string SFX = "sfx";
    private const string VOLUME = "volume";
    private const string CURRENT_LVL = "currLvl";
    private const string MAX_LVL = "maxLvl";

    public static void ToggleSfx(bool toggle) {
        int res = 0;
        if (toggle) {
            res = 1;
        }
        PlayerPrefs.SetInt(SFX,res);
    }

    public static int GetCurrentLvl() => PlayerPrefs.GetInt(CURRENT_LVL, 1);

    public static void SetCurrentLvl(int lvl) => PlayerPrefs.SetInt(CURRENT_LVL, lvl);
    public static int GetMaxLvl() => PlayerPrefs.GetInt(MAX_LVL,1);
    
    public static void SetMaxLvl(int lvl) => PlayerPrefs.SetInt(MAX_LVL,lvl);

    public static bool GetToggleSfx() => PlayerPrefs.GetInt(SFX) == 1;

    public static void SetVolume(float volume) => PlayerPrefs.SetFloat(VOLUME,volume);

    public static float GetVolume() => PlayerPrefs.GetFloat(VOLUME);
}