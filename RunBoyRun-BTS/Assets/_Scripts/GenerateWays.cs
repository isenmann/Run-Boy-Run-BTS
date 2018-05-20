using System.Collections.Generic;
using UnityEngine;

public class GenerateWays : MonoBehaviour
{
    public List<GameObject> WaySegments = new List<GameObject>();

    private List<GameObject> WayLayouts = new List<GameObject>();

    private void Start()
    {
        GeneratePossibleLayouts();
    }

    private void GeneratePossibleLayouts()
    {
        WayLayouts.Add(GenerateFullCrossLayout());
        WayLayouts.Add(GenerateTurn());
        WayLayouts.Add(GenerateStraight());
        WayLayouts.Add(GenerateTCrossLayout());

        WayLayouts.ForEach(p => p.SetActive(false));
    }

    private GameObject GenerateStraight()
    {
        GameObject layout = new GameObject("StraightLayout");
        layout.transform.parent = transform;
        layout.transform.transform.localScale = new Vector3(1, 1, 1);
        layout.transform.transform.localPosition = new Vector3(0, 0, 0);
        layout.transform.transform.localRotation = new Quaternion(0, 0, 0, 0);

        WayLayout way = new WayLayout {Name = "StraightLayout"};

        GameObject sidewalk = WaySegments.Find(p => p.tag == "Segment_SidewalkLine");

        var places = new List<Placement>
        {
            new Placement(0, 4, 0),
            new Placement(0, 3, 0),
            new Placement(0, 2, 0),
            new Placement(0, 1, 0),
            new Placement(0, 0, 0),
            new Placement(0, -1, 0),
            new Placement(0, -2, 0),
            new Placement(0, -3, 0),
            new Placement(0, -4, 0),
            new Placement(0, -5, 0)
        };

        for (int i = 0; i < places.Count; i++)
        {
            GameObject obj = Instantiate(sidewalk, layout.transform, false);
            WaySegment seg = new WaySegment(places[i], obj);
            way.Segments.Add(seg);
        }

        way.Segments.ForEach(p => p.Segment.SetActive(true));

        return layout;
    }

    private GameObject GenerateTurn()
    {
        GameObject layout = new GameObject("TurnLayout");
        layout.transform.parent = transform;
        layout.transform.transform.localScale = new Vector3(1, 1, 1);
        layout.transform.transform.localPosition = new Vector3(0, 0, 0);
        layout.transform.transform.localRotation = new Quaternion(0, 0, 0, 0);

        WayLayout way = new WayLayout {Name = "TurnLayout"};

        GameObject sidewalk = WaySegments.Find(p => p.tag == "Segment_SidewalkLine");

        var places = new List<Placement>
        {
            new Placement(0, 4, 0),
            new Placement(0, 3, 0),
            new Placement(0, 2, 0),
            new Placement(0, 1, 0),
            new Placement(-2, 0, 90),
            new Placement(-3, 0, 90),
            new Placement(-4, 0, 90),
            new Placement(-5, 0, 90)
        };

        for (int i = 0; i < places.Count; i++)
        {
            GameObject obj = Instantiate(sidewalk, layout.transform, false);
            WaySegment seg = new WaySegment(places[i], obj);
            way.Segments.Add(seg);
        }

        GameObject corner = WaySegments.Find(p => p.tag == "Segment_Corner");
        GameObject instCorner = Instantiate(corner, layout.transform);
        WaySegment segment = new WaySegment(new Placement(-1, 1, 0), instCorner);
        way.Segments.Add(segment);

        way.Segments.ForEach(p => p.Segment.SetActive(true));

        return layout;
    }

