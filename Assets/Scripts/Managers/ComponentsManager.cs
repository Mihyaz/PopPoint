using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsManager : MonoBehaviour, IComponent
{
    public PolygonCollider2D Collider { get; set; }
    public SpriteRenderer Sprite { get; set; }
    public GameObject ParticleGameObject_1 { get; set; }
    public GameObject ParticleGameObject_2 { get; set; }

    private void Awake()
    {
        Collider = GetComponent<PolygonCollider2D>();
        Sprite = GetComponent<SpriteRenderer>();
        ParticleGameObject_1 = GameObject.FindGameObjectWithTag("PS_1");
        ParticleGameObject_2 = GameObject.FindGameObjectWithTag("PS_2");
        Init();
    }

    public void HandleReset()
    {
        Sprite.enabled = false;
        Collider.enabled = false;
        ParticleGameObject_1.SetActive(true);
        ParticleGameObject_2.SetActive(true);
    }
    public void HandleRestart()
    {
        Sprite.enabled = true;
        Collider.enabled = true;
    }
    public void Init()
    {
        var ps_1 = ParticleGameObject_1.GetComponent<ParticleSystem>().main;
        var ps_2 = ParticleGameObject_2.GetComponent<ParticleSystem>().main;

        ps_1.playOnAwake = true;
        ps_1.playOnAwake = true;

        ParticleGameObject_1.SetActive(false);
        ParticleGameObject_2.SetActive(false);
    }
}
