using Newtonsoft.Json;
using System.IO;
using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace ChillioXMenus
{
    class DarkSoulsParryMenu : ModMenu
    {
        private static Button saveButton;
        private static Button ds1DurationBigMinus, ds1DurationMinus, ds1DurationPlus, ds1DurationBigPlus;
        private static Text ds1DurationText;
        private static Button ds1SlowBigMinus, ds1SlowMinus, ds1SlowPlus, ds1SlowBigPlus;
        private static Text ds1SlowText;
        private static Toggle ds2ParryToggle;
        private static Button ds2DelayBigMinus, ds2DelayMinus, ds2DelayPlus, ds2DelayBigPlus;
        private static Text ds2DelayText;
        private static Button velocityBigMinus, velocityMinus, velocityPlus, velocityBigPlus;
        private static Text velocityText;
        private static Toggle tieredParryToggle;
        private static Button tierBigMinus, tierMinus, tierPlus, tierBigPlus;
        private static Text tierText;

        public static void InitMenu(Menu menu)
        {
            saveButton = menu.GetCustomReference("QuicksilverSaveButton").GetComponent<Button>();
            saveButton.onClick.AddListener(SaveData);

            ds1DurationBigMinus = menu.GetCustomReference("ParryDS1DurationBigMinus").GetComponent<Button>();
            ds1DurationMinus = menu.GetCustomReference("ParryDS1DurationMinus").GetComponent<Button>();
            ds1DurationPlus = menu.GetCustomReference("ParryDS1DurationPlus").GetComponent<Button>();
            ds1DurationBigPlus = menu.GetCustomReference("ParryDS1DurationBigPlus").GetComponent<Button>();
            ds1DurationText = menu.GetCustomReference("ParryDS1DurationText").GetComponent<Text>();
            ds1DurationBigMinus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds1ParryDuration, -1f, ds1DurationText); });
            ds1DurationMinus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds1ParryDuration, -0.1f, ds1DurationText); });
            ds1DurationPlus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds1ParryDuration, 0.1f, ds1DurationText); });
            ds1DurationBigPlus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds1ParryDuration, 1f, ds1DurationText); });

            ds1SlowBigMinus = menu.GetCustomReference("ParryDS1SlowBigMinus").GetComponent<Button>();
            ds1SlowMinus = menu.GetCustomReference("ParryDS1SlowMinus").GetComponent<Button>();
            ds1SlowPlus = menu.GetCustomReference("ParryDS1SlowPlus").GetComponent<Button>();
            ds1SlowBigPlus = menu.GetCustomReference("ParryDS1SlowBigPlus").GetComponent<Button>();
            ds1SlowText = menu.GetCustomReference("ParryDS1SlowText").GetComponent<Text>();
            ds1SlowBigMinus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds1ParrySlow, -0.1f, ds1SlowText); });
            ds1SlowMinus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds1ParrySlow, -0.01f, ds1SlowText); });
            ds1SlowPlus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds1ParrySlow, 0.01f, ds1SlowText); });
            ds1SlowBigPlus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds1ParrySlow, 0.1f, ds1SlowText); });

            ds2ParryToggle = menu.GetCustomReference("ParryDS2Toggle").GetComponent<Toggle>();
            ds2ParryToggle.onValueChanged.AddListener(delegate { ToggleChange(ds2ParryToggle.isOn, ref DarkSoulsParry.DarkSoulsParry.data.useDarkSouls2Parry); });

            ds2DelayBigMinus = menu.GetCustomReference("ParryDS2DelayBigMinus").GetComponent<Button>();
            ds2DelayMinus = menu.GetCustomReference("ParryDS2DelayMinus").GetComponent<Button>();
            ds2DelayPlus = menu.GetCustomReference("ParryDS2DelayPlus").GetComponent<Button>();
            ds2DelayBigPlus = menu.GetCustomReference("ParryDS2DelayBigPlus").GetComponent<Button>();
            ds2DelayText = menu.GetCustomReference("ParryDS2DelayText").GetComponent<Text>();
            ds2DelayBigMinus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds2DelayDuration, -0.1f, ds2DelayText); });
            ds2DelayMinus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds2DelayDuration, -0.01f, ds2DelayText); });
            ds2DelayPlus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds2DelayDuration, 0.01f, ds2DelayText); });
            ds2DelayBigPlus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.ds2DelayDuration, 0.1f, ds2DelayText); });

            velocityBigMinus = menu.GetCustomReference("ParryMinWepBigMinus").GetComponent<Button>();
            velocityMinus = menu.GetCustomReference("ParryMinWepMinus").GetComponent<Button>();
            velocityPlus = menu.GetCustomReference("ParryMinWepPlus").GetComponent<Button>();
            velocityBigPlus = menu.GetCustomReference("ParryMinWepBigPlus").GetComponent<Button>();
            velocityText = menu.GetCustomReference("ParryMinWepText").GetComponent<Text>();
            velocityBigMinus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.minWeaponVelocity, -1f, velocityText); });
            velocityMinus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.minWeaponVelocity, -0.1f, velocityText); });
            velocityPlus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.minWeaponVelocity, 0.1f, velocityText); });
            velocityBigPlus.onClick.AddListener(delegate { AddToEntry(ref DarkSoulsParry.DarkSoulsParry.data.minWeaponVelocity, 1f, velocityText); });

            tieredParryToggle = menu.GetCustomReference("ParryTierToggle").GetComponent<Toggle>();
            tieredParryToggle.onValueChanged.AddListener(delegate { ToggleChange(tieredParryToggle.isOn, ref DarkSoulsParry.DarkSoulsParry.data.useTieredParry); });

            tierBigMinus = menu.GetCustomReference("ParryTierVelocityBigMinus").GetComponent<Button>();
            tierMinus = menu.GetCustomReference("ParryTierVelocityMinus").GetComponent<Button>();
            tierPlus = menu.GetCustomReference("ParryTierVelocityPlus").GetComponent<Button>();
            tierBigPlus = menu.GetCustomReference("ParryTierVelocityBigPlus").GetComponent<Button>();
            tierText = menu.GetCustomReference("ParryTierVelocityText").GetComponent<Text>();
            tierBigMinus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.customTimescale, -1f, tierText); });
            tierMinus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.customTimescale, -0.1f, tierText); });
            tierPlus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.customTimescale, 0.1f, tierText); });
            tierBigPlus.onClick.AddListener(delegate { AddToEntry(ref Quicksilver.QuicksilverModule.data.customTimescale, 1f, tierText); });
        }

        public static void UpdateMenu()
        {
            ds1DurationText.text = DarkSoulsParry.DarkSoulsParry.data.ds1ParryDuration.ToString("0.0#");
            ds1SlowText.text = DarkSoulsParry.DarkSoulsParry.data.ds1ParrySlow.ToString("0.0#");
            ds2ParryToggle.isOn = DarkSoulsParry.DarkSoulsParry.data.useDarkSouls2Parry;
            ds2DelayText.text = DarkSoulsParry.DarkSoulsParry.data.ds2DelayDuration.ToString("0.0#");
            velocityText.text = DarkSoulsParry.DarkSoulsParry.data.minWeaponVelocity.ToString("0.0#");
            tieredParryToggle.isOn = DarkSoulsParry.DarkSoulsParry.data.useTieredParry;
            tierText.text = DarkSoulsParry.DarkSoulsParry.data.minTier2Velocity.ToString("0.0#");
        }

        private static void SaveData()
        {
            File.WriteAllText(Application.streamingAssetsPath + DarkSoulsParry.DarkSoulsParry.OPTIONS_FILE_PATH, JsonConvert.SerializeObject(DarkSoulsParry.DarkSoulsParry.data));
            Debug.Log("Saving Dark Souls Parry settings!");
        }
    }
}
