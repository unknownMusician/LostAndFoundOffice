using UnityEngine;

public sealed class Painting {

    public readonly Texture2D rgbTexture;
    public readonly Color[] colors;

    public Painting(Texture2D rgbTexture, Color[] colors) {
        this.rgbTexture = rgbTexture;
        this.colors = colors;
    }
}
