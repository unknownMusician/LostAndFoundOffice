using System.Collections.Generic;
using UI;
using UnityEngine;

namespace DataManager {
    public static class Generator {

        private static List<ItemInfo> items;

        public static ItemInfo[] GenerateItemInfos(int count, int colorsForEach) {
            var itemInfoList = new List<ItemInfo>();

            // Load
            var colors = LoadColors();
            var materials = LoadMaterials();
            var models = LoadModels();
            var rgbs = LoadRgbs();

            // Generate
            var rawInfos = GetRandomRawInfos(count, models.Length, colorsForEach, materials.Length);

            // Assign
            for (int i = 0; i < count; i++) {
                itemInfoList.Add(new ItemInfo(GetPainting(rawInfos[i], rgbs, colors), GetModel(rawInfos[i], models, materials)));
            }

            return itemInfoList.ToArray();
        }

        #region Load

        private static Color[] LoadColors() {
            return new[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta, Color.black };
        }
        private static Material[] LoadMaterials() {
            var materialsList = new List<Material>();
            string path = "Materials/ItemMaterials/Item{0}Material";
            string[] diffs = new[] { "Red", "Yellow", "Green", "Cyan", "Blue", "Magenta", "Black" };
            foreach (string diff in diffs) {
                materialsList.Add(Resources.Load<Material>(string.Format(path, diff)));
            }
            return materialsList.ToArray();
        }
        private static GameObject[] LoadModels() {
            var modelsList = new List<GameObject>();
            string path = "Prefabs/Items/{0}";
            string[] diffs = new[] { "Backpack", "Ball", "Book", "Case", "Cube", "Drum", "Laptop", "Phone", "Ring", "Shield", "Sword", "ToyTrack" };
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

        #endregion

        #region Generate

        private static ItemRawInfo[] GetRandomRawInfos(int objectsCountNeeded, int objectsCountAll, int colorsCountNeededForEach, int colorsCountAll) {
            var rawInfoList = new List<ItemRawInfo>();

            bool @break = false;
            for (int i = 1; ; i++) {
                int needMore;
                if (objectsCountAll * i > objectsCountNeeded) {
                    needMore = objectsCountNeeded - objectsCountAll * (i - 1);
                    @break = true;
                } else {
                    needMore = objectsCountAll;
                }
                rawInfoList.AddRange(GetRandomRawInfos1Iter(needMore, objectsCountAll, colorsCountNeededForEach, colorsCountAll));
                if (@break) { break; }
            }

            return rawInfoList.ToArray();
        }

        private static ItemRawInfo[] GetRandomRawInfos1Iter(int objectsCountNeeded, int objectsCountAll, int colorsCountNeededForEach, int colorsCountAll) {
            var rawInfoList = new List<ItemRawInfo>();

            int[] objectIds = GetRandomIndexes(objectsCountAll, objectsCountNeeded);

            for (int i = 0; i < objectsCountNeeded; i++) {
                rawInfoList.Add(new ItemRawInfo(objectIds[i], GetRandomIndexes(colorsCountAll, colorsCountNeededForEach)));
            }

            return rawInfoList.ToArray();
        }

        private static int[] GetRandomIndexes(int objectsCountAll, int objectsCountNeeded) {
            List<int> indexes = new List<int>();
            while (indexes.Count < objectsCountNeeded) {
                int index = Mathf.FloorToInt(Random.Range(0, objectsCountAll - 0.01f));
                if (!indexes.Contains(index)) { indexes.Add(index); }
            }
            return indexes.ToArray();
        }

        private sealed class ItemRawInfo {

            public readonly int objectId;
            public readonly int[] colorIds;

            public ItemRawInfo(int objectId, int[] colorIds) {
                this.objectId = objectId;
                this.colorIds = colorIds;
            }
        }

        #endregion

        #region Assign

        private static Painting GetPainting(ItemRawInfo rawInfo, Texture2D[] rgbs, Color[] colors) {
            var cols = new List<Color>();
            foreach (int i in rawInfo.colorIds) {
                cols.Add(colors[i]);
            }
            return new Painting(rgbs[rawInfo.objectId], cols.ToArray());
        }

        private static GameObject GetModel(ItemRawInfo rawInfo, GameObject[] models, Material[] materials) {
            // TODO: check if material changes (it actually should)
            var obj = Object.Instantiate(models[rawInfo.objectId]);
            var meshRenderer = obj.GetComponent<MeshRenderer>();
            var modifiedMaterials = meshRenderer.materials;

            string[] colors = new[] { "ItemRedMaterial", "ItemGreenMaterial", "ItemBlueMaterial" };

            for (int i = 0; i < rawInfo.colorIds.Length; i++) {
                int index = GetMaterialIndex(modifiedMaterials, colors[i]);
                if (index != -1) {
                    modifiedMaterials[index] = materials[rawInfo.colorIds[i]];
                }
            }
            meshRenderer.materials = modifiedMaterials;
            return obj;
        }

        #endregion

        private static int GetMaterialIndex(Material[] mats, string name) {
            for (int i = 0; i < mats.Length; i++) {
                if (mats[i].name.Contains(name)) { return i; }
            }
            return -1;
        }
    }
}