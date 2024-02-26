using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //추가구현
        //추가, 삭제 영역 저장 

        //feed gold trap(fire,)
        //     ㄴ 위치 랜덤/ 수량 조절 가능하게 / 겹치면X
        //                                      ㄴ 겹치면 destroy?


        //Create Field : Create 구역내에 생성 및 Destroy타일을 만듬
        //예비 타일이 이거고, 
        //생성하면 create타일은 삭제 or 이동
        //**그곳에 destroy가 있는지 확인, 없으면 생성
        //collider로 닿으면 false반환?


        enum TilePos
        {
            Upper,      //위
            Right,      //오른
            Left,       //왼
            Lower       //아래 
        }

        private float createPosX;
        private float createPosY;


        //Destroy Field : 현 Destroy타일이 플레이어에서 일정거리 멀어지면 (필드 위 모든 아이템과 함께)삭제
        private float destroyPosX;
        private float destroyPosY;

        private float destroyDistance; //일정거리


    }
}
