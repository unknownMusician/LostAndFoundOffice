using UnityEngine;


namespace CustomerSpawning
{
    public class Customer : MonoBehaviour
    {
        #region Properties

        private Sprite[] objectImages;              // Список изображений, которые приносят клиенты

        public int ObjectID { get; set; }           // Уникальный айди объекта, который нужно найти (предположительно каждой модели - свой айди)
        public float ObjectTime { get; set; }       // Время на нахождение объекта
        public Sprite ObjectImage { get; set; }     // Картинка объекта, которую приносит клиент

        private bool isOrderMade;

        #endregion

        void Awake()
        {
            objectImages = transform.parent.parent.GetComponent<ObjectImagesController>().images;
        }
        void OnEnable()
        {
            isOrderMade = false;
        }

        #region Methods

        public void ConfigureProperties()
        {
            // Настраиваем клиента случайным образом

            ObjectID = Random.Range(0, 5);
            ObjectTime = Random.Range(10, 20);

            int imageID = Random.Range(0, objectImages.Length);
            ObjectImage = objectImages[imageID];
        }
        public void ConfigureProperties(int objectToFindID, float timeToSearch, Sprite image)
        {
            // Вручную присваеваем значения всем основным переменным клиента

            this.ObjectID = objectToFindID;
            this.ObjectTime = timeToSearch;
            this.ObjectImage = image;
        }

        public void MakeAnOrder()
        {
            if (!isOrderMade)
            {
                Debug.Log("I've made an order!");
                isOrderMade = true;
            }
        }

        #endregion
    }
}
