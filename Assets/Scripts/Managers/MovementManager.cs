using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour, IMove
{
    public Speeds SpeedType { get; set; }
    public Transform Transform { get; set; }
    public float Angle { get; set; }
    public float RadAngle { get; set; }
    public float RotationSpeed { get; set; }
    public float RotationDegree { get; set; }

    private void Awake()
    {
        Transform = transform;
        SpeedType = new Speeds();
    }

    public void Rotate(SpeedTypes type)
    {
        Transform.Rotate(0, 0, RotationSpeed * Time.deltaTime * SpeedType.Speedies[type] * RotationDegree);
    }

    public void Rotate(int speed)
    {
        throw new System.NotImplementedException();
    }

    public class Speeds
    {
        public Dictionary<SpeedTypes, int> Speedies = new Dictionary<SpeedTypes, int>();
        public Speeds()
        {
            Speedies.Add(SpeedTypes.Player, 15);
            Speedies.Add(SpeedTypes.Turner, 10);
        }
    }
}
