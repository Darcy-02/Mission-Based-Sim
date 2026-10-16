using UnityEngine;

public class GravityFieldRings : MonoBehaviour
{
    [Header("Gravity Sources")]
    public Transform gravitySource1;
    public Transform gravitySource2;

    [Header("Big Mass Field")]
    public int bigRingCount = 10;
    public float bigFirstRadius = 1f;
    public float bigRingSpacing = 1.5f;

    [Header("Small Mass Field")]
    public int smallRingCount = 5;
    public float smallFirstRadius = 0.5f;
    public float smallRingSpacing = 0.6f;

    [Header("Ring Detail")]
    public int pointsPerRing = 96;
    public float spacingPower = 2f;

    [Header("Appearance")]
    public Color lineColor = Color.cyan;
    public float lineWidth = 0.025f;

    [Header("Fade")]
    public float innerAlpha = 1f;
    public float outerAlpha = 0.05f;

    public Color innerColor = Color.cyan;
    public Color outerColor =
        new Color(0f, 0.1f, 0.4f, 1f);

    [Header("Ring Thickness")]
    public float innerWidth = 0.025f;
    public float outerWidth = 0.006f;

    [Header("Radial Lines")]
    public int radialLineCount = 32;
    public int pointsPerRadialLine = 80;
    public float radialLineWidth = 0.008f;

    [Header("Surface")]
    public LayerMask surfaceLayer;
    public float rayHeight = 20f;
    public float surfaceOffset = 0.08f;

    

    private LineRenderer[] rings1;
    private LineRenderer[] rings2;

    private LineRenderer[] radialLines1;
    private LineRenderer[] radialLines2;

    

    void Start()
    {
        CreateRings();
        CreateRadialLines();
        ApplyFade();
    }

    

    void LateUpdate()
    {
        
        UpdateRings(
            gravitySource1,
            rings1,
            bigRingCount,
            bigFirstRadius,
            bigRingSpacing
        );

        
        UpdateRings(
            gravitySource2,
            rings2,
            smallRingCount,
            smallFirstRadius,
            smallRingSpacing
        );

        
        float bigMaximumRadius =
            bigFirstRadius +
            bigRingSpacing *
            (bigRingCount - 1);

        UpdateRadialLines(
            gravitySource1,
            radialLines1,
            bigMaximumRadius
        );

        
        float smallMaximumRadius =
            smallFirstRadius +
            smallRingSpacing *
            (smallRingCount - 1);

        UpdateRadialLines(
            gravitySource2,
            radialLines2,
            smallMaximumRadius
        );
    }

    

    void CreateRings()
    {
        rings1 =
            new LineRenderer[bigRingCount];

        rings2 =
            new LineRenderer[smallRingCount];

       
        for (int i = 0; i < bigRingCount; i++)
        {
            rings1[i] =
                CreateRing(
                    "Big Gravity Ring - " + i
                );
        }

        
        for (int i = 0; i < smallRingCount; i++)
        {
            rings2[i] =
                CreateRing(
                    "Small Gravity Ring - " + i
                );
        }
    }

    
    LineRenderer CreateRing(string lineName)
    {
        GameObject ringObject =
            new GameObject(lineName);

        ringObject.transform.SetParent(
            transform,
            false
        );

        LineRenderer line =
            ringObject.AddComponent<LineRenderer>();

        line.useWorldSpace = true;
        line.loop = true;

        line.positionCount =
            pointsPerRing;

        line.numCornerVertices = 4;
        line.numCapVertices = 4;

        line.startWidth =
            innerWidth;

        line.endWidth =
            outerWidth;

        line.startColor =
            lineColor;

        line.endColor =
            lineColor;

        line.material =
            new Material(
                Shader.Find("Sprites/Default")
            );

        line.material.color =
            lineColor;

        return line;
    }

    

    void CreateRadialLines()
    {
        radialLines1 =
            new LineRenderer[
                radialLineCount
            ];

        radialLines2 =
            new LineRenderer[
                radialLineCount
            ];

        
        for (int i = 0; i < radialLineCount; i++)
        {
            radialLines1[i] =
                CreateRadialLine(
                    "Big Radial Line - " + i
                );
        }

        
        for (int i = 0; i < radialLineCount; i++)
        {
            radialLines2[i] =
                CreateRadialLine(
                    "Small Radial Line - " + i
                );
        }
    }

    

