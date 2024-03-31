using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WiggleQuest
{
    public class GaugeLevelConnect : MonoBehaviour
    {
        //갑옷 뺀 나머지 //처음엔 false해놧다가 하나씩 true로 변경
        public GameObject speedImGroup;
        public GameObject goldImGroup;
        public GameObject healthImGroup;

        private Image[] speedIms = new Image[10];
        private Image[] goldIms  = new Image[10];
        private Image[] heartIms = new Image[10];

        //이 스크립트가 알고 있는 레벨만 저장
        private int speedLv  = 0;
        private int goldLv   = 0;
        private int heartLv  = 0;

        void Start()
        {
            //참조
            speedIms = speedImGroup.GetComponentsInChildren<Image>();
            goldIms = goldImGroup.GetComponentsInChildren<Image>();
            heartIms = healthImGroup.GetComponentsInChildren<Image>();
        }

        void Update()
        {
            if (Worm.SpeedLv > speedLv)
            {
                AddGaugeImage(speedIms, ref speedLv);
            }
            if (Worm.GoldLv > goldLv)
            {
                AddGaugeImage(goldIms, ref goldLv);
            }
            if (Worm.HeartLv > heartLv)
            {
                AddGaugeImage(heartIms, ref heartLv);
            }
        }

        void AddGaugeImage(Image[] images, ref int level)
        {
            level++;
            images[(level - 1)].enabled = true;
        }
    }
}