using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WiggleQuest
{
    public class GaugeLevelConnect : MonoBehaviour
    {
        //이미지만 나오게 하면됨 / 삭제는 X

        //갑옷
        public List<Button> armorImage = new List<Button>(3); //갑옷은 버튼 ins어쩌구 그걸루

        //나머지 //처음엔 false해놧다가 하나씩 true로 변경
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

        private int defLv    = 0;

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

            if (Worm.DefLv != defLv)
            {
                defLv = Worm.DefLv;
                for(int i = 0; i < armorImage.Count; i++)
                {
                    armorImage[i].interactable = false;
                    if (i == defLv) 
                    {
                        armorImage[i].interactable = true;
                    }
                }
            }

        }

        void AddGaugeImage(Image[] images, ref int level)
        {
            level++;
            images[(level - 1)].enabled = true;
        }
    }
}