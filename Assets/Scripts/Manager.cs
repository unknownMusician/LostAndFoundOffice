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

        protected void Awake()
        {

        }

        protected void Start()
        {
            GetItemsAndImages();
            DeliverATruckOfItems();
            Timer.GetSingleton().StartTimer(Items.Count * 15);
            Timer.GetSingleton().TimeOver += Finish;
        }

        private void GetItemsAndImages()
        {
            var itemPrefabs = Resources.LoadAll<GameObject>("Prefabs/Items");
            foreach (var o in itemPrefabs)
            {
                Items.Add(Generator.GetItemObject(o));
            }
            var imagePrefabs = Resources.LoadAll<Texture2D>("Images/RGB");
            foreach (var o in imagePrefabs)
            {
                Images.Add(Generator.GetPainting(o));
            }
        }

        private void DeliverATruckOfItems()
        {
            int i = 0;
            int j = 0;
            foreach (GameObject item in Items)
            {
                if (i == 5)
                {
                    i = 0;
                    j++;
                }
                Instantiate(item, new Vector3(i++ - 10, 5, j + 7), Quaternion.identity);
            }
        }

        public Texture2D GetImage()
        {
            return Images[AnswerItems.Count];
        }

        public bool CheckItem(GameObject item)
        {
            AnswerItems.Add(item);
            if (item == Items[AnswerItems.Count - 1])
                return true;
            MismatchItems++;
            return false;
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