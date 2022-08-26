using UnityEngine.UI;

namespace ChillioXMenus
{
    abstract class ModMenu
    {
        public static void AddToText(float termToAdd, Text text)
        {
            text.text = (float.Parse(text.text) + termToAdd).ToString("0.0#");
        }

        public static void AddToEntry(ref float option, float termToAdd, Text text)
        {
            option += termToAdd;
            text.text = (float.Parse(text.text) + termToAdd).ToString("0.0#");
        }

        public static void ToggleChange(bool isOn, ref bool arg)
        {
            arg = isOn;
        }
    }
}