    private GameObject GenerateFullCrossLayout()
    {
        GameObject layout = new GameObject("FullCrossLayout");
        layout.transform.parent = transform;
        layout.transform.transform.localScale = new Vector3(1, 1, 1);
        layout.transform.transform.localPosition = new Vector3(0, 0, 0);
        layout.transform.transform.localRotation = new Quaternion(0, 0, 0, 0);

        WayLayout way = new WayLayout {Name = "FullCrossLayout"};

        GameObject sidewalk = WaySegments.Find(p => p.tag == "Segment_SidewalkLine");

        var places = new List<Placement>
        {
            new Placement(0, 4, 0),
            new Placement(0, 3, 0),
            new Placement(0, 2, 0),
            new Placement(0, 1, 0),
            new Placement(0, -2, 0),
            new Placement(0, -3, 0),
            new Placement(0, -4, 0),
            new Placement(0, -5, 0),
            new Placement(4, 0, 90),
            new Placement(3, 0, 90),
            new Placement(2, 0, 90),
            new Placement(1, 0, 90),
            new Placement(-2, 0, 90),
            new Placement(-3, 0, 90),
            new Placement(-4, 0, 90),
            new Placement(-5, 0, 90)
        };

        for (int i = 0; i < places.Count; i++)
        {
            GameObject obj = Instantiate(sidewalk, layout.transform, false);
            WaySegment seg = new WaySegment(places[i], obj);
            way.Segments.Add(seg);
        }

        GameObject cross = WaySegments.Find(p => p.tag == "Segment_FullCross");
        GameObject instCross = Instantiate(cross, layout.transform);
        WaySegment segment = new WaySegment(new Placement(0, 0, 0), instCross);
        way.Segments.Add(segment);

        way.Segments.ForEach(p => p.Segment.SetActive(true));

        return layout;
    }

    private GameObject GenerateTCrossLayout()
    {
        GameObject layout = new GameObject("TCrossLayout");
        layout.transform.parent = transform;
        layout.transform.transform.localScale = new Vector3(1, 1, 1);
        layout.transform.transform.localPosition = new Vector3(0, 0, 0);
        layout.transform.transform.localRotation = new Quaternion(0, 0, 0, 0);

        WayLayout way = new WayLayout {Name = "TCrossLayout"};

        GameObject sidewalk = WaySegments.Find(p => p.tag == "Segment_SidewalkLine");

        var places = new List<Placement>
        {
            new Placement(0, 4, 0),
            new Placement(0, 3, 0),
            new Placement(0, 2, 0),
            new Placement(0, 1, 0),
            new Placement(0, -2, 0),
            new Placement(0, -3, 0),
            new Placement(0, -4, 0),
            new Placement(0, -5, 0),
            new Placement(4, 0, 90),
            new Placement(3, 0, 90),
            new Placement(2, 0, 90),
            new Placement(1, 0, 90),
        };

        for (int i = 0; i < places.Count; i++)
        {
            GameObject obj = Instantiate(sidewalk, layout.transform, false);
            WaySegment seg = new WaySegment(places[i], obj);
            way.Segments.Add(seg);
        }

        GameObject cross = WaySegments.Find(p => p.tag == "Segment_TCross");
        GameObject instCross = Instantiate(cross, layout.transform);
        WaySegment segment = new WaySegment(new Placement(-1, 0, 0), instCross);
        way.Segments.Add(segment);

        way.Segments.ForEach(p => p.Segment.SetActive(true));

        return layout;
    }

    public GameObject GetRandomWayLayout()
    {
        WayLayouts.ForEach(p => p.SetActive(false));

        GameObject obj = WayLayouts[Random.Range(0, WayLayouts.Count)];
        obj.SetActive(true);
        return obj;
    }
}

public class WayLayout
{
    public List<WaySegment> Segments = new List<WaySegment>();
    public string Name;
}

public class WaySegment
{
    public Placement Placement;
    public GameObject Segment;

    public WaySegment(Placement place, GameObject segment)
    {
        Placement = place;
        Segment = segment;

        Segment.SetActive(true);

        Segment.transform.localPosition = new Vector3(Placement.X, 0, Placement.Z);
        Segment.transform.Rotate(0, Placement.Rotation, 0);
    }
}

public class Placement
{
    public int X;
    public int Z;
    public int Rotation;

    public Placement(int x, int z, int rotation)
    {
        X = x;
        Z = z;
        Rotation = rotation;
    }
}
