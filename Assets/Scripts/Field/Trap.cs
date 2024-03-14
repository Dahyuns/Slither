using UnityEngine;

namespace WiggleQuest
{
    public class Trap : MonoBehaviour
    {
        private Vector2 thisPosition;

        [SerializeField] private int fireValue = 5;

        private void OnTriggerEnter(Collider collision)
        {
            Worm worm = collision.gameObject.GetComponent<Worm>();
            if (worm != null)
            {
                //���(����)���� : �������� 
                worm.SubtractHeart(fireValue);

                //����Ʈ �߰� : �Ҳ� Ƣ���, �����׸���
            }
        }
    }
}