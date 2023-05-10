using UnityEngine;

public class CurvedLineRenderer : MonoBehaviour 
{
	//PUBLIC
	public float height = 2f;
	public float lineSegmentSize = 0.15f;
	//PRIVATE
	CurvedLinePoint[] linePoints;
	public LineRenderer lineRenderer;

    private void Awake()
    {
		linePoints = new CurvedLinePoint[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			linePoints[i] = transform.GetChild(i).gameObject.GetComponent<CurvedLinePoint>();
		}
	}

	public void CanShow()
    {
		canUpdate = true;
    }

	private bool canUpdate = false;

    public void Update () 
	{
		if (canUpdate)
        {
			Vector3[] smoothedPoints = LineSmoother.SmoothLine(new Vector3[] { linePoints[0].transform.position,
			linePoints[1].transform.position, linePoints[2].transform.position }, lineSegmentSize);

			//set line settings
			lineRenderer.positionCount = smoothedPoints.Length;
			lineRenderer.SetPositions(smoothedPoints);
		}
	}

	public void SetPoints(Vector3 pointA,Vector3 pointB)
    {
		if (canUpdate == false || linePoints == null)
			return;

		linePoints[0].transform.position = pointA;
		linePoints[1].transform.position = Vector3.Lerp(pointA, pointB, 0.5f) + Vector3.up * height;
		linePoints[2].transform.position = pointB;
    }
}
