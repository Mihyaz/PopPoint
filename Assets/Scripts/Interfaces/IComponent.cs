using UnityEngine;

public interface IComponent
{
    PolygonCollider2D Collider { get; set; }
    SpriteRenderer Sprite { get; set; }
    GameObject ParticleGameObject_1 { get; set; }
    GameObject ParticleGameObject_2 { get; set; }

    void HandleReset();
    void HandleRestart();
}