using System.Collections.Generic;
using UnityEngine;

public static class Generator {

    private static List<ItemInfo> items;

    private static Color[] LoadColors() {
        return new[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta, Color.black, Color.white };
    }
    private static Material[] LoadMaterials() {
        var materialsList = new List<Material>();
        string path = "Materials/ItemMaterials/Item{0}Material";
        string[] diffs = new[] { "Red", "Yellow", "Green" , "Cyan" , "Blue" , "Magenta" , "Black" , "White" };
        foreach(string diff in diffs) {
            materialsList.Add(Resources.Load<Material>(string.Format(path, diff)));
        }
        return materialsList.ToArray();
    }
    private static GameObject[] LoadModels() {
        // todo
        return new GameObject[0];
    }
    private static Texture2D[] LoadRgbs() {
        // todo
        return new Texture2D[0];
    }

    private static Texture2D GetPainting(Texture2D rgbPainting, params Color[] colors) {
        // todo
        return rgbPainting;
    }

    private static GameObject GetModel(GameObject model, params Material[] materials) {
        // todo
        return model;
    }

    public static ItemInfo[] GetItemInfos(int count) {
        var itemInfoList = new List<ItemInfo>();

        for(int i = 0; i < count; i++) {
            itemInfoList.Add(new ItemInfo(GetPainting(), GetModel()));
        }

        return itemInfoList.ToArray();
    }

    public sealed class ItemInfo {

        public readonly Texture painting;
        public readonly GameObject model;

        public ItemInfo(Texture painting, GameObject model) {
            this.painting = painting;
            this.model = model;
        }
    }
}