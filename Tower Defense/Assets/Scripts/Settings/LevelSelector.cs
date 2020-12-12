using Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Settings {
    public class LevelSelector : MonoBehaviour
    {
        private Button[] _lvlButtons;
        private int _maxLvl;

        private void Awake() {
            //get maximum lvl reached
            _maxLvl = 1;
            Preferences.SetMaxLvl(_maxLvl);
            
            //loop through all buttons and disable ones not reached
            _lvlButtons = GetComponentsInChildren<Button>();
            for (int i = 0; i < _lvlButtons.Length; i++) {
                if (i + 1 > _maxLvl) {
                    _lvlButtons[i].interactable = false;
                }
            }
        }
    }
}
