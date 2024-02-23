using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class EffectManager : MonoBehaviour
    {
        public GameObject RunEffect;
        public Transform Efftransform;

        //생성한 이펙트
        private GameObject runEff;

        // 상태 bool형으로 가져와서 달리기 시작 @@초후부터~ false될때까지
        [SerializeField] private float createCount = 0.5f;
        // 이펙트 지속시간
        [SerializeField] private float Destroycount = 0.5f;

        // 코루틴 실행중인지 체크
        [SerializeField]  private bool isActEffct = false;

        private void Update()
        {
            //[달리기이펙트관리]
            //이펙트X, 지렁이가 움직임
            if (isActEffct == false && Worm.isWormMoving == true)
            {
                StartCoroutine(CreateRunEff());
            }
            //이펙트O, 지렁이가 움직이지 않을때
            else if (isActEffct == true && Worm.isWormMoving == false)
            {
                //생성된 이펙트가 있다면
                if (runEff != null)
                {
                    //0.5초후 이펙트 삭제
                    Destroy(runEff, Destroycount);
                    isActEffct = false;
                }
            }
        }

        IEnumerator CreateRunEff()
        {
            isActEffct = true;

            //@@초 이후
            yield return new WaitForSeconds(createCount);

            //이펙트 생성
            runEff = Instantiate(RunEffect, Efftransform);
        }
    }
}