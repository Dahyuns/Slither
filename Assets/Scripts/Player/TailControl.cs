using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class TailControl : MonoBehaviour
    {
        //�߰�����
        //���� ���� ����, ���� �����̰� �����

        private enum Count
        { Up, Down, None }
        public GameObject wormFace;    //������ ��
        public GameObject tailPrefab; //���� ������

        private Tail headTail = null;  //���Ḯ��Ʈ Head
        private Tail lastTail = null; //���� ���� ���� ����

        [SerializeField]
        private int tailCount = 0;   //'������' ���� ����
        private int tailMakedCount = 0; //'����' ���� ����
        public int tailStartCount = 3; //'���۽�'�� ���� ����

        private Vector3 tailSize;

        //�ڷ�ƾ ���� ����
        private bool isAddingTail = false;

        private void Start()
        {
            //�ʱ�ȭ : '������' ���� ���� = '���۽�'�� ���� ����
            tailCount = tailStartCount;

            //������ ���ؼ� �� �ڿ� ���� : ����� scale����
            tailSize = Vector3.Scale(tailPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size, tailPrefab.transform.localScale);
        }

        //countüũ�ϰ� �޼���ȣ��
        private void Update()
        {
            //���� ���� = �����ؾ��ϴ� ���� ����
            //tailCount = Worm.Level;

            //[ġƮŰ] : ���� ���� up
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                tailCount++;
            }

            switch (CheckCount())
            {
                case Count.None:
                    break;

                case Count.Up:
                    if (isAddingTail == false)
                    {
                        StartCoroutine(AddTail());
                    }
                    break;

                case Count.Down:
                    DestroyTail();
                    break;
            }
        }

        Count CheckCount()
        {
            //�������� ��������ִ� ������ ������
            int countValue = tailCount - tailMakedCount;

            if (countValue == 0)
                return Count.None;

            else if (countValue > 0)
                return Count.Up;

            else //if (countValue < 0)
                return Count.Down;
        }

        //[2]���Ḯ��Ʈ ���Ȱ��  
        IEnumerator AddTail()
        {
            Debug.Log("�ڷ�ƾ ������");
            isAddingTail = true;

            //����
            GameObject newobj = Instantiate(tailPrefab);
            Tail newtail = newobj.GetComponent<Tail>();


            // ���� ������ �ϳ��� ���ٸ�(ó�� ����� ����) : ��ó���� ȣ��
            if (tailMakedCount == 0)
            {
                //���� ���� ������
                newtail.beforeTail = wormFace.GetComponent<Tail>();

                //�� ������
                Vector3 faceSize = Vector3.Scale(wormFace.GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                                                     wormFace.transform.localScale);
                // ���� ���� ����
                // ����� ���� ����! 0<x<r , 0<y<r 
                float r = tailSize.x / 2 + faceSize.x / 2; //������
                float x = Random.Range(0f, -r); // x��ǥ(2��и�)
                //y = (��Ʈ) r2 - x2
                float y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow(x, 2));

                //new���� ��ǥ
                Vector3 newPos = wormFace.transform.position + new Vector3(-x, 0, -y);

                //�̹� ������ ��ġ��? : �� ��ġ�� + ���� ������ (������ġ / 4��и�)
                newtail.transform.position = newPos;
            }

            // ���� ������ �ִٸ� == beforeTail�� �����Ѵٸ�
            else if (tailMakedCount > 0)
            {
                //head(������ ���� ����)�� �� ������ beforeTail 
                newtail.beforeTail = headTail;

                //�̹� ������ ��ġ��? :
                //���� ���� ��ġ��
                //      ���� : ������ ��ġ���� ��������, ũ�� : ����������  
                Vector3 dir = newtail.beforeTail.transform.position - newtail.beforeTail.beforeTail.transform.position;
                dir = new Vector3(dir.x, 0f, dir.z); //���̹���

                if(tailMakedCount == 1) //2��° ����
                {
                    dir = Vector3.ClampMagnitude(dir, tailSize.x);
                }

                newtail.transform.position = newtail.beforeTail.transform.position + dir;
            }
            //����ó��
            else if (tailMakedCount < 0)
            {
                Debug.Log("AddTail Code Error");
            }



            tailMakedCount++;//ī��Ʈ +1
            newtail.tailNumber = tailMakedCount; //�̹� ������ �����?
            Debug.Log(tailMakedCount);

            //�� ������ head�� last�� ����
            headTail = newtail; //������
            lastTail = newtail; //������

            isAddingTail = false;
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
            tailMakedCount--;
        }
    }
}