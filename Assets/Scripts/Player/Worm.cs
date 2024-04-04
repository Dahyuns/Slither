using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WiggleQuest
{
    //추가구현
    //이동 제한 (꼬리쪽으로 못가게)
    //스피드와 방어력 적용


    // 추가확률 종류
    public enum AddPercent
    {
        Gold,   //골드  
        Heart,  //먹이
        Speed,  //속도  
        Def     //방어력
    }

    public class Worm : MonoBehaviour
    {
        //지렁이 스탯
        [SerializeField] private static float heart;                       //목숨
        [SerializeField] private static int gold;                          //골드
        [SerializeField] private static float speed;                       //속도
        [SerializeField] private static float def;                         //방어력
        //  ㄴ읽기전용
        public static float Heart { get { return heart; } }
        public static int Gold { get { return gold; } }
        public static float Speed { get { return speed; } }
        public static float Def { get { return def; } }

        //머리위에 표시용 //레벨 = 목숨 반내림값
        public static int Level { get { return (int)heart; } }

        //*--Lv만큼 추가 획득
        private static int heartAddLv = 0;                   //먹이   추가 획득Lv
        private static int goldAddLv = 0;                    //골드   추가 획득Lv
        private static int speedAddLv = 0;                   //속도   추가 획득Lv
        private static int defAddLv = 0;                     //방어력 추가 획득Lv
        //  ㄴ읽기전용 - 상점UI에 가져가서 레벨 별 이미지 세팅해야함
        public static int HeartLv { get { return heartAddLv; } }
        public static int GoldLv { get { return goldAddLv; } }
        public static int SpeedLv { get { return speedAddLv; } }
        public static int DefLv { get { return defAddLv; } }

        //--%만큼 추가 획득 <고정> //밸런스조절부분
        private const float heartAddP   = 0.2f;                   //먹이   --%만큼 추가 획득
        private const float goldAddP    = 1f;                     //골드   --%만큼 추가 획득
        private const float speedAddP   = 0.7f;                   //속도   --%만큼 추가 획득
        private const float defAddP     = 0.2f;                   //방어력 --%만큼 추가 획득

        //시작 스탯 > 초기화용
        private int startHeart = 3;
        private int startGold = 10;
        private float startSpeed = 4f;
        private float startDef = 0;

        //이동
        Vector3 moveDir;
        private Vector3 beforeLocate;

        //참조
        private ShopUI shopUI;
        private StatusUI statusUI;
        //참조용
        public static bool isWormMoving = false;
        public static bool isWormDead = false;

        private void Start()
        {
            //초기화
            gold = startGold;
            heart = startHeart;
            speed = startSpeed;
            def = startDef;

            //참조
            shopUI = GameObject.Find("ShopUI")?.GetComponent<ShopUI>();
            statusUI = GameObject.Find("StatusUI")?.GetComponent<StatusUI>();
        }

        private void Update()
        {
            //죽음처리
            if (heart < 1)
            {
                isWormDead = true;
                return;
            }

            //이동
            if (moveDir != Vector3.zero)
            {
                transform.Translate(moveDir.normalized * Time.deltaTime * Speed, Space.World);
                isWormMoving = true;
            }
            else
            {
                isWormMoving = false;
                beforeLocate = this.transform.position;
            }

            #region [치트키] : M , LeftShift, LeftControl 
            //골드 up
            if (Input.GetKeyDown(KeyCode.M)) gold += 2000;
            //꼬리 개수 up
            if (Input.GetKeyDown(KeyCode.LeftShift)) heart++;
            //꼬리 개수 down
            if (Input.GetKeyDown(KeyCode.LeftControl)) heart--;
            #endregion
        }

        //Gold, Heart, Def중 선택, % 레벨 추가     
        public void AddLv(AddPercent what)
        {
            switch (what)
            {
                //먹이
                case AddPercent.Heart:
                    if (heartAddLv < shopUI.PriceHeart.Length)
                    {
                        heartAddLv++;
                    }
                    break;

                //골드
                case AddPercent.Gold:
                    if (goldAddLv < shopUI.PriceGold.Length)
                    {
                        goldAddLv++;
                    }
                    break;

                //속도
                case AddPercent.Speed:
                    if (speedAddLv < shopUI.PriceSpeed.Length)
                    {
                        speedAddLv++;
                        AddSpeed(); //적용
                    }
                    break;

                //방어력
                case AddPercent.Def:
                    if (defAddLv < shopUI.PriceDef.Length)
                    {
                        defAddLv++;
                    }
                    break;
            }
        }

        #region + , - (획득 사용 차감)
        //[ float 에서 int 로 변환할때 기본적으로 소수점 뒷자리를 버림을 한다. ]

        // 목숨 추가 - 먹이획득 => 원래 갖고있는 목숨 += 얻은 먹이 + 얻은 먹이 * (goldAddLv * 00%)
        public void AddHeart(float value)
        {
            float tmp = value + (value * heartAddLv * heartAddP);
            heart += tmp;

            string str = "+" + tmp.ToString("F1");
            statusUI.StartCoroutine(statusUI.DrawAddText(AddType.Heart, str));
        }

        //      감소 - 장애물 부딪힘 => 방어력 적용!
        public void SubtractHeart(float value)
        {
            float tmp = value - (value * defAddLv * defAddP);
            if (heart - tmp <= 0)
            {
                heart = 0;
                //죽음처리
                DeadWorm();
                return;
            }
            heart -= tmp;

            string str = "-" + tmp.ToString("F1");
            statusUI.StartCoroutine(statusUI.DrawAddText(AddType.Heart, str));
        }

        // 골드 추가 - 골드획득 => 원래 들고있는 골드 += 얻은 골드 + 얻은 골드 * (goldAddLv * 00%) 
        public void AddGold(int value)
        {
            int tmp = value + (int)(value * goldAddLv * goldAddP);
            gold += tmp;

            string str = "+" + tmp.ToString();
            statusUI.StartCoroutine(statusUI.DrawAddText(AddType.Gold, str));
        }

        //      감소 - 상점구매
        public bool SubtractGold(int value)
        {
            if (gold < value)
            {
                Debug.Log("돈부족! 구매불가능");
                return false;
            }
            else // gold >= value 
            {
                gold -= value;
                return true;
            }
        }

        // 속도 증가 - 적용
        public void AddSpeed()
        {
            speed += speedAddP;
        }
        #endregion

        public float MoveDis()
        {
            return (beforeLocate - transform.position).magnitude;
        }

        //Death 처리 : 속도 0만들기
        public void DeadWorm()
        {
            speed = 0;
            isWormMoving = false;
        }

        void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            if (input != null)
            {
                moveDir = new Vector3(input.x, 0, input.y);
            }
        }
    }
}