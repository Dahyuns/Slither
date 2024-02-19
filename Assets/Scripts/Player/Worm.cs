using UnityEngine;
using UnityEngine.InputSystem;

namespace WiggleQuest
{
    // --% 만큼 추가 획득
    public enum AddPercent
    {
        Gold,   //골드  
        Heart,  //먹이
        Def,    //방어력
        Speed   //속도  
    }

    public class Worm : MonoBehaviour
    {
        //지렁이 스탯
        private static float heart;                       //목숨
        private static int gold;                          //골드
        private static float speed;                       //속도
        private static float def;                         //방어력

        //읽기전용
        public static float Heart   { get { return heart; } }
        public static int   Gold      { get { return gold; } }
        public static float Speed   { get { return speed; } }
        public static float Def     { get { return def; } }

        public static int Level     { get { return (int)heart;} } //머리위에 표시 //레벨 = 목숨 반내림값

        //*--Lv만큼 추가 획득
        private static float heartAddLv = 0;                   //먹이   추가 획득Lv
        private static float goldAddLv = 0;                    //골드   추가 획득Lv
        private static float defAddLv = 0;                     //방어력 추가 획득Lv
        private static float speedAddLv = 0;                   //속도   추가 획득Lv

        //--%만큼 추가 획득 <고정> //밸런스조절부분
        private float heartAddP = 0;                   //먹이   --%만큼 추가 획득
        private float goldAddP = 0;                    //골드   --%만큼 추가 획득
        private float defAddP = 0;                     //방어력 --%만큼 추가 획득
        private float speedAddP = 0;                   //속도   --%만큼 추가 획득

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

        //시작 스탯
        [SerializeField] private int startHeart = 3;
        [SerializeField] private int startGold = 0;
        [SerializeField] private float startSpeed = 10f;
        [SerializeField] private float startDef = 0;

        //움직임 벡터
        Vector3 moveDir;

        //다른 코드에 가져갈거
        public static bool isWormMoving = false;

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
            if (moveDir != Vector3.zero)
                transform.Translate(moveDir.normalized * Time.deltaTime * Speed, Space.World);

            //치트키
            if (Input.GetKey(KeyCode.M))
            {
                gold += 10000;
            }
        }

        //[ float 에서 int 로 변환할때 기본적으로 소수점 뒷자리를 버림을 한다. ]

        // 골드 추가 - 골드획득 => 원래 들고있는 골드 += 얻은 골드 + 얻은 골드 * (goldAddLv * 00%) 
        public void AddGold(int value)
        {
            gold += value + (int)(value * goldAddLv * goldAddP);
        }

        //      감소 - 상점구매
        public void SubtractGold(int value)
        {
            gold -= value;
        }

        // 목숨 추가 - 먹이획득 => 원래 갖고있는 목숨 += 얻은 먹이 + 얻은 먹이 * (goldAddLv * 00%)
        public void AddHeart(float value)
        {
            heart += value + (int)(value * heartAddLv * heartAddP);
        }

        //      감소 - 장애물 부딪힘 => 방어력!!!
        public void SubtractHeart(float value)
        {
            heart -= value;
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

        void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            if (input != null)
            {
                moveDir = new Vector3(input.x, 0, input.y);
            }
            isWormMoving = true;
        }

    }
}