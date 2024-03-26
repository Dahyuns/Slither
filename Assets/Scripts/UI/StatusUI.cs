using UnityEngine;
using TMPro;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

namespace WiggleQuest
{
    public enum AddType
    {
        Gold, Heart
    }
    public class StatusUI : MonoBehaviour
    {
        public TextMeshProUGUI Leveltext;
        public TextMeshProUGUI Goldtext;
        public TextMeshProUGUI AddSubText;

        private void Awake()
        {
            AddSubText.text = string.Empty;
        }

        private void Update ()
        {
            DrawLevelText();
            DrawGoldText();
        }

        private void DrawLevelText()
        {
            Leveltext.text = Worm.Level.ToString();
        }

        private void DrawGoldText()
        {
            Goldtext.text = Worm.Gold.ToString() + "G";
        }


        public IEnumerator DrawAddText(AddType type, string value)
        {
            switch (type)
            {
                case AddType.Heart:

                    AddSubText.text += $"{value}¢½\n";
                    break; 

                case AddType.Gold:
                    AddSubText.text += $"{value}G\n";

                    break;
            }
            yield return new WaitForSeconds(2f);

            AddSubText.text = "";
        }
    }
}