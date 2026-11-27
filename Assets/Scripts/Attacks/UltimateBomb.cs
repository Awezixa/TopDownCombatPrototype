using System;
using UnityEngine;

// bomb flies to player mouse, the calls ultimate so does locke ult thing.
public class UltimateBomb : MonoBehaviour
{
    [SerializeField] private float speed = 12f;

    private Vector2 target;
    private Action<Vector2> onLand;//action for locke ult aka. deal damage, blind and burn

    public void Init(Vector2 targetPoint, Action<Vector2> landCallback)
    {
        target = targetPoint;
        onLand = landCallback;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.Rotate(0f, 0f, 360f * Time.deltaTime);   // spin while in the air

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            onLand?.Invoke(target);
            Destroy(gameObject);
        }
    }
}