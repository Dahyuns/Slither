using System.Collections;
using UnityEngine;

namespace WiggleQuest
{
    public class CameraController : MonoBehaviour
    {
        //������ : ���� ��ü 
        public GameObject worm;

        private void Update()
        {
            //worm ���� ������
            this.transform.position = new Vector3(worm.transform.position.x, transform.position.y, worm.transform.position.z);      
        }

        //public void ShackeCamera(float intencity, float duration)
        //{
        //    StartCoroutine(Shake(intencity, duration));
        //}
        //
        //IEnumerator Shake(float intencity, float duration)
        //{
        //    Vector3 thisPos = transform.position;
        //    float countdown = 0f;
        //
        //    while (countdown < duration)
        //    {
        //        float x = 
        //
        //        countdown += Time.deltaTime;
        //        yield return null;
        //    }
        //
        //
        //    yield return null;
        //}
    }
}