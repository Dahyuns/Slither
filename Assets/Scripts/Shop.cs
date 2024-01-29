using UnityEngine;
using UnityEngine.UI;

namespace Slither
{
    public class Shop : MonoBehaviour
    {
        //상점 버튼 [ Shop ]
        public Button ShopButton;

        //상점에 들어갔는지?
        private bool isInShop = false;

        // 타이틀 <-> ( 필드 <-> 상점 )
        //필드입장 : 게임시작        ㅡ 타이틀에서
        //필드퇴장 : 게임오버        ㅡ 타이틀로
        //상점입장 : 상점 버튼 클릭  ㅡ 필드에서
        //상점퇴장 : ESC            ㅡ 필드로
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

        //상점에서 필드로
        public void GotoPlayGround()
        {
            Time.timeScale = 1f; //다시 재생
            ShopStatusReverse();
        }

        //버튼 클릭시
        //필드에서 상점으로
        public void GotoShop()
        {
            Time.timeScale = 0f; //일시정지
            ShopStatusReverse();
        }

        //상점 진입 / 퇴장
        private void ShopStatusReverse()
        {
            ShopButton.enabled = !(ShopButton.enabled);
            ShopButton.image.enabled = !(ShopButton.image.enabled);
            isInShop = !isInShop;
        }

        //방어력 구매
        public void PurchaseDef()
        {

        }

        //먹이퍼센트 구매
        public void PurchaseHeartP()
        {

        }

        //골드퍼센트 구매
        public void PurchaseGoldP()
        {

        }

    }
}
