using UnityEngine;
using TMPro;

namespace WiggleQuest
{
    public enum PriceType
    {
        Shop, Speed, Heart, Gold, ArmorS, ArmorM, ArmorL
    }
    public class PriceTextControl : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
    {
        //�߰�����
        //�ؽ�Ʈ����

        //����
        private ShopUI shopUI;
        private RectTransform thisRectTransform;
        private GameObject panel;
        private TextMeshProUGUI priceText;

        [SerializeField]
        private Vector2 offset;

        //shopUI���� �޾ƿ�
        public static Vector2 mousePoint;

        //�ν�����â���� ����
        public PriceType priceType;

        private bool isDone = false;

        void Start()
        {
            shopUI = GameObject.Find("ShopUI").GetComponent<ShopUI>();
            thisRectTransform = GetComponent<RectTransform>();
            panel = this.transform.Find("PriceBg").gameObject; //GetComponentInChildren<GameObject>();

            priceText = panel.GetComponentInChildren<TextMeshProUGUI>();
            //�Ⱥ��̰� ����
            panel.SetActive(false);
        }

        void Update()
        {
            switch (priceType)
            {
                case PriceType.Shop:
                    priceText.text = shopUI.PriceShop.ToString() + "G";
                    break;


                case PriceType.Heart:
                    if (shopUI.PriceHeart.Length == Worm.HeartLv)
                    {
                        isDone = true;
                        break;
                    }
                    priceText.text = shopUI.PriceHeart[Worm.HeartLv].ToString() + "G";
                    break; 
                case PriceType.Gold:
                    if (shopUI.PriceGold.Length == Worm.GoldLv)
                    {
                        isDone = true;
                        break;
                    }
                    priceText.text = shopUI.PriceGold[Worm.GoldLv].ToString() + "G";
                    break;
                case PriceType.Speed:
                    if (shopUI.PriceSpeed.Length == Worm.SpeedLv)
                    {
                        isDone = true;
                        break;
                    }
                    priceText.text = shopUI.PriceSpeed[Worm.SpeedLv].ToString() + "G";
                    break; 


                case PriceType.ArmorS:
                    if (Worm.DefLv > 0)
                    {
                        isDone = true;
                        break;
                    }
                    priceText.text = shopUI.PriceDef[0].ToString() + "G";
                    break;                    
                case PriceType.ArmorM:
                    if (Worm.DefLv > 1)
                    {
                        isDone = true;
                        break;
                    }
                    priceText.text = shopUI.PriceDef[1].ToString() + "G";
                    break;                    
                case PriceType.ArmorL:
                    if (shopUI.PriceDef.Length == Worm.DefLv)
                    {
                        isDone = true;
                        break;
                    }
                    priceText.text = shopUI.PriceDef[2].ToString() + "G";
                    break;
            }

            if(isDone == true)
            {
                priceText.text = "Sold Out";
                isDone = false;
            }
            
            //���콺 ��ġ�� �� ��ư�� �����̶�� 
            if (CheckMouse() == true)
            {
                if (panel.activeInHierarchy == false)
                {
                    //�� ��ư�� ���� ���̰� �ϱ�
                    panel.SetActive(true);
                }
                SetPosPriceGroup();
            }
            else if (CheckMouse() == false)
            {
                if (panel.activeInHierarchy == true)
                {
                    panel.SetActive(false);
                }
            }
        }

        bool CheckMouse()
        {
            return RectTransformUtility.RectangleContainsScreenPoint(thisRectTransform, mousePoint);
        }

        void SetPosPriceGroup()
        {
            panel.transform.position = mousePoint + offset;
        }


        ////mouse in
        //���콺�� ��ư �ȿ� �ִٸ�
        //void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        //{
        //    Debug.Log($"IPointerEnter  {eventData.ToString()}");
        //}
        //
        ////mouse out
        //void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        //{
        //    isInButton = false;
        //    Debug.Log($"IPointerExit  {eventData.ToString()}");
        //}

    }
}