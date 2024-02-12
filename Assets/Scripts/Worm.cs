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
        private static int gold;                          //골드
        private static float heart;                       //목숨
        private static float speed;                       //속도
        private static float def;                         //방어력

        //읽기전용
        public static int Gold      { get { return gold; } }
        public static float Heart   { get { return heart; } }
        public static float Speed   { get { return speed; } }
        public static float Def     { get { return def; } }
        public static int Level     { get { return (int)heart;} } //머리위에 표시 //레벨 = 목숨 반내림값

        //--%만큼 추가 획득
        private static float goldAddP = 0;                    //골드   --%만큼 추가 획득
        private static float heartAddP = 0;                   //먹이   --%만큼 추가 획득
        private static float defAddP = 0;                     //방어력 --%만큼 추가 획득
        private static float speedAddP = 0;                   //속도   --%만큼 추가 획득

        //Gold, Heart, Def중 선택, %추가
        //밸런스조절부분
        public void AddP(AddPercent what)
        {
            //골드차감?..상점에서 하는걸로 나중에 바꾸삼
            SubtractGold(100);

            switch (what)
            {
                //골드
                case AddPercent.Gold:
                    goldAddP += 0;
                    break;

                //먹이
                case AddPercent.Heart:
                    heartAddP += 0;
                    break;

                //방어력
                case AddPercent.Def:
                    defAddP += 0;
                    break;

                //속도
                case AddPercent.Speed:
                    speedAddP += 0;
                    break;
            }
        }

        //시작 스탯
        [SerializeField] private int startGold = 0;
        [SerializeField] private int startHeart = 3;
        [SerializeField] private float startSpeed = 10f;
        [SerializeField] private float startDef = 0;

        //움직임 벡터
        Vector3 moveDir;
        //꼬리 코드에 가져갈거
        public static bool isWormMoving = false;

        private void Start()
        {
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

        void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            if (input != null)
            {
                moveDir = new Vector3(input.x, 0, input.y);
            }
            isWormMoving = true;
        }

        //[ float 에서 int 로 변환할때 기본적으로 소수점 뒷자리를 버림을 한다. ]

        // 골드 추가 - 골드획득 => 원래 들고있는 골드 += 얻은 골드 + 얻은 골드 * goldAddP
        public void AddGold(int value)
        {
            gold += value + (int)(value * goldAddP);
        }

        //      감소 - 상점구매
        public void SubtractGold(int value)
        {
            gold -= value;
        }

        // 목숨 추가 - 먹이획득 => 원래 갖고있는 목숨 += 얻은 먹이 + 얻은 먹이 * goldAddP
        public void AddHeart(float value)
        {
            heart += value + (int)(value * heartAddP);
        }

        //      감소 - 장애물 부딪힘 => 방어력!!!
        public void SubtractHeart(float value)
        {
            heart -= value;
        }

        // 속도 증가
        public void AddSpeed(float value)
        {
            speed += value;
        }

        // 방어력 추가 (퍼센트)
        public void AddDef(float value)
        {
            def += value;
        }
    }
}