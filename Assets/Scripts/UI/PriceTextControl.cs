using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace WiggleQuest
{
    public enum PriceType
    {
        Shop, Speed, Heart, Gold, ArmorS, ArmorM, ArmorL
    }
    public class PriceTextControl : MonoBehaviour
    {
        //참조
        private ShopUI shopUI;
        private RectTransform thisRectTransform;
        private GameObject panel;
        private TextMeshProUGUI priceText;

        [SerializeField]
        private Vector2 offset;

        //shopUI에서 받아옴
        public static Vector2 mousePoint;

        //인스펙터창에서 설정
        public PriceType priceType;

        private bool isDone = false;

        void Start()
        {
            shopUI = GameObject.Find("ShopUI").GetComponent<ShopUI>();
            thisRectTransform = GetComponent<RectTransform>();
            panel = this.transform.Find("PriceBg").gameObject; 

            priceText = panel.GetComponentInChildren<TextMeshProUGUI>();
            //안보이게 시작
            panel.SetActive(false);
        }

        void Update()
        {
            //마우스 위치가 이 버튼의 안쪽이라면 가격 보이게 하기
            if (CheckMouse() == true)
            {
                if (panel.activeInHierarchy == false)
                {
                    //가격 보이게 하기
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

            //팔렸다면 return
            if (isDone == true)
            {
                priceText.text = "Sold Out";
                return;
            }

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

                case PriceType.ArmorS: //0레벨 > 1레벨
                    if (Worm.DefLv > 0) 
                    {
                        this.GetComponent<Button>().interactable = false;
                        isDone = true;
                        break;
                    }
                    priceText.text = shopUI.PriceDef[0].ToString() + "G";
                    break;           
                    
                case PriceType.ArmorM: //1레벨 > 2레벨
                    if (Worm.DefLv > 1)
                    {
                        this.GetComponent<Button>().interactable = false;
                        isDone = true;
                        break;
                    }
                    else if 
                        (Worm.DefLv == 1)  this.GetComponent<Button>().interactable = true; 

                    priceText.text = shopUI.PriceDef[1].ToString() + "G";
                    break;              
                    
                case PriceType.ArmorL:  //2레벨 > 3레벨
                    if (shopUI.PriceDef.Length == Worm.DefLv)
                    {
                        this.GetComponent<Button>().interactable = false;
                        isDone = true;
                        break;
                    }
                    else if
                        (Worm.DefLv == 2) this.GetComponent<Button>().interactable = true;
                    priceText.text = shopUI.PriceDef[2].ToString() + "G";
                    break;
            }

            if (isDone == true)
            {
                priceText.text = "Sold Out";
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
    }
}