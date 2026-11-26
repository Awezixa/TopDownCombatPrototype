using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject magePrefab;
    [SerializeField] private GameObject shieldPrefab; 
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private float spacing = 3f;   // distance between enemies spawn positions
    [SerializeField] private int slotCount = 5;    // how many slots in the row

    private int nextSlot;


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


    private Vector3 GetSlotPosition(int index)
    {
        Vector3 basePosition = spawnPoint != null ? spawnPoint.position : transform.position;

        // centre the row on the spawn point
        float offsetX = (index - (slotCount - 1) * 0.5f) * spacing;
        return basePosition + new Vector3(offsetX, 0f, 0f);
    }

    private void Spawn(GameObject prefab)
    {
        if (prefab == null) return;

        Instantiate(prefab, GetSlotPosition(nextSlot), Quaternion.identity);//instantiates the selected prefab to the next available spot
        nextSlot = (nextSlot + 1) % slotCount;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < slotCount; i++)
            Gizmos.DrawWireSphere(GetSlotPosition(i), 0.3f);
    }


}
