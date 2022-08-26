using Newtonsoft.Json;
using System.IO;
using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace ChillioXMenus
{
    class WingsMenu : ModMenu
    {
        private static Button saveButton;
        private static Button speedBigMinus, speedMinus, speedPlus, speedBigPlus;
        private static Text speedText;
        private static Button accelerationBigMinus, accelerationMinus, accelerationPlus, accelerationBigPlus;
        private static Text accelerationText;

        public static void InitMenu(Menu menu)
        {
            saveButton = menu.GetCustomReference("WingsSaveButton").GetComponent<Button>();
            saveButton.onClick.AddListener(SaveData);

            speedBigMinus = menu.GetCustomReference("WingsSpeedBigMinus").GetComponent<Button>();
            speedMinus = menu.GetCustomReference("WingsSpeedMinus").GetComponent<Button>();
            speedPlus = menu.GetCustomReference("WingsSpeedPlus").GetComponent<Button>();
            speedBigPlus = menu.GetCustomReference("WingsSpeedBigPlus").GetComponent<Button>();
            speedText = menu.GetCustomReference("WingsSpeedText").GetComponent<Text>();
            speedBigMinus.onClick.AddListener(delegate { AddToEntry(ref Wings.WingsLevelModule.wingData.horizontalSpeed, -0.1f, speedText); });
            speedMinus.onClick.AddListener(delegate { AddToEntry(ref Wings.WingsLevelModule.wingData.horizontalSpeed, -0.01f, speedText); });
            speedPlus.onClick.AddListener(delegate { AddToEntry(ref Wings.WingsLevelModule.wingData.horizontalSpeed, 0.01f, speedText); });
            speedBigPlus.onClick.AddListener(delegate { AddToEntry(ref Wings.WingsLevelModule.wingData.horizontalSpeed, 0.1f, speedText); });

            accelerationBigMinus = menu.GetCustomReference("WingsAccelerationBigMinus").GetComponent<Button>();
            accelerationMinus = menu.GetCustomReference("WingsAccelerationMinus").GetComponent<Button>();
            accelerationPlus = menu.GetCustomReference("WingsAccelerationPlus").GetComponent<Button>();
            accelerationBigPlus = menu.GetCustomReference("WingsAccelerationBigPlus").GetComponent<Button>();
            accelerationText = menu.GetCustomReference("WingsAccelerationText").GetComponent<Text>();
            accelerationBigMinus.onClick.AddListener(delegate { AddToEntry(ref Wings.WingsLevelModule.wingData.verticalForce, -1.0f, accelerationText); });
            accelerationMinus.onClick.AddListener(delegate { AddToEntry(ref Wings.WingsLevelModule.wingData.verticalForce, -0.1f, accelerationText); });
            accelerationPlus.onClick.AddListener(delegate { AddToEntry(ref Wings.WingsLevelModule.wingData.verticalForce, 0.1f, accelerationText); });
            accelerationBigPlus.onClick.AddListener(delegate { AddToEntry(ref Wings.WingsLevelModule.wingData.verticalForce, 1.0f, accelerationText); });
        }

        public static void UpdateMenu()
        {
            speedText.text = Wings.WingsLevelModule.wingData.horizontalSpeed.ToString("0.0#");
            accelerationText.text = Wings.WingsLevelModule.wingData.verticalForce.ToString("0.0#");
        }

        private static void SaveData()
        {
            File.WriteAllText(Application.streamingAssetsPath + Wings.WingsLevelModule.OPTIONS_FILE_PATH, JsonConvert.SerializeObject(Wings.WingsLevelModule.wingData));
            Debug.Log("Saving Wings settings!");
        }
    }
}
