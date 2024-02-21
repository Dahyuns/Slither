using UnityEngine;
using TMPro;

namespace WiggleQuest
{
    public class StatusUI : MonoBehaviour
    {
        public TextMeshProUGUI Leveltext;
        public TextMeshProUGUI Goldtext;

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
    }
}