using System.Collections.Generic;
using UnityEngine;

public sealed class Manager : MonoBehaviour {

    #region Instance

    public static Manager instance = null;
    private void Awake() => instance = this;
    private void OnDestroy() => instance = null;

    #endregion

    private static ItemInfo[] ItemInfos { get; set; } = null;

    private static int currentId = 0;

    private void Start() => Scenario();

    #region Scenario

    private void Scenario() {
        GenerateItemInfos();
        DeliverATruckOfItems();
        Timer.Singleton.TimeOver += Finish;
        Timer.Singleton.StartTimer(ItemInfos.Length * 15);
    }

    private void GenerateItemInfos() => ItemInfos = Generator.GenerateItemInfos(10, 2);

    private void DeliverATruckOfItems() // todo
    {
        int i = 0;
        int j = 0;
        foreach (var info in ItemInfos) {
            if (i == 5) {
                i = 0;
                j++;
            }
            Instantiate(info.model, new Vector3(i++ - 10, 5, j + 7), Quaternion.identity);
        }
    }

    private void Finish() // todo
    {
        if (ComplaintBook.Mismatches == 0 && ComplaintBook.Size == ItemInfos.Length) {
            Win();
        } else {
            Loser();
        }
        ShowCorrectAnswer();
    }

    private void Win() { } // todo

    private void Loser() { } // todo

    private void ShowCorrectAnswer() { } // todo

    #endregion

    public static Texture2D GetImage() // TODO: What for?!
    {
        // TODO: return current image, but ↑↑↑
        throw new System.NotImplementedException();
    }

    public static bool CheckItem(GameObject item) => ComplaintBook.MakeGuess(currentId++, GetIdOfItem(item));
    public static void NextItem() => currentId++;

    private static int GetIdOfItem(GameObject item) {
        for(int i = 0; i < ItemInfos.Length;i++) {
            if(ItemInfos[i].model == item) { return i; }
        }
        throw new System.ArgumentException("WTF, you trying to find an object not from those, that spawned???!!?!?!!"); // TODO
    }
}