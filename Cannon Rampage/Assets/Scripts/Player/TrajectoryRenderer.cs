using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
	public float height = 2f;
	public float lineSegmentSize = 0.75f;
	private CurvedLinePoint[] linePoints;

	public GameObject trajectoryPoint;
	private GameObject[] points = new GameObject[250];
	private void Awake()
	{
		trajectoryPoint.SetActive(false);

        for (int i = 0; i < points.Length; i++)
        {
			points[i] = Instantiate(trajectoryPoint);
			points[i].transform.parent = transform;
        }

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

	public void Update()
	{
		if (canUpdate)
		{
			Vector3[] smoothedPoints = LineSmoother.SmoothLine(new Vector3[] { linePoints[0].transform.position,
			linePoints[1].transform.position, linePoints[2].transform.position }, lineSegmentSize);

            for (int i = 0; i < points.Length; i++)
            {
				if (i < smoothedPoints.Length)
                {
					points[i].transform.position = smoothedPoints[i];
					points[i].SetActive(true);
				}
				else
                {
					points[i].SetActive(false);
                }
            }
		}
	}

	public void HideTrajectory()
    {
		canUpdate = false;

		for (int i = 0; i < points.Length; i++)
		{
			if (points[i] != null)
				points[i].SetActive(false);
		}
	}

	public void ShowTrajectory()
	{
		canUpdate = true;
	}

	public void SetPoints(Vector3 pointA, Vector3 pointB)
	{
		if (canUpdate == false || linePoints == null)
			return;

		linePoints[0].transform.position = pointA;
		linePoints[2].transform.position = pointB;

		pointA.y = 0.0f;
		pointB.y = 0.0f;

		linePoints[1].transform.position = Vector3.Lerp(pointA, pointB, 0.5f) + Vector3.up * height;
	}
}
