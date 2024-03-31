using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WiggleQuest
{
    public class GaugeLevelConnect : MonoBehaviour
    {
        //���� �� ������ //ó���� false�؇J�ٰ� �ϳ��� true�� ����
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
        }

        void AddGaugeImage(Image[] images, ref int level)
        {
            level++;
            images[(level - 1)].enabled = true;
        }
    }
}