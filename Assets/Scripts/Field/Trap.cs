using UnityEngine;

namespace WiggleQuest
{
    public class Trap : MonoBehaviour
    {
        //Ʈ������ : ��Ÿ�� �������� �޾ƿͼ� ����, ������ X : �ε���������!
        //�� ��ġ���� ������� �Ÿ� ��������, ����!
        private Vector2 thisPosition;

        //���� �޸𸮿� ������ ���������鼭 ������ �帧�� �����ʰ�
        // ���� �Ÿ��� �ִٸ�, ������ġ�� �޾ƿ;���!!����
        [SerializeField] private int fireValue = 5;

        private void OnCollisionEnter(Collision collision)
        {
            //�������̱�
            //��������������??
            //��Destroy(collision.gameObject.����);
            Destroy(this.gameObject);
            Worm worm = collision.gameObject.GetComponent<Worm>();
            if (worm != null)
            {
                //����߰�
                worm.SubtractHeart(fireValue);
            }

            //����Ʈ �߰�
        }
    }
}