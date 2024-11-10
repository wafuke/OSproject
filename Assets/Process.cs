using System.Collections;
using UnityEngine;

public class Process
{
    public string Name { get; private set; }
    public float ExecutionTime { get; private set; }
    public int Priority { get; private set; }
    public GameObject Character; // Reference to the character GameObject

    public Process(string name, float executionTime, int priority, GameObject character)
    {
        Name = name;
        ExecutionTime = executionTime;
        Priority = priority;
        Character = character;
    }

    // Coroutine to move the character on the track smoothly
    public IEnumerator MoveCharacterSmooth(float distance)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = Character.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(distance, 0, 0); // Move character to the right

        while (elapsedTime < ExecutionTime)
        {
            Character.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / ExecutionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Character.transform.position = targetPosition; // Ensure it reaches the target
    }
}
