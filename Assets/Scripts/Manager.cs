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
        public List<Texture2D> Images = new List<Texture2D>();
        public List<GameObject> Items = new List<GameObject>();
        public List<GameObject> AnswerItems = new List<GameObject>();
        private int MismatchItems { get; set; } = 0;

        protected void Start()
        {
            GetItemsAndImages();
            DeliverATruckOfItems();
            Timer.GetSingleton().StartTimer(Items.Count * 15);
            Timer.GetSingleton().TimeOver += Finish;
        }

        private void GetItemsAndImages()
        {
            //var itemPrefabs = Resources.LoadAll("Assets/Resources/Prefabs/Items");
            //foreach (var o in itemPrefabs)
            //{
            //    Items.Add(Generator.GetItemObject((GameObject)o,)); 
            //}
            //var imagePrefabs = Resources.LoadAll("Assets/Resources/Prefabs/Items");
            //foreach (var o in imagePrefabs)
            //{
            //    Images.Add(Generator.GetPainting((Texture2D)o,));
            //}
        }

        private void DeliverATruckOfItems()
        {
            foreach (GameObject item in Items)
            {
                Instantiate(item, new Vector3(0, 5, 0), Quaternion.identity);
            }
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