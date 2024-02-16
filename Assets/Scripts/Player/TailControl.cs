using UnityEngine;

namespace WiggleQuest
{
    public class TailControl : MonoBehaviour
    {
        public Transform worm;        //������ ��!
        public GameObject tailPrefab; //���� ������

        private Tail headTail = null;  //���� ���� ����
        private Tail lastTail = null; //���� ���� ���� ����

        public static int tailCount = 0; //���� �?

        private void Start()
        {
            //���۽� count��ŭ ���� ����
            for(int i = 0; i < tailCount; i++)
            {
                AddTail();
            }
        }

        void AddTail()
        {
            //[2]���Ȱ��!- ���Ḯ��Ʈ
            //�̹� ���� ����!
            Tail newtail = Instantiate(tailPrefab, worm).GetComponent<Tail>();
            //�̹� ������ �����?
            newtail.tailNumber = tailCount;
            tailCount++; //ī��Ʈ +1
            Debug.Log(tailCount);
            //head�� �� ������ �ռ��� 
            newtail.beforeTail = headTail;
            //�� ������ head�� last�� ����
            headTail = newtail;
            lastTail = newtail;

            //�̹� ������ ��ġ��
            //�������� �ٶ󺸴� ������ �ݴ������� 00��ŭ�̴�. 
            newtail.transform.position = newtail.beforeTail.transform.position;

        }

        void DestroyTail()
        {
            headTail = headTail.beforeTail;
            Destroy(lastTail);
            lastTail = headTail;
            tailCount--;
        }

    }
}