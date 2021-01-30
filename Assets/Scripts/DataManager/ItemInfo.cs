using UI;
using UnityEngine;

namespace DataManager {
    public sealed class ItemInfo {

        public readonly Painting painting;
        public readonly GameObject model;

        public ItemInfo(Painting painting, GameObject model) {
            this.painting = painting;
            this.model = model;
        }
    }
}