public interface IMove
{
    void PlayAnim();
    float Angle { get; set; }
    float RadAngle { get; set; }
    float RotationSpeed { get; set; }
    float RotationDegree { get; set; }
}
