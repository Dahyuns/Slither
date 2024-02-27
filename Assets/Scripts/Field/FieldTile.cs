using System.Collections.Generic;
using UnityEngine;


namespace WiggleQuest
{
    public class FieldTile : MonoBehaviour
    {
        //FieldTile : �� Ÿ���� �÷��̾�� �����Ÿ� �־����� (�ʵ� �� ��� �����۰� �Բ�)����

        //����
        public GameObject worm;

        //������
        public GameObject firePrefab;
        public GameObject feedPrefab;
        public GameObject goldPrefab;

        //���� ���� �Ÿ�
        [SerializeField] private float standardDis = 20f;

        private List<GameObject> FieldItemList;

        private void Start()
        {
            //���� ���� + ����Ʈ�� ����
            CreateFieldItems();

        }

        private void Update()
        {
            //�� ��ġ üũ �� ����
            if (CheckFieldTilePosWithWorm())
            {
                DestroyFieldItems();
            }
        }


        //����ġüũ
        private bool CheckFieldTilePosWithWorm()
        {
            float distance = (transform.position - worm.transform.position).magnitude;
            if (distance > standardDis)
                return true;
            else
                return false;
        }

        //feed gold trap(fire,...)
        //     �� ��ġ ����/ ���� ���� �����ϰ� / ��ġ��X
        //                                      �� ��ġ�� destroy? 
        //                                          �� ��Ģ ����!! , ��ġ ���� �����ؼ� ��ġ�� ���� X


        private void CreateFieldItems()
        {
            //�뷱�� ���� ���� �κ� - ���� ����
            int numFire = Random.Range(0, 3);
            int numGold = Random.Range(0, 3);
            int numFeed = Random.Range(0, 3);

            int numList = 0;

            FieldItemList = new List<GameObject>(numFeed + numFire + numGold);

            for (int i = 0; i < numFire; i++)
            {
                FieldItemList.Add(Instantiate(firePrefab));

                FieldItemList[numList].transform.position = new Vector3(0f, 0f, 0f);
                //������ġ ����. �ٵ� ��Ģ�� �־����

                numList++;
            }
        }

        private void DestroyFieldItems()
        {
            //���� ����
            //�����س��ٰ� �ٽ� �������� �����? -  ������Ʈ Ǯ���̶�� �ϴ���?
            //this.gameObject.SetActive(false);
            for (int i = 0; i < FieldItemList.Count; ++i)
            {
                Destroy(FieldItemList[i]);
            }
            Destroy(this.gameObject);
        }
    }
}