using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class EffectManager : MonoBehaviour
    {
        public GameObject RunEffect;
        public Transform Efftransform;

        //������ ����Ʈ
        private GameObject runEff;

        // ���� bool������ �����ͼ� �޸��� ���� @@���ĺ���~ false�ɶ�����
        [SerializeField] private float createCount = 0.5f;
        // ����Ʈ ���ӽð�
        [SerializeField] private float Destroycount = 0.5f;

        // �ڷ�ƾ ���������� üũ
        [SerializeField]  private bool isActEffct = false;

        private void Update()
        {
            //[�޸�������Ʈ����]
            //����ƮX, �����̰� ������
            if (isActEffct == false && Worm.isWormMoving == true)
            {
                StartCoroutine(CreateRunEff());
            }
            //����ƮO, �����̰� �������� ������
            else if (isActEffct == true && Worm.isWormMoving == false)
            {
                //������ ����Ʈ�� �ִٸ�
                if (runEff != null)
                {
                    //0.5���� ����Ʈ ����
                    Destroy(runEff, Destroycount);
                    isActEffct = false;
                }
            }
        }

        IEnumerator CreateRunEff()
        {
            isActEffct = true;

            //@@�� ����
            yield return new WaitForSeconds(createCount);

            //����Ʈ ����
            runEff = Instantiate(RunEffect, Efftransform);
        }
    }
}