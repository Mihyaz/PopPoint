using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour, IMove
{
    public float Angle { get; set; }
    public float RadAngle { get; set; }
    public float RotationSpeed { get; set; }
    public float RotationDegree { get; set; }
    public void PlayAnim()
    {
        throw new System.NotImplementedException();
    }
}
