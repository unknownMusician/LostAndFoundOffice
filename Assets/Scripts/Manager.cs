using UnityEngine;

public sealed class Manager : MonoBehaviour {

    private static ItemInfo[] ItemInfos { get; set; } = null;

    private static int currentId = 0;

    private void Start() => Scenario();

    #region Scenario

    private static void Scenario() {
        ItemInfos = Generator.GenerateItemInfos(10, 2);
        print(ItemInfos);
        DeliverATruckOfItems();
        Timer.instance.TimeOver += Finish;
        Timer.instance.StartTimer(ItemInfos.Length * 15);
    }

    private static void DeliverATruckOfItems() // todo
    {
        int i = 0;
        int j = 0;
        foreach (var info in ItemInfos) {
            if (i == 5) {
                i = 0;
                j++;
            }
            info.model.transform.position = new Vector3(i++ - 10, 5, j + 7);
        }
    }

    private static void Finish() // todo
    {
        if (ComplaintBook.Mismatches == 0 && ComplaintBook.Size == ItemInfos.Length) {
            Win();
        } else {
            Loser();
        }
        ShowCorrectAnswer();
    }

    private static void Win() { print("YOU WON, my kickass-MAN!!!"); } // todo

    private static void Loser() { print("you lost, you piece of shit"); } // todo

    private static void ShowCorrectAnswer() { } // todo

    #endregion

    public static bool? ItemGiven(GameObject item) {
        Computer.instance.SetPainting(new Painting(null, new Color[3])); // TODO

        return ComplaintBook.MakeGuess(currentId++, item != null ? GetIdOfItem(item) : -1);
    }
    public static void NextItem() {
        currentId++;
        if (currentId >= ItemInfos.Length) {
            Finish();
            return;
        }
        Computer.instance.SetPainting(ItemInfos[currentId].painting);
    }

    private static int GetIdOfItem(GameObject item) {
        for (int i = 0; i < ItemInfos.Length; i++) {
            if (ItemInfos[i].model == item) { return i; }
        }
        throw new System.ArgumentException("WTF, you trying to find an object not from those, that spawned???!!?!?!!"); // TODO
    }
}