using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class CameraController : MonoBehaviour
    {
        //지렁이 : 따라갈 객체 
        public GameObject worm;
        //이펙트
        public GameObject runEffect;

        //이펙트 쓰고있나?
        private bool isEffctAct = false;

        private void Update()
        {
            /*Vector3 dir = worm.transform.position - this.transform.position;
            Vector3 moveDir = new Vector3(dir.x * cameraSpeed * Time.deltaTime,
                                          0.0f,
                                          dir.y * cameraSpeed * Time.deltaTime);
            this.transform.Translate(moveDir);*/
            //worm 따라서 움직임
            this.transform.position = new Vector3(worm.transform.position.x, transform.position.y, worm.transform.position.z);

            if (isEffctAct == false)
            {
                if (CheckInput())//입력이 있다면 이펙트 출력
                {
                    StartCoroutine(EffectAct());
                }
            }

            //입력체크wsad
            bool CheckInput()
            {
                if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                     Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)  )
                    return true;
                return false;
            }

            //이펙트 출력
            IEnumerator EffectAct()
            {

                isEffctAct = true;
                GameObject effect = Instantiate(runEffect, this.transform);

                yield return new WaitForSecondsRealtime(0.5f);

                Destroy(effect, 0.5f);
                isEffctAct = false;
            }
        }
    }
}