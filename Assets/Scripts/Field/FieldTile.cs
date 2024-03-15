using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WiggleQuest
{
    public class FieldTile : MonoBehaviour
    {
        //�߰�����?
        //�����س��ٰ� �ٽ� �������� ���? -  ������Ʈ Ǯ��?
        //�ʵ� �������� ��ǥ ���� ��ġ���ʰ� ����� : sin�׷����� x��ǥ ����Ʈ �����! x��ġ�� �ʱ�
        // ��sin�׷��� ��� 200sin ? x = y   : ?�� �ֱ⿡ ���ϴ� ��� , ������ ��������

        //���� 
        private GameObject mainCamera;
        private FieldControl fieldControl;
        private GameObject groupTrap;
        private GameObject groupDrop;

        //������
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        private Vector3 thisSize;

        //�ʵ� ��� ���� (Ŀ������ ��������, �⺻�� 1)
        public float cycleMulti = 1;

        //�ش� �ʵ峻 ������ ���� ����Ʈ
        [SerializeField] private List<GameObject> FieldItemList = new List<GameObject>();
        [SerializeField] private List<Vector3> itemPosList;

        private void Awake()
        {
            fieldControl = GameObject.Find("Field").GetComponent<FieldControl>();
            mainCamera = GameObject.Find("Main Camera");
            groupTrap = GameObject.Find("Trap");
            groupDrop = GameObject.Find("Drop");
            //�������� ����X (�浹 ���ɼ�)
            if (fieldControl.fieldTiles.Count == 0)
                return;
            else  //�ʵ峻 �����۵� ����
                StartCoroutine(CreateItems());
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
            if (distance > fieldControl.standardDis)
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
        public IEnumerator CreateItems()
        {
            //�ʵ� Ÿ�� ������(�Ÿ� ������)
            thisSize = Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                         transform.localScale);

            int numFire = fieldControl.numFire;
            int numGold = fieldControl.numGold;
            int numFeed = fieldControl.numFeed;

            //�ʵ�� * ~ *�� ����
            //numFire = Random.Range(0, numFire);
            //numGold = Random.Range(0, numGold);
            //numFeed = Random.Range(0, numFeed);

            //fire
            for (int i = 0; i < numFire; i++)
            {
                //����
                GameObject item = Instantiate(firePrefab, groupTrap.transform);

                //������ ��Ͽ� �߰�
                FieldItemList.Add(item);

                RanPosMake();

                //���� ������ ��ġ ����
                FieldItemList[i].transform.position = itemPosList[i];
                numFire--;
            }




            //�� ����
            int totalList = numFeed + numFire + numGold;

            for (int i = 0; i < totalList; i++)
            {
                GameObject item;

                //���� + ������ ����Ʈ�� �߰�
                if (numFeed > 0)
                {
                    item = Instantiate(feedPrefab, groupDrop.transform);

                    Debug.Log(numFeed);
                    numFeed--;
                }
                else if (numFire > 0)
                {
                    item = Instantiate(firePrefab, groupTrap.transform);

                    numFire--;
                }

                else //if (numGold > 0)
                {
                    item = Instantiate(goldPrefab, groupDrop.transform);

                    Debug.Log(numGold);
                    numGold--;
                }

                FieldItemList.Add(item);

                //������ ��ǥ ���� //��ġ���ʰ� �����
                Vector3 thisPos = this.transform.position - (thisSize / 2); //���ʾƷ� ����
                                                                            // Sin�Լ� ���� ����(=z���� ������ ��) * SIN�Լ� (�� �Ÿ��� ��� * ������ x��) + �����̵���(=z���� ������ ��)
                float numX = RanX();
                float numZ = (thisSize.z / 2) * Mathf.Sin(cycleMulti * numX) + (thisSize.z / 2);

                //������ ��ǥ, ��ǥ ����Ʈ�� �߰�
                itemPosList.Add(new Vector3(thisPos.x + numX, 0f, thisPos.z + numZ));

                //���� ������ ��ġ ����
                FieldItemList[i].transform.position = itemPosList[i];



                //�����ؾ��� ���� ����

                totalList--;
            }
            yield return null;
        }

        private void RanPosMake()
        {
            //���ʾƷ� ���� ��ǥ ���� (sin�׷����� ����)
            Vector3 thisPos = this.transform.position - (thisSize / 2);

            //������ ��ǥ ���� //��ġ���ʰ� �����
            // Sin�Լ� ���� ����(=z���� ������ ��) * SIN�Լ� (�� �Ÿ��� ��� * ������ x��) + �����̵���(=z���� ������ ��)
            float numX = RanX();
            float numZ = (thisSize.z / 2) * Mathf.Sin(cycleMulti * numX) + (thisSize.z / 2);

            //������ ��ǥ, ��ǥ ����Ʈ�� �߰�
            itemPosList.Add(new Vector3(thisPos.x + numX, 0f, thisPos.z + numZ));
        }

        private float RanX()
        {
            float numX = Random.Range(0, thisSize.x);
            bool isNullPos = false;

            while (isNullPos == false)
            {
                foreach (Vector3 itempos in itemPosList)
                {
                    if (numX < itempos.x + 1 || numX > itempos.x - 1)
                    {
                        continue;
                    }
                }
                isNullPos = true;
                return numX;
            }

            Debug.Log("FIELDTILE RanX �Լ� ����");
            return 0;
        }
    }
}