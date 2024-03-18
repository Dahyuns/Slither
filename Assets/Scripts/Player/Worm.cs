using UnityEngine;
using UnityEngine.InputSystem;

namespace WiggleQuest
{
    //추가구현
    //이동 제한 (꼬리쪽으로 못가게)


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
        public static float Heart   { get { return heart; } }
        public static int   Gold    { get { return gold; } }
        public static float Speed   { get { return speed; } }
        public static float Def     { get { return def; } }

        //머리위에 표시용 //레벨 = 목숨 반내림값
        public static int Level     { get { return (int)heart;} } 


        //*--Lv만큼 추가 획득
        private static int heartAddLv = 10;                   //먹이   추가 획득Lv
        private static int goldAddLv = 10;                    //골드   추가 획득Lv
        private static int speedAddLv = 10;                   //속도   추가 획득Lv
        private static int defAddLv = 10;                     //방어력 추가 획득Lv
        //  ㄴ읽기전용
        public static int HeartLv { get { return heartAddLv; } }
        public static int GoldLv { get { return goldAddLv; } }
        public static int SpeedLv { get { return defAddLv; } }
        public static int DefLv { get { return speedAddLv; } }


        //--%만큼 추가 획득 <고정> //밸런스조절부분
        private const float heartAddP = 0;                    //먹이   --%만큼 추가 획득
        private const float goldAddP  = 0;                    //골드   --%만큼 추가 획득
        private const float speedAddP = 0;                    //속도   --%만큼 추가 획득
        private const float defAddP   = 0;                    //방어력 --%만큼 추가 획득

        //시작 스탯 > 초기화용
        private int startHeart = 3;
        private int startGold = 10;
        private float startSpeed = 5f;
        private float startDef = 0;

        //이동
        Vector3 moveDir;
        private Vector3 beforeLocate;

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

            //[치트키]
            //골드 up
            if (Input.GetKey(KeyCode.M))                            gold += 10000;
            //꼬리 개수 up
            if (Input.GetKeyDown(KeyCode.LeftShift))                heart++;
            //꼬리 개수 down
            if (Input.GetKeyDown(KeyCode.LeftControl))              heart--;

        }

        //Gold, Heart, Def중 선택, %추가     
        public void AddLv(AddPercent what)
        {
            switch (what)
            {
                //골드레벨업
                case AddPercent.Gold:
                    goldAddLv += 1;
                    break;

                //먹이
                case AddPercent.Heart:
                    heartAddLv += 1;
                    break;

                //방어력
                case AddPercent.Def:
                    defAddLv += 1;
                    break;

                //속도
                case AddPercent.Speed:
                    speedAddLv += 1;
                    break;
            }
        }

        #region + , - (획득 사용 차감)
        //[ float 에서 int 로 변환할때 기본적으로 소수점 뒷자리를 버림을 한다. ]

        // 골드 추가 - 골드획득 => 원래 들고있는 골드 += 얻은 골드 + 얻은 골드 * (goldAddLv * 00%) 
        public void AddGold(int value)
        {
            gold += value + (int)(value * goldAddLv * goldAddP);
            Debug.Log(gold);
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

        // 목숨 추가 - 먹이획득 => 원래 갖고있는 목숨 += 얻은 먹이 + 얻은 먹이 * (goldAddLv * 00%)
        public void AddHeart(float value)
        {
            heart += value + (int)(value * heartAddLv * heartAddP);
        }

        //      감소 - 장애물 부딪힘 => 방어력!!!
        public void SubtractHeart(float value)
        {
            float newvalue = heart - value - (int)(value * ((heartAddLv * heartAddP) / 100));
            if (newvalue < 0)
            {
                heart = 0;
                DeadWorm();
                return;
            }
            heart = newvalue ;
        }

        // 속도 증가
        public void AddSpeed(float value)
        {
            speed += value + (int)(value * speedAddLv * speedAddP);
        }

        // 방어력 추가 (퍼센트)
        public void AddDef(float value)
        {
            def += value + (int)(value * defAddLv * defAddP);
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