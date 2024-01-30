using UnityEngine;
using UnityEngine.InputSystem;

namespace Slither
{
    public class Worm : MonoBehaviour
    {
        private static int gold;                          //골드
        private static float heart;                       //목숨
        private static float speed;                       //속도
        private static float def;                         //방어력
        private static int level = (int)heart;                         //레벨 = 목숨 반내림값
                                                          
        //이거 상점에 두는게 낫나?                          
        private static float goldAddP;                    //골드  --%만큼 추가 획득
        private static float heartAddP;                   //먹이  --%만큼 추가 획득
        private static float defAddP;                     //방어력 --%만큼 추가 획득

        //읽기전용
        public int   Gold  { get { return gold;  } }
        public float Heart { get { return heart; } }
        public float Speed { get { return speed; } }
        public float Def   { get { return def;   } }
        public int   Level { get { return level; } }

        //시작시 
        [SerializeField] private int startGold = 0;
        [SerializeField] private int startHeart = 5;
        [SerializeField] private float startSpeed = 10f;
        [SerializeField] private float startDef = 0;

        Vector3 moveDir;

        private void Awake()
        {
            //이거 되는지 궁금스
            //inputActions = GetComponent<WormControls>();
       }

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
        }


        // 골드 추가 - 골드획득 => 원래 들고있는 골드 += 얻은 골드 + 얻은 골드 * goldAddP

        // 골드 감소 - 상점구매

        // 목숨 추가 - 먹이획득 => 원래 갖고있는 목숨 += 얻은 먹이 + 얻은 먹이 * goldAddP

        // 목숨 감소 - 장애물 부딪힘

        // 속도 증가

        // 방어력 추가 (퍼센트)
    }
}