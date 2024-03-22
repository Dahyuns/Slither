using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace WiggleQuest
{
    //추가구현
    //돈있는지 확인

    public class ShopUI : MonoBehaviour
    {
        private static ShopUI instance;
        public static ShopUI Instance { get { return instance; } }

        //참조
        [SerializeField] private Worm worm;
        [SerializeField] private GameObject ShopButton; //상점 버튼 [ Shop ]
        [SerializeField] private GameObject ShopMenu; //상점 UI
        [SerializeField] private GameObject LockImage;

        //가격 - 밸런스조절부분
        #region Shop Price
        [SerializeField] private int PriceShop = 50;

        [SerializeField] private int[] priceHeart = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100 };
        [SerializeField] private int[] priceGold  = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100 };
        [SerializeField] private int[] priceSpeed = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100 };
        [SerializeField] private int[] priceDef   = { 100, 200, 300 };

        public int lengthPriceHeart { get { return priceHeart.Length; } }
        public int lengthPriceGold { get { return priceGold.Length; } }
        public int lengthPriceSpeed { get { return priceSpeed.Length; } }
        public int lengthPriceDef { get { return priceDef.Length; } }
        #endregion

        private bool shopButtonOn = true;

        private void Update()
        {
            //죽으면 버튼 없애고 RETURN
            if (Worm.isWormDead)
            {
                ShopButton.GetComponent<Button>().interactable = false;
                return;
            }

            #region Shop inout

            //상점 들어갈 돈이 없을때
            if (Worm.Gold < PriceShop && shopButtonOn == true)
            {
                //버튼 끄기, LOCK이미지 켜기
                ShopButton.GetComponent<Button>().interactable = false;
                LockImage.SetActive(true);
                shopButtonOn = false;
            } 
            
            // 있을때
            else if (Worm.Gold >= PriceShop && shopButtonOn == false)
            {
                //버튼 켜기, LOCK이미지 끄기
                ShopButton.GetComponent<Button>().interactable = true;
                LockImage.SetActive(false);
                shopButtonOn = true;
            }


            //상점 안에 들어갔을때, ESC키로 밖으로 나옴
            if (isInShop)
            {
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    //필드로 가는 매서드
                    GotoPlayGround();
                }
            }
            #endregion
        }

        #region Shop inout
        // 타이틀 <-> ( 필드 <-> 상점 )
        //필드입장 : 게임시작        ㅡ 타이틀에서
        //필드퇴장 : 게임오버        ㅡ 타이틀로
        //상점입장 : 상점 버튼 클릭  ㅡ 필드에서
        //상점퇴장 : ESC            ㅡ 필드로

        //상점에 들어갔는지?
        private bool isInShop = false;

        //Back버튼 클릭시 및 ESC input시 //상점에서 필드로
        public void GotoPlayGround()
        {
            Time.timeScale = 1f; //다시 재생
            ShopStatusReverse();
        }

        //Shop버튼 클릭시 //필드에서 상점으로
        public void GotoShop()
        {
            if (worm.SubtractGold(PriceShop))
            {
                Time.timeScale = 0f; //일시정지
                ShopStatusReverse();
            }
            else
            {
                Debug.Log("상점진입불가능");
            }
        }

        //상점 진입 / 퇴장
        private void ShopStatusReverse()
        {
            /*1 버튼으로 가져왔을때
            ShopButton.enabled = !(ShopButton.enabled);
            ShopButton.image.enabled = !(ShopButton.image.enabled);
            foreach (GameObject each in EveryInShop)
            {
                each.SetActive(isInShop);
            }*/
            //2 게임 오브젝트로 가져왔을때
            ShopButton.SetActive(isInShop);

            isInShop = !isInShop; //반대로 작업
            ShopMenu.SetActive(isInShop);
        }
        #endregion

        #region Purchase in Shop
        //먹이추가확률 구매
        public void PurchaseHeart()
        {
            //레벨이 업가능레벨보다 크거나 같으면 return
            if (Worm.HeartLv >= priceHeart.Length)
            {
                Debug.Log("!! HP 구매 불가");
                return;
            }
            //                         작으면 진행 

            //살 수 있는 돈이 있음
            if (worm.SubtractGold(priceHeart[Worm.HeartLv]))
            {
                worm.AddLv(AddPercent.Heart);
                Debug.Log(Worm.HeartLv.ToString() + "HP 구매");
                return;
            }
        }

        //골드추가확률 구매
        public void PurchaseGold()
        {
            //레벨이 업가능레벨보다 크거나 같으면 return
            if (Worm.GoldLv >= priceGold.Length)
            {
                Debug.Log("!! Gold 구매 불가");
                return;
            }

            //                         작으면 진행 
            if (worm.SubtractGold(priceGold[Worm.GoldLv]))
            {
                worm.AddLv(AddPercent.Gold);
                Debug.Log(Worm.GoldLv.ToString() + "Gold 구매");
                return;
            }
        }

        //스피드추가확률 구매
        public void PurchaseSpeed()
        {
            //레벨이 업가능레벨보다 크거나 같으면 return
            if (Worm.SpeedLv >= priceSpeed.Length)
            {
                Debug.Log("speed 구매불가");
                return;
            }
            //                         작으면 진행 
            if (worm.SubtractGold(priceSpeed[Worm.SpeedLv]))
            {
                worm.AddLv(AddPercent.Speed);
                Debug.Log(Worm.SpeedLv.ToString() + "스피드 구매");
                return;
            }
        }

        //방어력 구매
        public void PurchaseDef()
        {
            //레벨이 업가능레벨보다 크거나 같으면 return
            if (Worm.DefLv >= priceDef.Length)
            {
                Debug.Log("방어력 구매불가");
                return; 
            }
            //                         작으면 진행 
            //1,2,3레벨 갑옷 구분필요
            if (worm.SubtractGold(priceDef[Worm.DefLv]))
            {
                worm.AddLv(AddPercent.Def);
                Debug.Log(Worm.DefLv.ToString() + "방어력 구매");
                return;
            }
        }
        #endregion

        void OnPoint(InputValue value)
        {
            PriceTextControl.mousePoint = value.Get<Vector2>();
        }
    }
}
