using System.Collections;
using Assets.Scripts;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace DataManager
{
    public sealed class Manager : MonoBehaviour
    {

        [SerializeField] private int itemsSpawnCount;
        [SerializeField] private int thiefSpawnCount;
        [SerializeField] private int extraModelsSpawnCount;

        public static UnityAction Fin;

        public static ItemInfo[] ItemInfos { get; set; } = null;

        private static int currentId = -1;
        private static bool finished = false;
        public static bool timerEnded = false;

        private void Start() => StartCoroutine(Scenario(itemsSpawnCount, thiefSpawnCount, extraModelsSpawnCount));

        #region Scenario

        private static IEnumerator Scenario(int itemsSpawnCount, int thiefSpawnCount, int extraModelsSpawnCount)
        {
            ItemInfos = Generator.GenerateItemInfos(itemsSpawnCount, 3, thiefSpawnCount, extraModelsSpawnCount);

            yield return DeliverATruckOfItems();
            CustomerSpawning.CustomerSpawningManager.instance.StartSpawning();
            yield return new WaitForSeconds(1); // TODO: remove
            Timer.TimeOver += Finish;
            AudioManager.OnRepeatMusicStart += () => Timer.instance.StartTimer(ItemInfos.Length * 8);
        }

        private static IEnumerator DeliverATruckOfItems() // todo
        {
            yield return new WaitForSeconds(0.85f); // TODO: remove
            int i = 0;
            int j = 0;
            foreach (var info in ItemInfos)
            {
                if (i == 5)
                {
                    i = 0;
                    j++;
                }

                if (info.model != null)
                {
                    info.model.transform.position = new Vector3(i++ - 8f, 2, j + 5);
                }
            }
            yield return new WaitForSeconds(1); // TODO: remove
        }

        private static void Finish() // todo
        {
            if(finished) { return; }
            finished = true;

            if (ComplaintBook.Mismatches == 0 && ComplaintBook.Size == ItemInfos.Length)
            {
                Win();
            }
            else
            {
                Loser();
            }

            Fin?.Invoke();
        }

        private static void Win() { print("YOU WON, my kickass-MAN!!!"); } // todo

        private static void Loser() { print("you lost, you piece of shit"); } // todo

        #endregion

        public static bool? ItemGiven(GameObject item)
        {
            Computer.instance.SetPainting(new Painting(null, new Color[3])); // TODO

            return ComplaintBook.MakeGuess(item != null ? currentId : -1, item != null ? GetIdOfItem(item) : -1);
        }
        public static void NextItem()
        {
            do
            {
                currentId++;
            } while (currentId < ItemInfos.Length && ItemInfos[currentId].painting == null);
            if (currentId >= ItemInfos.Length)
            {
                Finish();
                return;
            }
            Computer.instance.SetPainting(ItemInfos[currentId].painting);
        }

        private static int GetIdOfItem(GameObject item)
        {
            for (int i = 0; i < ItemInfos.Length; i++)
            {
                if (ItemInfos[i].model == item) { return i; }
            }
            throw new System.ArgumentException("WTF, you trying to find an object not from those, that spawned???!!?!?!!"); // TODO
        }
    }
}