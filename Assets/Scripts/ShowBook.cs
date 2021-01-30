using DataManager;
using Interaction;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class ShowBook : MonoBehaviour
    {
        public GameObject AnswerObject;
        public static ShowBook Instance { get; private set; } = null;
        private ShowBook() => Instance = this;

        private void Awake() => Manager.Fin += ShowAll;

        public void ShowAll()
        {
            for (int i = 0; i < ComplaintBook.Answers.Length; i++)
            {
                ShowNext(i);
            }
        }

        public void ShowNext(int index)
        {
            if (index < ComplaintBook.Answers.Length)
            {
                var ind = ComplaintBook.Answers[index].rightId;
                if (ind != -1)
                {
                    GameObject correct = Manager.ItemInfos[ind].model;
                    if (correct != null)
                    {
                        correct = Instantiate(correct);
                        Destroy(correct.gameObject.GetComponent<Item>());
                        //Destroy(correct.gameObject.GetComponent<Rigidbody>());
                        correct.transform.position =
                            new Vector3(AnswerObject.transform.position.x - 1.2f,
                                AnswerObject.transform.position.y - 0.3f * index,
                                AnswerObject.transform.position.z);
                        correct.transform.rotation = Quaternion.Euler(-45,45,0);
                    }
                }


                GameObject answer = null;
                if (ComplaintBook.Answers[index].givenId != -1)
                {
                    answer = Manager.ItemInfos[ComplaintBook.Answers[index].givenId].model;
                    answer = Instantiate(answer);
                    Destroy(answer.gameObject.GetComponent<Item>());
                    //Destroy(answer.gameObject.GetComponent<Rigidbody>());
                    answer.transform.position =
                        new Vector3(AnswerObject.transform.position.x + 1.2f,
                            AnswerObject.transform.position.y - 0.3f * index,
                            AnswerObject.transform.position.z);
                    answer.transform.rotation = Quaternion.Euler(-45, 45, 0);
                }
            }
        }
    }
}
