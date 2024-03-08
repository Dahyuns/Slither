using UnityEngine;

namespace WiggleQuest
{
    public class Feed : MonoBehaviour
    {
        [SerializeField] private int feedValue = 100;


        Color[] fourColors = { new Color ( 1, 0,     0,      1), //Red
                               new Color ( 0, 1,     0,      1), //green
                               new Color ( 1, 0.64f, 0,      1), //orange
                               new Color ( 1, 0.92f, 0.016f, 1) }; //yellow

        private void Awake()
        {
           //.SetColor();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SetColor();
            }
        }

        private void SetColor()
        {
            int colorNum = Random.Range(1,fourColors.Length);
            GetComponent<MeshRenderer>().material.color = fourColors[colorNum];
            Debug.Log(GetComponent<MeshRenderer>().material.color + "Feed 컬러변경!");
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);

            Worm worm = collision.gameObject.GetComponent<Worm>();
            if (worm != null)
            {
                //목숨(꼬리)추가
                worm.AddHeart(feedValue);
            }
            //이펙트추가 - 몸통생성시에도
        }
    }
}