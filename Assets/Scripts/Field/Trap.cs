using UnityEngine;

namespace WiggleQuest
{
    public class Trap : MonoBehaviour
    {
        //트랩생성 : 맵타일 기준으로 받아와서 생성, 삭제는 X : 부딪혔을때만!
        //현 위치에서 어느정도 거리 떨어지면, 삭제!
        private Vector2 thisPosition;

        //게임 메모리에 무리가 가지않으면서 게임의 흐름을 끊지않게
        // 삭제 거리가 있다면, 생성위치도 받아와야함!!ㄷㄷ
        [SerializeField] private int fireValue = 5;

        private void OnCollisionEnter(Collision collision)
        {
            //몸통줄이기
            //ㄴ꼬리가져오기??
            //ㄴDestroy(collision.gameObject.꼬리);
            Destroy(this.gameObject);
            Worm worm = collision.gameObject.GetComponent<Worm>();
            if (worm != null)
            {
                //골드추가
                worm.SubtractHeart(fireValue);
            }

            //이펙트 추가
        }
    }
}