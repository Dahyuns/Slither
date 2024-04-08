using UnityEngine;
using System;

namespace WiggleQuest
{
    /// <summary>
    /// 이벤트용 클래스
    /// </summary>
    public class ResetGame : MonoBehaviour
    {
        //이벤트
        public static event Action Reset;

        public static void PerformReset()
        {
            Reset?.Invoke();
        }
    }
}