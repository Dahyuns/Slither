using UnityEngine;

namespace WiggleQuest
{
    public class Gold : MonoBehaviour
    {
        //참조
        private GameObject worm;

        [SerializeField] private int goldValue = 100;

        private void Awake()
        {
            worm = GameObject.Find("PlayerLook");
        }

        private void Update()
        {
            Vector3 dir = worm.transform.position - this.transform.position;

            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            this.transform.rotation = rot*Quaternion.Euler(50,0,0) ;
        }


        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
            Worm worm = collision.gameObject.GetComponent<Worm>();
            if (worm != null)
            {
                //골드추가
                worm.AddGold(goldValue);
            }

            //이펙트 추가
        }
    }
}