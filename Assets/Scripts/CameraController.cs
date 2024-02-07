using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class CameraController : MonoBehaviour
    {
        //������ : ���� ��ü 
        public GameObject worm;
        //����Ʈ
        public GameObject runEffect;

        //����Ʈ �����ֳ�?
        private bool isEffctAct = false;

        private void Update()
        {
            /*Vector3 dir = worm.transform.position - this.transform.position;
            Vector3 moveDir = new Vector3(dir.x * cameraSpeed * Time.deltaTime,
                                          0.0f,
                                          dir.y * cameraSpeed * Time.deltaTime);
            this.transform.Translate(moveDir);*/
            //worm ���� ������
            this.transform.position = new Vector3(worm.transform.position.x, transform.position.y, worm.transform.position.z);

            if (isEffctAct == false)
            {
                if (CheckInput())//�Է��� �ִٸ� ����Ʈ ���
                {
                    StartCoroutine(EffectAct());
                }
            }

            //�Է�üũwsad
            bool CheckInput()
            {
                if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                     Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)  )
                    return true;
                return false;
            }

            //����Ʈ ���
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