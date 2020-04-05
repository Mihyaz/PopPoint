using UnityEngine;

public interface IMove
{
    Transform Transform { get; set; }
    float Angle { get; set; }
    float RadAngle { get; set; }
    float RotationSpeed { get; set; }
    float RotationDegree { get; set; }
    void Rotate(SpeedTypes speed);
    MovementManager.Speeds SpeedType { get; set; }
}
