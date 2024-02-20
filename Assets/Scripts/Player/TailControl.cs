using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class TailControl : MonoBehaviour
    {
        private enum Count
        { Up, Down, None }
        public Transform wormFace;    //������ ��
        public GameObject tailPrefab; //���� ������

        private Tail headTail = null;  //���Ḯ
        private Tail lastTail = null; //���� ���� ���� ����

        [SerializeField]
        private int tailCount = 0;   //'������' ���� ����
        private int tailMakeCount = 0; //'����' ���� ����
        public int tailStartCount = 3; //'���۽�'�� ���� ����

        private Vector3 tailSize;

        private void Start()
        {
            //�ʱ�ȭ : '������' ���� ���� = '���۽�'�� ���� ����
            tailCount = tailStartCount;

            //������ ���ؼ� �� �ڿ� ���� : ����� scale����
            tailSize = Vector3.Scale(tailPrefab.GetComponent<MeshRenderer>().bounds.size, tailPrefab.transform.localScale);
        }

        //countüũ�ϰ� �޼���ȣ��
        private void Update()
        {
            //����� : ���� ���� ���� up
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                tailCount++;
            }

            switch (CheckCount())
            {
                case Count.None:
                    break;

                case Count.Up:
                    StartCoroutine(AddTail());
                    break;

                case Count.Down:
                    DestroyTail();
                    break;
            }
        }
        Count CheckCount()
        {
            //�������� ��������ִ� ������ ������
            int countValue = tailCount - tailMakeCount;
            if (countValue == 0)
                return Count.None;
            else if (countValue > 0)
                return Count.Up;
            else //if (countValue < 0)
                return Count.Down;
        }

        IEnumerator AddTail()
        {
            //[2]���Ȱ��!- ���Ḯ��Ʈ
            //�̹� ���� ����! : �� �ڽ�����
            Tail newtail = Instantiate(tailPrefab, wormFace).GetComponent<Tail>();

            //���� ������ �ϳ��� ���ٸ�(ó�� ����� ����) : ��ó���� ȣ��
            if (tailMakeCount == 0)
            {
                //���� ���� ������
                newtail.beforeTail = wormFace.GetComponent<Tail>();
                //�̹� ������ ��ġ��? : �� ��ġ�� + ���� ������ (������ġ / �ݴ��� ����)
                Vector3 faceSize = Vector3.Scale(wormFace.GetComponent<MeshRenderer>().bounds.size,
                                                                     wormFace.transform.localScale);//�̰� �߸��ȵ�!!?

                newtail.transform.position = new Vector3
                    (wormFace.transform.position.x + (faceSize.x / 2 + tailSize.x / 2) , //x
                     0f, 0f);
                     //wormFace.transform.position.y + ((faceSize.y + tailSize.y) / 2));
            }
            //���� ������ �ִٸ� == beforeTail�� �����Ѵٸ�
            else if (tailMakeCount > 0)
            {
                //head�� �� ������ �ռ��� 
                newtail.beforeTail = headTail;
                //�̹� ������ ��ġ��? : ���� ���� ��ġ�� + ���� ������ (������ġ / �ݴ��� ����) 
                newtail.transform.position = newtail.beforeTail.transform.position +
                                                new Vector3(tailSize.x, 0f, -tailSize.y);
            }
            //����ó��
            else if (tailMakeCount < 0)
            {
                Debug.Log("AddTail Code Error");
            }

            tailMakeCount++;//ī��Ʈ +1
            //�̹� ������ �����?
            newtail.tailNumber = tailMakeCount;

            //�� ������ head�� last�� ����
            headTail = newtail; //������
            lastTail = newtail; //������

            yield return null;
        }

        void DestroyTail()
        {
            //Head�� ���� ������
            headTail = headTail.beforeTail;
            //�̹����� ����    
            Destroy(lastTail);

            //������, null������ lastTail�� Head����
            lastTail = headTail;
            //'����' ���� ���� ����
            tailMakeCount--;
        }
    }
}