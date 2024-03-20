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

        //+ ���������� ���� �Լ��� fire���� Ȯ���� ��������. < �ٸ� �������� �ʹ� ���� ���ͼ� �Ѱ��� ��ħ����
        //  ���� ������ ���� ����ؼ� Ȯ���� �ݿ��ϴ� ������?

        //���� 
        private GameObject mainCamera;
        private GameObject groupTrap;
        private GameObject groupDrop;
        private FieldControl fieldControl;

        private GameObject item;

        //������
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        private Vector3 thisSize;

        //�ʵ� ��� ���� (Ŀ������ ��������, �⺻�� 1)
        private float cycleMulti = 1;

        //�ش� �ʵ峻 ������ ���� ����Ʈ
        [SerializeField] private List<GameObject> FieldItemList = new List<GameObject>();
        [SerializeField] private List<Vector3> itemPosList;

        int numFire;
        int numGold;
        int numFeed;

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
            {
                StartCoroutine(CreateItems()); //�ε��� �Բ� ����?
            }
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


        public enum DropItem
        {
            Fire, Gold, Feed, None
        }

        //���� ������ ����
        public GameObject RandomItemCreate()
        {
            List<GameObject> prefabList = new List<GameObject>() { firePrefab, goldPrefab, feedPrefab };
            if (numFire == 0)
                prefabList.Remove(firePrefab);
            if (numGold == 0)
                prefabList.Remove(goldPrefab);
            if (numFeed == 0)
                prefabList.Remove(feedPrefab);
            //Debug.Log($"numFire : {numFire}, numGold : {numGold}, numFeed : {numFeed}");
            if (prefabList.Count != 0)
            {
                //�������� ��� ����
                int rand = Random.Range(0, prefabList.Count);
                if (prefabList[rand] == firePrefab)
                {
                    numFire--;
                    return Instantiate(prefabList[rand], groupTrap.transform);
                }
                else
                {
                    if (prefabList[rand] == goldPrefab)
                        numGold--;
                    else if (prefabList[rand] == feedPrefab)
                        numFeed--;

                    return Instantiate(prefabList[rand], groupDrop.transform);
                }
            }
            else
            {
                Debug.Log("fieldTile - totalũ�⺸�� �� ���� ���� / prefabList����");
                return null;
            } //����üũ
        }

        //�ʵ����� ������ ���� + ����Ʈ�� ����
        public IEnumerator CreateItems()
        {
            //�ʵ� Ÿ�� ������(�Ÿ� ������)
            thisSize = Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.size,
                                         transform.localScale);

            numFire = fieldControl.numFire - Random.Range(0, fieldControl.numFire);
            numGold = fieldControl.numGold - Random.Range(0, fieldControl.numGold);
            numFeed = fieldControl.numFeed - Random.Range(0, fieldControl.numFeed);

            //�� ����
            int total = numFeed + numFire + numGold;

            float offset = 1f; //�翷,���Ʒ� �ٸ�Ÿ�ϰ� ��ġ���ʰ� ���� �뵵
            float unit = (thisSize.x - offset * 2) / total;

            
            for (int i = 0; i < total; i++)
            {
                //���� + ������ ����Ʈ�� �߰�(�����Ҷ� �ʿ��� ����Ʈ)
                item = RandomItemCreate();
                FieldItemList.Add(item);

                //���ʾƷ� ���� ��ǥ ���� (sin�׷����� ����)
                Vector3 thisPos = this.transform.position - (thisSize / 2);

                // Sin�Լ� ���� ����(=z���� ������ ��) * SIN�Լ� (�� �Ÿ��� ��� * ������ x��) + �����̵���(=z���� ������ ��)
                float numX = offset + unit * i;
                float numZ = (thisSize.z / 2 - offset *2) * Mathf.Sin(cycleMulti * numX) + (thisSize.z / 2);

                //���� ������ ��ġ ����
                FieldItemList[i].transform.position = new Vector3(thisPos.x + numX, 0f, thisPos.z + numZ);
            }
            yield return null;
        }
    }
}




/*
    int rand = Random.Range(0, 3);
    switch (rand)
    {
        case 0:
            if (numFire > 0)
            {
                item = Instantiate(goldPrefab, groupTrap.transform);
                numFire--;
            }
            break;

        case 1:
            if (numGold > 0)
            {
                item = Instantiate(goldPrefab, groupDrop.transform);
                numGold--;
            }
            break;

        case 2:
            if (numFeed > 0)
            {
                item = Instantiate(feedPrefab, groupDrop.transform);
                numFeed--;
            }
            break;
    }
*/
/*
                if (numFeed > 0)
                {
                    item = Instantiate(feedPrefab, groupDrop.transform);
                    numFeed--;
                }
                else if (numFire > 0)
                {
                    item = Instantiate(firePrefab, groupTrap.transform);
                    numFire--;
                }
                else if (numGold > 0)
                {
                    item = Instantiate(goldPrefab, groupDrop.transform);
                    numGold--;
                }
                else { Debug.Log("itemcreate Error"); }*/