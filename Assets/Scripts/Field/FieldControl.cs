using System.Collections.Generic;
using UnityEngine;

namespace WiggleQuest
{
    enum TilePos //Ÿ�� move�� ����
    {
        Up, Down, Left, Right, None//�ʱ�ȭ��
    }

    // Create Tile �̵� �� ��ġ�� ���� Field Tile ����
    public class FieldControl : MonoBehaviour
    {
        //����
        public MapTile mapTileControl; //�̵��Ÿ� ����� ���� ����
        public GameObject fieldTilePrefab;
        public Transform fieldTileParent; //Hierarchy�� �ʵ�Ÿ�� ����

        //[CreateTile ����,����]
        [SerializeField] private float mapSizeMag = 4;          //�ʻ�������� ����
        private float offset;                                   //CreateTile �̵� ũ�� (1����)
        private TilePos tilePOSDir;                             //CreateTile �̵� ����, None�� ��

        public bool isCreateTileMove = false;                   //CreateTile �̵� ����

        //[FieldTile ����, ����]
        private Vector3 createTileCenter;                       //������ġ�� �߾���ǥ

        private Vector3[] creatTilesPos = new Vector3[]         //�ʵ�Ÿ�� ������ġ : �߾���ǥ�κ����� ��ǥ(����)
        { new Vector3(-40f, 0f, 40f), new Vector3(0f, 0f, 40f), new Vector3(40f, 0f, 40f),
          new Vector3(-40f, 0f, 0),                             new Vector3(40f, 0f, 0f) ,
          new Vector3(-40f, 0f, -40f),new Vector3(0f, 0f, -40f),new Vector3(40f, 0f, -40f) };     

        public List<Vector3> fieldTiles = new List<Vector3>();  //FieldTile ��ġ ���� (FieldTile������ ���� ������)
        public float standardDis = 1000f;                       // FieldTile ���� ���� �Ÿ�

        //FieldTile �ϳ��� ������ ���� �ִ� ���� (���� ���� ����)
        public int numFire = 5;
        public int numGold = 1;
        public int numFeed = 4;

        private void Start()
        {
            offset = 10 * mapSizeMag;
            tilePOSDir = TilePos.None;
            createTileCenter = new Vector3(60f, -10f, 60f); //������ġ�� �߾���ǥ

            //[ù ��ġ FieldTile ����] : �߾� 1�� + ���� 8��
            CreateField(createTileCenter);
            CheckNCreate();
        }

        private void Update()
        {
            //none���� �ٽ� �ʱ�ȭ ������ ����X
            if (tilePOSDir == TilePos.None)
            {
                //���� ���� ��ũ���� 4�踸ŭ �������ٸ�?
                if (CheckMoveCount())
                {
                    //CreateTile�� ������ + �󸶳� ����������(Count) �ʱ�ȭ
                    MoveCreateTile();

                    //������ ��ġ �������� �װ��� FieldTile�� �ִ��� Ȯ�� ��, '���ٸ�' ����
                    CheckNCreate();

                    isCreateTileMove = true;
                }
                else
                    isCreateTileMove = false;
            }
        }


         
        bool CheckMoveCount()
        {
            if (mapTileControl.countX >= mapSizeMag)        //�󸶳� ���������� üũ
            {
                tilePOSDir = TilePos.Right;                 //����������� ����
                mapTileControl.countX -= (int)mapSizeMag;   //�� moveī��Ʈ �ʱ�ȭ (ũ�Ⱑ �ٸ�)
                return true;
            }
            else if (mapTileControl.countX <= -mapSizeMag)
            {
                tilePOSDir = TilePos.Left;
                mapTileControl.countX += (int)mapSizeMag;
                return true;
            }
            else if (mapTileControl.countY >= mapSizeMag)
            {
                tilePOSDir = TilePos.Up;
                mapTileControl.countY -= (int)mapSizeMag;
                return true;
            }
            else if (mapTileControl.countY <= -mapSizeMag)
            {
                tilePOSDir = TilePos.Down;
                mapTileControl.countY += (int)mapSizeMag;
                return true;
            }
            else
            {
                return false;
            }
        }

        void MoveCreateTile()
        {
            switch (tilePOSDir) //���⿡���� �̵�
            {
                case TilePos.Up:
                    createTileCenter.z += offset;
                    break;
                case TilePos.Down:
                    createTileCenter.z -= offset;
                    break;

                case TilePos.Right:
                    createTileCenter.x += offset;
                    break;
                case TilePos.Left:
                    createTileCenter.x -= offset;
                    break;
            }

            //���� ���������� �ʱ�ȭ - none���� �缳��
            tilePOSDir = TilePos.None;
        }

        void CheckNCreate()
        {
            for (int i = 0; i < creatTilesPos.Length; i++) //createTile ��ġ ����
            {
                //Ÿ���� ���� ��ġ = ���峻 �߽� + �� ���� ��ġ
                Vector3 eachTilePos = createTileCenter + creatTilesPos[i];

                if (CheckNullField(eachTilePos)) //�ش� ��ġ�� Ÿ���� �ִ��� �˻�
                { 
                    CreateField(eachTilePos); //������ ����
                }
            }
        }

        //true��ȯ�� field Tile ����
        bool CheckNullField(Vector3 newTilePos)
        {
            //����� ��ǥ(������� Ÿ���� ��ǥ)�� �����ϴٸ� ������X
            for (int i = 0; i < fieldTiles.Count; i++)
            {
                if (fieldTiles[i] == newTilePos)
                {
                    return false;
                }
            }

            //����� ��ǥ�� ���ο� ��ǥ�� ������ �����.
            return true;
        }

        void CreateField(Vector3 newTilePos)
        {
            GameObject newTile = Instantiate(fieldTilePrefab, newTilePos, Quaternion.identity, fieldTileParent);
            FieldTile newfieldTile = newTile.GetComponent<FieldTile>();

            if (newfieldTile != null) //����� �����ƴٸ�
            {
                //����Ʈ�� �߰�
                fieldTiles.Add(newTilePos);
            }
        }
    }
}