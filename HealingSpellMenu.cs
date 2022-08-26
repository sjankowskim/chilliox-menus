using Newtonsoft.Json;
using System.IO;
using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace ChillioXMenus
{
    class HealingSpellMenu : ModMenu
    {
        private static Toggle aoeFXtoggle;
        private static Dropdown typeDropdown;
        private static Button saveButton;

        private static Button healAmtBigMinus, healAmtMinus, healAmtPlus, healAmtBigPlus;
        private static Button minChargeMinus, minChargePlus;
        private static Button imbueHealBigMinus, imbueHealMinus, imbueHealPlus, imbueHealBigPlus;
        private static Button gripThreshMinus, gripThreshPlus;
        private static Button smashDistMinus, smashDistPlus;
        private static Button smashVelMinus, smashVelPlus;
        private static Button hpsBigMinus, hpsMinus, hpsPlus, hpsBigPlus;
        private static Button mdpsBigMinus, mdpsMinus, mdpsPlus, mdpsBigPlus;

        private static Text healAmtText;
        private static Text minChargeText;
        private static Text imbueHealText;
        private static Text gripThreshText;
        private static Text smashDistText;
        private static Text smashVelText;
        private static Text hpsText;
        private static Text mdpsText;

        public static void InitMenu(Menu menu)
        {
            // Heal Type Dropdown
            typeDropdown = menu.GetCustomReference("HealTypeDropdown").GetComponent<Dropdown>();
            typeDropdown.onValueChanged.AddListener(HealTypeChange);

            // AOE FX Toggle
            aoeFXtoggle = menu.GetCustomReference("AOEfxButton").GetComponent<Toggle>();
            aoeFXtoggle.onValueChanged.AddListener(delegate { ToggleChange(aoeFXtoggle.isOn, ref HealingSpell.HealingSpell.healingOptions.useAOEfx); });

            // Save settings Button
            saveButton = menu.GetCustomReference("HealSaveButton").GetComponent<Button>();
            saveButton.onClick.AddListener(SaveData);

            // Heal amount
            healAmtBigMinus = menu.GetCustomReference("HealAmtBigMinus").GetComponent<Button>();
            healAmtMinus = menu.GetCustomReference("HealAmtMinus").GetComponent<Button>();
            healAmtPlus = menu.GetCustomReference("HealAmtPlus").GetComponent<Button>();
            healAmtBigPlus = menu.GetCustomReference("HealAmtBigPlus").GetComponent<Button>();
            healAmtText = menu.GetCustomReference("HealAmtText").GetComponent<Text>();
            healAmtBigMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.healAmount, -1.0f, healAmtText); });
            healAmtMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.healAmount, -0.1f, healAmtText); });
            healAmtPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.healAmount, 0.1f, healAmtText); });
            healAmtBigPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.healAmount, 1.0f, healAmtText); });

            // Minimum charge
            minChargeMinus = menu.GetCustomReference("MinChargeMinus").GetComponent<Button>();
            minChargePlus = menu.GetCustomReference("MinChargePlus").GetComponent<Button>();
            minChargeText = menu.GetCustomReference("MinChargeText").GetComponent<Text>();
            minChargeMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.minimumChargeForHeal, -0.1f, minChargeText); });
            minChargePlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.minimumChargeForHeal, 0.1f, minChargeText); });

            // Imbue Heal
            imbueHealBigMinus = menu.GetCustomReference("ImbueHealBigMinus").GetComponent<Button>();
            imbueHealMinus = menu.GetCustomReference("ImbueHealMinus").GetComponent<Button>();
            imbueHealPlus = menu.GetCustomReference("ImbueHealPlus").GetComponent<Button>();
            imbueHealBigPlus = menu.GetCustomReference("ImbueHealBigPlus").GetComponent<Button>();
            imbueHealText = menu.GetCustomReference("ImbueHealText").GetComponent<Text>();
            imbueHealBigMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.imbueHealOnKill, -1.0f, imbueHealText); });
            imbueHealMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.imbueHealOnKill, -0.1f, imbueHealText); });
            imbueHealPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.imbueHealOnKill, 0.1f, imbueHealText); });
            imbueHealBigPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.imbueHealOnKill, 1.0f, imbueHealText); });

            // Grip Threshold
            gripThreshMinus = menu.GetCustomReference("GripThreshMinus").GetComponent<Button>();
            gripThreshPlus = menu.GetCustomReference("GripThreshPlus").GetComponent<Button>();
            gripThreshText = menu.GetCustomReference("GripThreshText").GetComponent<Text>();
            gripThreshMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.gripThreshold, -0.1f, gripThreshText); });
            gripThreshPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.gripThreshold, 0.1f, gripThreshText); });

            // Smash Distance
            smashDistMinus = menu.GetCustomReference("SmashDistMinus").GetComponent<Button>();
            smashDistPlus = menu.GetCustomReference("SmashDistPlus").GetComponent<Button>();
            smashDistText = menu.GetCustomReference("SmashDistText").GetComponent<Text>();
            smashDistMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.smashDistance, -0.01f, smashDistText); });
            smashDistPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.smashDistance, 0.01f, smashDistText); });

            // Smash Velocity
            smashVelMinus = menu.GetCustomReference("SmashVelMinus").GetComponent<Button>();
            smashVelPlus = menu.GetCustomReference("SmashVelPlus").GetComponent<Button>();
            smashVelText = menu.GetCustomReference("SmashVelText").GetComponent<Text>();
            smashVelMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.smashVelocity, -0.01f, smashVelText); });
            smashVelPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.smashVelocity, 0.01f, smashVelText); });

            // Health per Second
            hpsBigMinus = menu.GetCustomReference("HPSBigMinus").GetComponent<Button>();
            hpsMinus = menu.GetCustomReference("HPSMinus").GetComponent<Button>();
            hpsPlus = menu.GetCustomReference("HPSPlus").GetComponent<Button>();
            hpsBigPlus = menu.GetCustomReference("HPSBigPlus").GetComponent<Button>();
            hpsText = menu.GetCustomReference("HPSText").GetComponent<Text>();
            hpsBigMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.healthPerSecond, -1.0f, hpsText); });
            hpsMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.healthPerSecond, -0.1f, hpsText); });
            hpsPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.healthPerSecond, 0.1f, hpsText); });
            hpsBigPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.healthPerSecond, 1.0f, hpsText); });

            // Mana Drain per Second
            mdpsBigMinus = menu.GetCustomReference("MDPSBigMinus").GetComponent<Button>();
            mdpsMinus = menu.GetCustomReference("MDPSMinus").GetComponent<Button>();
            mdpsPlus = menu.GetCustomReference("MDPSPlus").GetComponent<Button>();
            mdpsBigPlus = menu.GetCustomReference("MDPSBigPlus").GetComponent<Button>();
            mdpsText = menu.GetCustomReference("MDPSText").GetComponent<Text>();
            mdpsBigMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.manaDrainPerSecond, -1.0f, mdpsText); });
            mdpsMinus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.manaDrainPerSecond, -0.1f, mdpsText); });
            mdpsPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.manaDrainPerSecond, 0.1f, mdpsText); });
            mdpsBigPlus.onClick.AddListener(delegate { AddToEntry(ref HealingSpell.HealingSpell.healingOptions.manaDrainPerSecond, 1.0f, mdpsText); });
        }

        public static void UpdateMenu()
        {
            typeDropdown.value = (int)HealingSpell.HealingSpell.healingOptions.healTypeEnum;
            aoeFXtoggle.isOn = HealingSpell.HealingSpell.healingOptions.useAOEfx;
            healAmtText.text = HealingSpell.HealingSpell.healingOptions.healAmount.ToString("0.0#");
            minChargeText.text = HealingSpell.HealingSpell.healingOptions.minimumChargeForHeal.ToString("0.0#");
            imbueHealText.text = HealingSpell.HealingSpell.healingOptions.imbueHealOnKill.ToString("0.0#");
            gripThreshText.text = HealingSpell.HealingSpell.healingOptions.gripThreshold.ToString("0.0#");
            smashDistText.text = HealingSpell.HealingSpell.healingOptions.smashDistance.ToString("0.0#");
            smashVelText.text = HealingSpell.HealingSpell.healingOptions.smashVelocity.ToString("0.0#");
            hpsText.text = HealingSpell.HealingSpell.healingOptions.healthPerSecond.ToString("0.0#");
            mdpsText.text = HealingSpell.HealingSpell.healingOptions.manaDrainPerSecond.ToString("0.0#");
        }

        private static void HealTypeChange(int newType)
        {
            HealingSpell.HealingSpell.healingOptions.healTypeEnum = (HealingSpell.HealType)newType;
        }

        private static void SaveData()
        {
            File.WriteAllText(Application.streamingAssetsPath + HealingSpell.HealingSpell.OPTIONS_FILE_PATH, JsonConvert.SerializeObject(HealingSpell.HealingSpell.healingOptions));
            Debug.Log("Saving Healing Spell settings!");
        }
    }
}
