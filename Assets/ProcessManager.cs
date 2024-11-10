using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    private List<Process> processes = new List<Process>();

    void Start()
    {
        // Find existing characters in the scene
        GameObject process1 = GameObject.Find("process1");
        GameObject process2 = GameObject.Find("process2");
        GameObject process3 = GameObject.Find("process3");

        // Add SpriteRenderer and assign colors based on priority
        process1.GetComponent<SpriteRenderer>().color = Color.green; // High priority
        process2.GetComponent<SpriteRenderer>().color = Color.yellow; // Medium priority
        process3.GetComponent<SpriteRenderer>().color = Color.red; // Low priority

        // Initialize processes with names, execution times, and priorities
        processes.Add(new Process("Process 1", 1.5f, 1, process1)); // Priority 1
        processes.Add(new Process("Process 2", 2f, 2, process2)); // Priority 2
        processes.Add(new Process("Process 3", 1.5f, 3, process3)); // Priority 3

        // Start the process scheduling coroutine
        StartCoroutine(ScheduleProcesses());
    }

    IEnumerator ScheduleProcesses()
    {
        // Sort processes by priority before executing
        processes.Sort((x, y) => x.Priority.CompareTo(y.Priority));

        // Run processes four times, alternating between running and resting
        for (int i = 0; i < 4; i++)
        {
            foreach (var process in processes)
            {
                // Move the character smoothly over the execution time
                yield return StartCoroutine(process.MoveCharacterSmooth(5.5f)); // Move character by 5.5 units
                Debug.Log("Running: " + process.Name);

                // Simulate execution time
                yield return new WaitForSeconds(process.ExecutionTime);

                // Simulate resting for others while one runs
                foreach (var otherProcess in processes)
                {
                    if (otherProcess != process)
                    {
                        Debug.Log(otherProcess.Name + " is resting.");
                    }
                }

                yield return new WaitForSeconds(1f); // Rest for 1 second
            }
        }

        Debug.Log("All processes completed.");
    }
}


