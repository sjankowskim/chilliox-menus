using Newtonsoft.Json;
using System.IO;
using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace ChillioXMenus
{
    class QuicksilverMenu : ModMenu
    {
        private static Button saveButton;
        private static Toggle instantStopToggle;
        private static Toggle indicatorToggle;
        private static Toggle trailToggle;
        private static Dropdown musicDropdown;
        private static Button volumeBigMinus, volumeMinus, volumePlus, volumeBigPlus;
        private static Text volumeText;
        private static Button speedBigMinus, speedMinus, speedPlus, speedBigPlus;
        private static Text speedText;
        private static Toggle timeToggle;
        private static Button timeBigMinus, timeMinus, timePlus, timeBigPlus;
        private static Text timeText;

        public static void InitMenu(Menu menu)
        {
            saveButton = menu.GetCustomReference("QuicksilverSaveButton").GetComponent<Button>();
            saveButton.onClick.AddListener(SaveData);

            instantStopToggle = menu.GetCustomReference("QuicksilverInstantStopToggle").GetComponent<Toggle>();
            instantStopToggle.onValueChanged.AddListener(delegate { ToggleChange(instantStopToggle.isOn, ref Quicksilver.QuicksilverModule.data.instantStop); });

            indicatorToggle = menu.GetCustomReference("QuicksilverIndicatorToggle").GetComponent<Toggle>();
            indicatorToggle.onValueChanged.AddListener(delegate { ToggleChange(indicatorToggle.isOn, ref Quicksilver.QuicksilverModule.data.lightningIndicators); });

            trailToggle = menu.GetCustomReference("QuicksilverTrailToggle").GetComponent<Toggle>();
            trailToggle.onValueChanged.AddListener(delegate { ToggleChange(trailToggle.isOn, ref Quicksilver.QuicksilverModule.data.lightningTrail); });

            musicDropdown = menu.GetCustomReference("QuicksilverMusicDropdown").GetComponent<Dropdown>();
            musicDropdown.onValueChanged.AddListener(DropdownChange);

            volumeBigMinus = menu.GetCustomReference("QuicksilverMusicBigMinus").GetComponent<Button>();
            volumeMinus = menu.GetCustomReference("QuicksilverMusicMinus").GetComponent<Button>();
            volumePlus = menu.GetCustomReference("QuicksilverMusicPlus").GetComponent<Button>();
            volumeBigPlus = menu.GetCustomReference("QuicksilverMusicBigPlus").GetComponent<Button>();
            volumeText = menu.GetCustomReference("QuicksilverMusicText").GetComponent<Text>();
            volumeBigMinus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.musicVolume, -0.1f, volumeText); });
            volumeMinus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.musicVolume, -0.01f, volumeText); });
            volumePlus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.musicVolume, 0.01f, volumeText); });
            volumeBigPlus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.musicVolume, 0.1f, volumeText); });

            speedBigMinus = menu.GetCustomReference("QuicksilverSpeedBigMinus").GetComponent<Button>();
            speedMinus = menu.GetCustomReference("QuicksilverSpeedMinus").GetComponent<Button>();
            speedPlus = menu.GetCustomReference("QuicksilverSpeedPlus").GetComponent<Button>();
            speedBigPlus = menu.GetCustomReference("QuicksilverSpeedBigPlus").GetComponent<Button>();
            speedText = menu.GetCustomReference("QuicksilverSpeedText").GetComponent<Text>();
            speedBigMinus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.movementSpeed, -1.0f, speedText); });
            speedMinus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.movementSpeed, -0.1f, speedText); });
            speedPlus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.movementSpeed, 0.1f, speedText); });
            speedBigPlus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.movementSpeed, 1.0f, speedText); });

            timeToggle = menu.GetCustomReference("QuicksilverCustomTimeToggle").GetComponent<Toggle>();
            timeToggle.onValueChanged.AddListener(delegate { ToggleChange(timeToggle.isOn, ref Quicksilver.QuicksilverModule.data.useCustomTimescale); });

            timeBigMinus = menu.GetCustomReference("QuicksilverCustomTimeBigMinus").GetComponent<Button>();
            timeMinus = menu.GetCustomReference("QuicksilverCustomTimeMinus").GetComponent<Button>();
            timePlus = menu.GetCustomReference("QuicksilverCustomTimePlus").GetComponent<Button>();
            timeBigPlus = menu.GetCustomReference("QuicksilverCustomTimeBigPlus").GetComponent<Button>();
            timeText = menu.GetCustomReference("QuicksilverCustomTimeText").GetComponent<Text>();
            timeBigMinus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.customTimescale, -0.1f, timeText); });
            timeMinus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.customTimescale, -0.01f, timeText); });
            timePlus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.customTimescale, 0.01f, timeText); });
            timeBigPlus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.customTimescale, 0.1f, timeText); });
        }

        public static void UpdateMenu()
        {
            instantStopToggle.isOn = Quicksilver.QuicksilverModule.data.instantStop;
            indicatorToggle.isOn = Quicksilver.QuicksilverModule.data.lightningIndicators;
            trailToggle.isOn = Quicksilver.QuicksilverModule.data.lightningTrail;
            musicDropdown.value = (int)Quicksilver.QuicksilverModule.data.backgroundMusic;
            volumeText.text = Quicksilver.QuicksilverModule.data.musicVolume.ToString("0.0#");
            speedText.text = Quicksilver.QuicksilverModule.data.movementSpeed.ToString("0.0#");
            timeToggle.isOn = Quicksilver.QuicksilverModule.data.useCustomTimescale;
            timeText.text = Quicksilver.QuicksilverModule.data.customTimescale.ToString("0.0#");
        }

        private static void DropdownChange(int arg0)
        {
            Quicksilver.QuicksilverModule.data.backgroundMusic = (Quicksilver.QuicksilverMusic)arg0;
        }

        private static void SaveData()
        {
            File.WriteAllText(Application.streamingAssetsPath + Quicksilver.QuicksilverModule.OPTIONS_FILE_PATH, JsonConvert.SerializeObject(Quicksilver.QuicksilverModule.data));
            Debug.Log("Saving Quicksilver settings!");
        }
    }
}
