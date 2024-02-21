using UnityEngine;

namespace WiggleQuest
{
    public class Feed : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
            //몸통추가
            //이펙트추가 - 몸통생성시에도
        }
    }
}