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
                //¸ñ¼û(·¹º§)°¨¼Ò : ²¿¸®»èÁ¦ 
                worm.SubtractHeart(fireValue);

                //ÀÌÆåÆ® Ãß°¡ : ºÒ²É Æ¢±â±â, ¾ó±¼Âô±×¸®±â
            }
        }
    }
}