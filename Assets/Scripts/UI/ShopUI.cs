using UnityEngine;
using UnityEngine.InputSystem;

namespace WiggleQuest
{
    //추가구현
    //돈있는지 확인

    public class ShopUI : MonoBehaviour
    {
        //참조
        public Worm worm;
        public GameObject ShopButton; //상점 버튼 [ Shop ]
        public GameObject ShopMenu; //상점 UI

        //가격 - 밸런스조절부분
        [SerializeField] private int[] priceHeart = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceGold  = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceSpeed = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
        [SerializeField] private int[] priceDef   = { 100, 200, 300 };

        //상점에 들어갔는지?
        private bool isInShop = false;

        // 타이틀 <-> ( 필드 <-> 상점 )
        //필드입장 : 게임시작        ㅡ 타이틀에서
        //필드퇴장 : 게임오버        ㅡ 타이틀로
        //상점입장 : 상점 버튼 클릭  ㅡ 필드에서
        //상점퇴장 : ESC            ㅡ 필드로
        private void Start()
        {
            //샵메뉴의 모든 오브젝트 가져오기?
            //EveryInShop = ShopMenu.GetComponentsInChildren<GameObject>(true);
        }
        private void Update()
        {
            //상점 안에 들어갔나?
            if (isInShop)
            {
                //상점 밖으로 나가고 싶다 - ESC
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    //필드로 가는 매서드
                    GotoPlayGround();
                }
            }
        }

        //Back버튼 클릭시 및 ESC input시 //상점에서 필드로
        public void GotoPlayGround()
        {
            Time.timeScale = 1f; //다시 재생
            ShopStatusReverse();
        }

        //Shop버튼 클릭시 //필드에서 상점으로
        public void GotoShop()
        {
            Time.timeScale = 0f; //일시정지
            ShopStatusReverse();
        }

        //상점 진입 / 퇴장
        private void ShopStatusReverse()
        {
            /*1 버튼으로 가져왔을때
            ShopButton.enabled = !(ShopButton.enabled);
            ShopButton.image.enabled = !(ShopButton.image.enabled);*/
            //2 게임 오브젝트로 가져왔을때
            ShopButton.SetActive(isInShop);

            isInShop = !isInShop; //반대로 작업
            ShopMenu.SetActive(isInShop);
            /*foreach (GameObject each in EveryInShop)
            {
                each.SetActive(isInShop);
            }*/
        }

        //먹이추가확률 구매
        public void PurchaseHeart()
        {
            if (worm.SubtractGold(priceHeart[0]))
            {
                worm.AddLv(AddPercent.Heart);
                Debug.Log("HP 구매");
            }
        }

        //골드추가확률 구매
        public void PurchaseGold()
        {
            if (worm.SubtractGold(priceGold[0]))
            {
                worm.AddLv(AddPercent.Gold);
                Debug.Log("Gold 구매");
            }
        }

        //스피드추가확률 구매
        public void PurchaseSpeed()
        {
            if (worm.SubtractGold(priceSpeed[0]))
            {
                worm.AddLv(AddPercent.Speed);
                Debug.Log("스피드 구매");
            }
        }

        //방어력 구매
        public void PurchaseDef()
        {
            //1,2,3레벨 갑옷 구분필요
            if (worm.SubtractGold(priceDef[0]))
            {
                worm.AddLv(AddPercent.Def);
                Debug.Log("방어력 구매");
            }
        }

        void OnPoint(InputValue value)
        {
            PriceTextControl.mousePoint = value.Get<Vector2>();
        }
    }
}
