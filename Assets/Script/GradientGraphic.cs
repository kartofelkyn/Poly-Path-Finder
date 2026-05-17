using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
using UnityEngine.U2D;
#endif
/// <summary>
/// A UI graphic that displays a gradient color across its area.
/// </summary>
[RequireComponent(typeof(CanvasRenderer))]
[AddComponentMenu("UI/Gradient Graphic", 12)]
public class GradientGraphic : MaskableGraphic, ILayoutElement
{
    public Sprite sprite;
    public override Texture mainTexture => sprite ? sprite.texture : s_WhiteTexture;

    public Color topLeftColor = Color.white;
    public Color topRightColor = Color.white;
    public Color bottomLeftColor = Color.white;
    public Color bottomRightColor = Color.white;

    [Range(1, 5)]
    public int gradientSmoothness = 1;

    public bool useSlicedSprite = true;
    public bool fillCenter = true;

    public float pixelsPerUnitMultiplier = 1f;

    public float pixelsPerUnit
    {
        get
        {
            float spritePixelsPerUnit = sprite ? sprite.pixelsPerUnit : 100;
            float referencePixelsPerUnit = canvas ? canvas.referencePixelsPerUnit : 100;
            return pixelsPerUnitMultiplier * spritePixelsPerUnit / referencePixelsPerUnit;
        }
    }

    public bool hasBorder => sprite && sprite.border.sqrMagnitude > 0f;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        Rect rect = GetPixelAdjustedRect();
        float width = rect.width;
        float height = rect.height;

        Vector4 uv = sprite ? DataUtility.GetOuterUV(sprite) : Vector4.zero;
        Vector2 pivot = rectTransform.pivot;

        float x0 = -width * pivot.x;
        float y0 = -height * pivot.y;

        vh.AddVert(new Vector3(x0, y0), bottomLeftColor, new Vector2(uv.x, uv.y));
        vh.AddVert(new Vector3(x0, y0 + height), topLeftColor, new Vector2(uv.x, uv.w));
        vh.AddVert(new Vector3(x0 + width, y0 + height), topRightColor, new Vector2(uv.z, uv.w));
        vh.AddVert(new Vector3(x0 + width, y0), bottomRightColor, new Vector2(uv.z, uv.y));

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }

    int ILayoutElement.layoutPriority => 0;
    float ILayoutElement.minWidth => 0;
    float ILayoutElement.minHeight => 0;
    float ILayoutElement.flexibleWidth => -1;
    float ILayoutElement.flexibleHeight => -1;

    float ILayoutElement.preferredWidth => sprite ? DataUtility.GetMinSize(sprite).x / pixelsPerUnit : 0;
    float ILayoutElement.preferredHeight => sprite ? DataUtility.GetMinSize(sprite).y / pixelsPerUnit : 0;

    void ILayoutElement.CalculateLayoutInputHorizontal() { }
    void ILayoutElement.CalculateLayoutInputVertical() { }
}