using UnityEngine;

namespace WiggleQuest
{
    public class FieldControl : MonoBehaviour
    {
        //�߰�����
        //���� ���� Ÿ�� 'Create Field' �������� Field Tile����
        //��Ÿ���� ��Ÿ�� �����ŭ �����̸�, Create Field�� Move
        //                 �� ������ ���� Field Tile ��ǥ�� �������� Ȯ�� (���� ��ġ ����Ʈ�� ����? - ������ Ž�� �˰��� ã�ƺ���), ������ ����

        enum TilePos
        {
            Upper,      //��
            Right,      //����
            Left,       //��
            Lower       //�Ʒ� 
        }

        // [Field Tile �����κ�]



        //��� ��ǥ�� �˻� - ��ġ�� ��ǥ ����, ���� ������.. ��¼��?

        //upper ��ǥ 4��?
        private float createPosX1;
        private float createPosX2;
        private float createPosY1;
        private float createPosY2;
        //Right ��ǥ 4��?
        //Left  ��ǥ 4��?
        //Lower ��ǥ 4��?


        //������ �������� �ʰ� ���� ȿ������ ����� ã��ʹ�!!!!!!!!!!!!!!!!!!!!!

        /* 
        collider ������ Create false��ȯ?      */

    }
}