    LineRenderer CreateRadialLine(
        string lineName)
    {
        GameObject obj =
            new GameObject(lineName);

        obj.transform.SetParent(
            transform,
            false
        );

        LineRenderer line =
            obj.AddComponent<LineRenderer>();

        line.useWorldSpace = true;

        line.positionCount =
            pointsPerRadialLine;

        line.startWidth =
            radialLineWidth;

        line.endWidth =
            radialLineWidth;

        line.startColor =
            lineColor;

        line.endColor =
            lineColor;

        line.numCornerVertices = 4;
        line.numCapVertices = 4;

        line.material =
            new Material(
                Shader.Find("Sprites/Default")
            );

        line.material.color =
            lineColor;

        return line;
    }

    

    void ApplyFade()
    {
        ApplyRingFade(rings1);
        ApplyRingFade(rings2);
    }

    void ApplyRingFade(
        LineRenderer[] rings)
    {
        if (rings == null)
            return;

        for (int i = 0; i < rings.Length; i++)
        {
            float t =
                (float)i /
                Mathf.Max(
                    1,
                    rings.Length - 1
                );

            Color color =
                Color.Lerp(
                    innerColor,
                    outerColor,
                    t
                );

            color.a =
                Mathf.Lerp(
                    innerAlpha,
                    outerAlpha,
                    t
                );

            float width =
                Mathf.Lerp(
                    innerWidth,
                    outerWidth,
                    t
                );

            rings[i].startColor =
                color;

            rings[i].endColor =
                color;

            rings[i].startWidth =
                width;

            rings[i].endWidth =
                width;
        }
    }

    

    void UpdateRings(
        Transform source,
        LineRenderer[] rings,
        int ringCount,
        float firstRadius,
        float ringSpacing)
    {
        if (source == null)
            return;

        if (rings == null)
            return;

        for (int r = 0;
             r < ringCount;
             r++)
        {
            float normalized =
                (float)r /
                Mathf.Max(
                    1,
                    ringCount - 1
                );

            float curvedSpacing =
                Mathf.Pow(
                    normalized,
                    spacingPower
                );

            float radius =
                firstRadius +
                curvedSpacing *
                ringSpacing *
                (ringCount - 1);

            for (int i = 0;
                 i < pointsPerRing;
                 i++)
            {
                float angle =
                    (float)i /
                    pointsPerRing *
                    Mathf.PI * 2f;

                Vector3 position =
                    GetSurfacePoint(
                        source,
                        angle,
                        radius
                    );

                rings[r].SetPosition(
                    i,
                    position
                );
            }
        }
    }

    

    void UpdateRadialLines(
        Transform source,
        LineRenderer[] radialLines,
        float maximumRadius)
    {
        if (source == null)
            return;

        if (radialLines == null)
            return;

        for (int r = 0;
             r < radialLineCount;
             r++)
        {
            float angle =
                (float)r /
                radialLineCount *
                Mathf.PI * 2f;

            for (int i = 0;
                 i < pointsPerRadialLine;
                 i++)
            {
                float t =
                    (float)i /
                    Mathf.Max(
                        1,
                        pointsPerRadialLine - 1
                    );

                float radius =
                    t * maximumRadius;

                Vector3 position =
                    GetSurfacePoint(
                        source,
                        angle,
                        radius
                    );

                radialLines[r].SetPosition(
                    i,
                    position
                );
            }
        }
    }

    

    Vector3 GetSurfacePoint(
        Transform source,
        float angle,
        float radius)
    {
        float x =
            source.position.x +
            Mathf.Cos(angle) *
            radius;

        float z =
            source.position.z +
            Mathf.Sin(angle) *
            radius;

        
        Vector3 rayOrigin =
            new Vector3(
                x,
                transform.position.y + rayHeight,
                z
            );

        if (Physics.Raycast(
            rayOrigin,
            Vector3.down,
            out RaycastHit hit,
            rayHeight * 2f,
            surfaceLayer,
            QueryTriggerInteraction.Ignore))
        {
            
            return hit.point +
                   hit.normal *
                   surfaceOffset;
        }

        
        return new Vector3(
            x,
            transform.position.y,
            z
        );
    }
}