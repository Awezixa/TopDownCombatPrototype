using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject soldierPrefab;
    [SerializeField]
    private GameObject magePrefab;
    [SerializeField]
    private GameObject shieldPrefab;

    [SerializeField]
    private Transform spawnPoint; // optional: where enemies appear

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.digit1Key.wasPressedThisFrame)
        {
            Spawn(soldierPrefab);
        }
        else if (keyboard.digit2Key.wasPressedThisFrame)
        {
            Spawn(magePrefab);
        }
        else if (keyboard.digit3Key.wasPressedThisFrame)
        {
            Spawn(shieldPrefab);
        }
    }

    private void Spawn(GameObject prefab)
    {
        if (prefab == null) return;

        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        Instantiate(prefab, position, Quaternion.identity);
    }






}
