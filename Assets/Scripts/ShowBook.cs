using DataManager;
using Interaction;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class ShowBook : MonoBehaviour
    {
        [SerializeField] private float diffY = 0.3f;
        [SerializeField] private Camera finalCamera = null;
        [SerializeField] private float cameraSpeed = 0.1f;

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
            StartCoroutine(Scrolling());
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
                        correct.gameObject.GetComponent<Item>().EndItem();
                        //Destroy(correct.gameObject.GetComponent<Rigidbody>());
                        correct.transform.position =
                            new Vector3(AnswerObject.transform.position.x - 1.2f,
                                AnswerObject.transform.position.y - diffY * index,
                                AnswerObject.transform.position.z);
                        correct.transform.rotation = Quaternion.Euler(-45,45,0);
                    }
                }


                GameObject answer = null;
                if (ComplaintBook.Answers[index].givenId != -1)
                {
                    answer = Manager.ItemInfos[ComplaintBook.Answers[index].givenId].model;

                    answer = Instantiate(answer);
                    answer.gameObject.GetComponent<Item>().EndItem();
                    //Destroy(answer.gameObject.GetComponent<Rigidbody>());
                    answer.transform.position =
                        new Vector3(AnswerObject.transform.position.x + 1.2f,
                            AnswerObject.transform.position.y - diffY * index,
                            AnswerObject.transform.position.z);
                    answer.transform.rotation = Quaternion.Euler(-45, 45, 0);
                }
            }
        }

        private IEnumerator Scrolling() {
            Vector2 pressPos = default;
            Vector3 pressLocalPos = default;
            while (true) {
                if (Input.GetMouseButtonDown(0)) {
                    pressPos = Input.mousePosition;
                    pressLocalPos = finalCamera.transform.localPosition;
                    print(pressPos);
                }
                if(Input.GetMouseButton(0)) {
                    finalCamera.transform.localPosition = pressLocalPos - Vector3.up * (Input.mousePosition.y - pressPos.y) * cameraSpeed;
                }
                yield return null;
            }
        }
    }
}
