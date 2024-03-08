using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;


namespace WiggleQuest
{
    public class FieldTile : MonoBehaviour
    {
        //�߰�����?
        //�����س��ٰ� �ٽ� �������� ���? -  ������Ʈ Ǯ���̶�� �ϴ���?
        //                              this.gameObject.SetActive(false);
        //������ ��ǥ ��ġ���ʰ� �����

        //FieldTile : �ش� �ʵ�Ÿ�� ������ ��Ģ�� ���� �������� ������ ����
        //             feed gold trap(fire,...) ��ġ ����/ ���� ���� �����ϰ� /
        //            �÷��̾�� �����Ÿ� �־����� (�ʵ� �� ��� �����۰� �Բ�)����

        //���� 
        private GameObject mainCamera;
        private FieldControl fieldControl;
        private GameObject groupTrap;
        private GameObject groupDrop;

        //������
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        //�ʵ�� ���� ����(�ִ�)
        [SerializeField] private int numFire = 5;
        [SerializeField] private int numGold = 1;
        [SerializeField] private int numFeed = 4;

        //���� ���� �Ÿ�
        [SerializeField] private float standardDis = 1000f;

        private Vector3 thisSize;

        //�ش� �ʵ峻 ������ ���� ����Ʈ
        [SerializeField] private List<GameObject> FieldItemList = new List<GameObject>();
        [SerializeField] private List<Vector3> itemPosList;

        //feed Mat ����
        Material[] mat;

        private void Awake()
        {
            mainCamera = GameObject.Find("Main Camera");
            fieldControl = GameObject.Find("Field").GetComponent<FieldControl>();
            groupTrap = GameObject.Find("Trap");
            groupDrop = GameObject.Find("Drop");
           // mat = feedPrefab.GetComponent<Renderer>().materials;
        }

        private void Update()
        {
            //�� ��ġ üũ �� [����]
            if (fieldControl.isCreateTileMove && CheckDis())
            {
                DestroyField();
            }
        }

        //�ʵ�� camera ������ �Ÿ� üũ
        private bool CheckDis()
        {
            float distance = (transform.position - mainCamera.transform.position).magnitude;
            if (distance > standardDis)
                return true;
            else
                return false;
        }

        private void DestroyField()
        {
            //���� ����
            
            //�ʵ峻 ������ ����
            for (int i = 0; i < FieldItemList.Count; ++i)
            {
                Destroy(FieldItemList[i]);
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
            //�ʵ� Ÿ�� ������(�Ÿ� ������)
            thisSize = Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                         transform.localScale);

            //�ʵ�� * ~ *�� ����
            numFire = Random.Range(1, numFire);
            numGold = Random.Range(1, numGold);
            numFeed = Random.Range(1, numFeed);

            //�� ����
            int totalList = numFeed + numFire + numGold;

            for (int i = 0; i < totalList; i++)
            {
                GameObject item;

                //���� + ������ ����Ʈ�� �߰�
                if (numFire > 0)
                {
                    item = Instantiate(firePrefab, groupTrap.transform);
                    FieldItemList.Add(item);

                    numFire--;
                }
                else if (numFeed > 0)
                {
                    item = Instantiate(feedPrefab, groupDrop.transform);
                    FieldItemList.Add(item);

                    numFeed--;
                }
                else if (numGold > 0)
                {
                    //���� + ������ ����Ʈ�� �߰�
                    item = Instantiate(goldPrefab, groupDrop.transform);
                    FieldItemList.Add(item);

                    numGold--;
                }


                //������ ��ǥ ���� //��ġ���ʰ� �����
                float numX = Random.Range(0, thisSize.x);
                float numZ = Random.Range(0, thisSize.z);
                Vector3 thisPos = this.transform.position - (thisSize / 2); //���ʾƷ� ����

                //������ ��ǥ, ��ǥ ����Ʈ�� �߰�
                itemPosList.Add(new Vector3(thisPos.x + numX, 0f, thisPos.z + numZ));

                //���� ������ ��ġ ����
                FieldItemList[i].transform.position = itemPosList[i];

                //�����ؾ��� ���� ����
                totalList--;
            }
        }


    }
}