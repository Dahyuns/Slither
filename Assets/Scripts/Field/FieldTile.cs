using System.Collections.Generic;
using UnityEngine;


namespace WiggleQuest
{
    public class FieldTile : MonoBehaviour
    {
        //�߰�����?
        //�����س��ٰ� �ٽ� �������� ���? -  ������Ʈ Ǯ���̶�� �ϴ���?
        //                              this.gameObject.SetActive(false);

        //FieldTile : �ش� �ʵ�Ÿ�� ������ ��Ģ�� ���� �������� ������ ����
        //             feed gold trap(fire,...) ��ġ ����/ ���� ���� �����ϰ� /
        //            �÷��̾�� �����Ÿ� �־����� (�ʵ� �� ��� �����۰� �Բ�)����

        //���� 
        private GameObject worm;
        private FieldControl fieldControl;

        //������
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        //���� ���� �Ÿ�
        [SerializeField] private float standardDis = 1000f;

        private Vector3 thisHalfSize;

        //�ش� �ʵ峻 ������ ���� ����Ʈ
        private List<GameObject> FieldItemList;
        private List<Vector3> itemPosList;

        private void Start()
        {
            worm = GameObject.Find("Worm");
            fieldControl = GameObject.Find("Field").GetComponent<FieldControl>();
            //�������� ��(�Ÿ� ������)
            thisHalfSize = ( Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                         transform.localScale) ) / 2;
        }

        private void Update()
        {
            //�� ��ġ üũ �� ����
            if (fieldControl.isCreateTileMove && CheckDis())
            {
                DestroyField();
            }
        }

        //�ʵ�� Worm ������ �Ÿ� üũ
        private bool CheckDis()
        {
            float distance = (transform.position - worm.transform.position).magnitude;
            if (distance > standardDis)
                return true;
            else
                return false;
        }

        private void DestroyField()
        {
            //���� ����
            
            //�ʵ峻 ������ ����
            //for (int i = 0; i < FieldItemList.Count; ++i)
            {
                //Destroy(FieldItemList[i]);
            }

            //����Ʈ �� �ʵ� ��ġ ����
            for (int i = 0; i < fieldControl.fieldTiles.Count; i++)
            {
                if (fieldControl.fieldTiles[i] == this.transform.position)
                {
                    fieldControl.fieldTiles.Remove(fieldControl.fieldTiles[i]);
                }
            }

            //�ʵ� ����
            Destroy(this.gameObject);
        }

        //�ʵ����� ������ ���� + ����Ʈ�� ����    (��Ģ�����󷣴�)
        public void CreateItems()
        {
            //�뷱�� ���� ���� �κ� - ���� ����
            int numFire = 2;//Random.Range(0, 3);  //�ʵ�� (0~3)�� ����
            int numGold = 2;//Random.Range(0, 1);  //�ʵ�� (0~1)�� ����
            int numFeed = 2;//Random.Range(0, 3);  //�ʵ�� (0~2)�� ����

            //�� ����
            int totalList = numFeed + numFire + numGold;

            FieldItemList = new List<GameObject>(numFeed + numFire + numGold);

            for (int i = 0; i < totalList; i++)
            {
                //���� + ������ ����Ʈ�� �߰�
                FieldItemList.Add(Instantiate(firePrefab));

                //������ ��ǥ ����
                float numX = Random.Range(this.transform.position.x - thisHalfSize.x,
                                        this.transform.position.x + thisHalfSize.x);
                float numZ = Random.Range(this.transform.position.z - thisHalfSize.z,
                                        this.transform.position.z + thisHalfSize.z);

                //������ ��ǥ, ��ǥ ����Ʈ�� �߰�
                itemPosList.Add(new Vector3(numX, 0f, numZ));

                //���� ������ ��ġ ����
                FieldItemList[i].transform.position = new Vector3(numX, 0f, numZ);

                //�����ؾ��� ���� ����
                totalList--;
            }
            Debug.Log(totalList);
        }

    }
}