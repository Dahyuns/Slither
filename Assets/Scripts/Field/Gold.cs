using UnityEngine;

namespace WiggleQuest
{
    public class Gold : MonoBehaviour
    {
        [SerializeField] private int goldValue = 100;

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
            Worm worm = collision.gameObject.GetComponent<Worm>();
            if (worm != null)
            {
                //∞ÒµÂ√ﬂ∞°
                worm.AddGold(goldValue);
            }

            //¿Ã∆Â∆Æ √ﬂ∞°
        }
    }
}