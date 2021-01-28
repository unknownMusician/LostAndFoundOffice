using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Timer : MonoBehaviour
    {
        public GameObject Time;
        public GameObject Sprite;
        public delegate void TimerDelegate();
        public TimerDelegate TimeOver;
        private static Timer _timer = null;
        private float StartTime { get; set; } = 120;
        private float CurrentTime { get; set; } = 120;

        private Timer()
        {

        }

        public static Timer GetSingleton()
        {
            if (_timer == null)
                _timer = new Timer();
            return _timer;
        }

        protected void Update()
        {
            CurrentTime -= UnityEngine.Time.deltaTime;
            Tick(CurrentTime);
        }


        public void Tick(float currentTime)
        {
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            Time.GetComponent<TextMesh>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
}
