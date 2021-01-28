using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Manager : MonoBehaviour
    {
        public List<Texture> Images = new List<Texture>();
        public List<GameObject> Items = new List<GameObject>();
        public List<GameObject> AnswerItems = new List<GameObject>();
        private int MismatchItems { get; set; } = 0;

        protected void Start()
        {
            GetItems();
            DeliverATruckOfItems();
            Timer.GetSingleton().StartTimer(Items.Count * 15);
            Timer.GetSingleton().TimeOver += Finish;
        }

        private void GetItems()
        {
            //Resources.LoadAll()
            //Generator.GetItemObject()
        }

        private void DeliverATruckOfItems()
        {

        }

        private void Finish()
        {
            if (MismatchItems == 0 && AnswerItems.Count == Items.Count)
            {
                Win();
            }
            else
            {
                Loser();
            }
            ShowCorrectAnswer();

        }

        private void Win()
        {

        }

        private void Loser()
        {

        }

        private void ShowCorrectAnswer()
        {

        }
    }
}