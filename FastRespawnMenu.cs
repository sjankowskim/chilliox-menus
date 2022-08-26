using Newtonsoft.Json.Linq;
using System;
using System.IO;
using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace ChillioXMenus
{
    class FastRespawnMenu : ModMenu
    {
        private static Button saveButton;
        private static Dropdown respawnDropdown;
        private static Button delayBigMinus, delayMinus, delayPlus, delayBigPlus;
        private static Text delayText;

        public static void InitMenu(Menu menu)
        {
            saveButton = menu.GetCustomReference("FastRespawnSaveButton").GetComponent<Button>();
            saveButton.onClick.AddListener(SaveData);

            respawnDropdown = menu.GetCustomReference("FastRespawnDropdown").GetComponent<Dropdown>();

            delayBigMinus = menu.GetCustomReference("FastRespawnDelayBigMinus").GetComponent<Button>();
            delayMinus = menu.GetCustomReference("FastRespawnDelayMinus").GetComponent<Button>();
            delayPlus = menu.GetCustomReference("FastRespawnDelayPlus").GetComponent<Button>();
            delayBigPlus = menu.GetCustomReference("FastRespawnDelayBigPlus").GetComponent<Button>();
            delayText = menu.GetCustomReference("FastRespawnDelayText").GetComponent<Text>();
            delayBigMinus.onClick.AddListener(delegate { AddToText(-1.0f, delayText); });
            delayMinus.onClick.AddListener(delegate { AddToText( -0.1f, delayText); });
            delayPlus.onClick.AddListener(delegate { AddToText(0.1f, delayText); });
            delayBigPlus.onClick.AddListener(delegate { AddToText(1.0f, delayText); });
        }

        private static void SaveData()
        {
            JObject json = JObject.Parse(File.ReadAllText(Application.streamingAssetsPath + "\\Mods\\FastRespawn\\Level_Master.json"));
            json["modes"][0]["modules"][0]["behaviour"] = ((LevelModuleDeath.Behaviour)respawnDropdown.value).ToString();
            json["modes"][0]["modules"][0]["delayBeforeLoad"] = float.Parse(delayText.text);
            File.WriteAllText(Application.streamingAssetsPath + "\\Mods\\FastRespawn\\Level_Master.json", json.ToString());
            Debug.Log("(Chillio X Menus) Saving Fast Respawn settings!");
        }

        public static void UpdateMenu()
        {
            JObject json = JObject.Parse(File.ReadAllText(Application.streamingAssetsPath + "\\Mods\\FastRespawn\\Level_Master.json"));
            string type = json["modes"][0]["modules"][0]["behaviour"].ToString();
            respawnDropdown.value = (int)Enum.Parse(typeof(LevelModuleDeath.Behaviour), type);
            delayText.text = float.Parse(json["modes"][0]["modules"][0]["delayBeforeLoad"].ToString()).ToString("0.0#");
        }
    }
}
