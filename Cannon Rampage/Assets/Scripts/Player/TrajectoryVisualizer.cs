using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryVisualizer : MonoBehaviour
{
    //public CurvedLineRenderer curvedLineRenderer;
    public TrajectoryRenderer trajectoryRenderer;
    private void Awake()
    {
        //HideTrajectory();
    }

    public void CanShowTrajectory()
    {
        trajectoryRenderer.CanShow();
    }

    public void ChangeHeight(float newHeight)
    {
        trajectoryRenderer.height = newHeight;
    }

    public void DrawTrajectory(Vector3 pointA,Vector3 pointB)
    {
        trajectoryRenderer.SetPoints(pointA, pointB);
    }

    public void HideTrajectory()
    {
        trajectoryRenderer.HideTrajectory();
    }

    public void ShowTrajectory()
    {
        trajectoryRenderer.ShowTrajectory();
    }
}
