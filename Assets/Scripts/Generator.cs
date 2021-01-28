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
        var modelsList = new List<GameObject>();
        string path = "Prefabs/Items/{0}";
        string[] diffs = new[] { "Backpack", "Ball", "Book", "Case", "Cube", "Drum", "Laptop", "Phone" , "Ring", "Shield", "Sword", "ToyTrack" };
        foreach (string diff in diffs) {
            modelsList.Add(Resources.Load<GameObject>(string.Format(path, diff)));
        }
        return modelsList.ToArray();
    }
    private static Texture2D[] LoadRgbs() {
        var texturesList = new List<Texture2D>();
        string path = "Images/RGB/{0}RGB";
        string[] diffs = new[] { "Backpack", "Ball", "Book", "Case", "Cube", "Drum", "Laptop", "Phone", "Ring", "Shield", "Sword", "ToyTrack" };
        foreach (string diff in diffs) {
            texturesList.Add(Resources.Load<Texture2D>(string.Format(path, diff)));
        }
        return texturesList.ToArray();
    }

    private static Texture2D GetRandomPainting(Texture2D[] rgbs, Color[] colors) {
        // todo
        return rgbs[0];
    }

    private static GameObject GetRandomModel(GameObject[] models, Material[] materials) {
        // todo
        return models[0];
    }

    public static ItemInfo[] GenerateItemInfos(int count) {
        var itemInfoList = new List<ItemInfo>();

        var colors = LoadColors();
        var materials = LoadMaterials();
        var models = LoadModels();
        var rgbs = LoadRgbs();

        for(int i = 0; i < count; i++) {
            itemInfoList.Add(new ItemInfo(GetRandomPainting(rgbs, colors), GetRandomModel(models, materials)));
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