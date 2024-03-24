using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WiggleQuest
{
    public class GaugeLevelConnect : MonoBehaviour
    {
        //�̹����� ������ �ϸ�� / ������ X

        //����
        public List<Button> armorImage = new List<Button>(3); //������ ��ư ins��¼�� �װɷ�

        //������ //ó���� false�؇J�ٰ� �ϳ��� true�� ����
        public GameObject speedImGroup;
        public GameObject goldImGroup;
        public GameObject healthImGroup;

        private Image[] speedIms = new Image[10];
        private Image[] goldIms  = new Image[10];
        private Image[] heartIms = new Image[10];

        //�� ��ũ��Ʈ�� �˰� �ִ� ������ ����
        private int speedLv  = 0;
        private int goldLv   = 0;
        private int heartLv  = 0;

        private int defLv    = 0;

        void Start()
        {
            //����
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