/*

* GameObject: Player
* Component: PlayerAction
* Description: Handles food throwing functionality by selecting from three available food prefabs,
* instantiating the selected prefab at the player's center, playing audio, and applying forward force.
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
[SerializeField]
private GameObject[] foodPrefabs = new GameObject[3];


[SerializeField]
private float throwForce = 10.0f;

[SerializeField]
private AudioClip throwAudioClip;

private AudioSource audioSource;

private void Start()
{
    audioSource = GetComponent<AudioSource>();
}

// Called by: Player Input
public void OnInteract(InputAction.CallbackContext ctx)
{
    if (ctx.started)
    {
        int foodIndex = SelectFood(ctx);
        ThrowFood(foodIndex);
    }
}

private int SelectFood(InputAction.CallbackContext ctx)
{
    int controlIndex = 0;
    Debug.Log($"Control name: {ctx.control.name}");
    string controlName = ctx.control.name;

    if (controlName.Contains("q"))
    {
        controlIndex = 1;
    }
    else if (controlName.Contains("f"))
    {
        controlIndex = 2;
    }

    return Mathf.Clamp(controlIndex, 0, foodPrefabs.Length - 1);
}

private void ThrowFood(int foodIndex)
{
    if (foodPrefabs == null || foodPrefabs.Length == 0)
    {
        return;
    }

    GameObject prefabToThrow = foodPrefabs[foodIndex];
    if (prefabToThrow == null)
    {
        return;
    }

    Vector3 spawnPosition = transform.position;
    GameObject instantiatedFood = Instantiate(prefabToThrow, spawnPosition, transform.rotation);

    if (audioSource != null && throwAudioClip != null)
    {
        audioSource.PlayOneShot(throwAudioClip);
    }

    Rigidbody foodRigidbody = instantiatedFood.GetComponent<Rigidbody>();
    if (foodRigidbody != null)
    {
        foodRigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }
}



}
