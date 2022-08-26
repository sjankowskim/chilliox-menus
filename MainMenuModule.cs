using System.Collections.Generic;
using System.IO;
using ThunderRoad;
using UnityEngine;
using UnityEngine.UI;

namespace ChillioXMenus
{
    public class MainMenuModule : MenuModule
    {
        private Dictionary<string, bool> modList = new Dictionary<string, bool>()
        {
            {"Healing Spell", false },
            {"Quicksilver", false },
            {"Wings", false },
            {"Dark Souls Parry", false },
            {"Fast Respawn", false }
        };
        private GameObject currMenu;
        private GameObject healingMenu;
        private GameObject quicksilverMenu;
        private GameObject wingsMenu;
        private GameObject darkSoulsParryMenu;
        private GameObject fastRespawnMenu;
        private Dropdown modDropdown;

        public override void Init(MenuData menuData, Menu menu)
        {
            base.Init(menuData, menu);
            if (Directory.Exists(Application.streamingAssetsPath + "\\Mods\\HealingSpell")) 
            {
                healingMenu = menu.GetCustomReference("HealingSpellMenu").gameObject;
                HealingSpellMenu.InitMenu(menu);
                modList["Healing Spell"] = true;
            } else
                Debug.LogWarning("(Chillio X Menus) Healing Spell not found! Skipping...");

            if (Directory.Exists(Application.streamingAssetsPath + "\\Mods\\Quicksilver"))
            {
                quicksilverMenu = menu.GetCustomReference("QuicksilverMenu").gameObject;
                QuicksilverMenu.InitMenu(menu);
                modList["Quicksilver"] = true;
            } else
                Debug.LogWarning("(Chillio X Menus) Quicksilver not found! Skipping...");


            if (Directory.Exists(Application.streamingAssetsPath + "\\Mods\\Wings"))
            {
                wingsMenu = menu.GetCustomReference("WingsMenu").gameObject;
                WingsMenu.InitMenu(menu);
                modList["Wings"] = true;
            }
            else
                Debug.LogWarning("(Chillio X Menus) Wings not found! Skipping...");

            if (Directory.Exists(Application.streamingAssetsPath + "\\Mods\\DarkSoulsParry"))
            {
                darkSoulsParryMenu = menu.GetCustomReference("DarkSoulsParryMenu").gameObject;
                DarkSoulsParryMenu.InitMenu(menu);
                modList["Dark Souls Parry"] = true;
            }
            else
                Debug.LogWarning("(Chillio X Menus) Dark Souls Parry not found! Skipping...");


            if (Directory.Exists(Application.streamingAssetsPath + "\\Mods\\FastRespawn"))
            {
                fastRespawnMenu = menu.GetCustomReference("FastRespawnMenu").gameObject;
                FastRespawnMenu.InitMenu(menu);
                modList["Fast Respawn"] = true;
            }
            else
                Debug.LogWarning("(Chillio X Menus) Fast Respawn not found! Skipping...");

            modDropdown = menu.GetCustomReference("ModDropdown").GetComponent<Dropdown>();
            List<string> temp = new List<string>();
            foreach (KeyValuePair<string, bool> pair in modList)
            {
                if (pair.Value)
                {
                    temp.Add(pair.Key);
                }
            }
            modDropdown.AddOptions(temp);
            modDropdown.onValueChanged.AddListener(ModChange);
        }

        private void ModChange(int index)
        {
            currMenu?.SetActive(false);
            switch (modDropdown.options[index].text)
            {
                case "Healing Spell":
                    healingMenu.SetActive(true);
                    currMenu = healingMenu;
                    HealingSpellMenu.UpdateMenu();
                    break;
                case "Quicksilver":
                    quicksilverMenu.SetActive(true);
                    currMenu = quicksilverMenu;
                    QuicksilverMenu.UpdateMenu();
                    break;
                case "Wings":
                    wingsMenu.SetActive(true);
                    currMenu = wingsMenu;
                    WingsMenu.UpdateMenu();
                    break;
                case "Dark Souls Parry":
                    darkSoulsParryMenu.SetActive(true);
                    currMenu = darkSoulsParryMenu;
                    DarkSoulsParryMenu.UpdateMenu();
                    break;
                case "Fast Respawn":
                    fastRespawnMenu.SetActive(true);
                    currMenu = fastRespawnMenu;
                    FastRespawnMenu.UpdateMenu();
                    break;
                default:
                    currMenu = null;
                    break;
            }
        }
    }
}
