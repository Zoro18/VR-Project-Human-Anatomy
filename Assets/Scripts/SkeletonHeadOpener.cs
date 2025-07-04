using System.Collections;
using UnityEngine;

public class SkeletonHeadOpener : MonoBehaviour
{
    public Transform origin; // The origin point in the middle of the skeleton
    public float moveDistance = 1.0f; // Distance each part will move backward
    public float moveDuration = 2.0f; // Time taken to move

    private bool isOpened = false;
    private Vector3[] originalPositions;

    void Start()
    {
        StoreOriginalPositions();
    }

    void Update()
    {
        // Remove key press detection to separate method calls
    }

    private void StoreOriginalPositions()
    {
        originalPositions = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            originalPositions[i] = transform.GetChild(i).localPosition;
        }
    }

    public void OpenHead()
    {
        if (!isOpened)
        {
            StartCoroutine(MoveHeadParts(true));
        }
    }

    public void CloseHead()
    {
        if (isOpened)
        {
            StartCoroutine(MoveHeadParts(false));
        }
    }

    private IEnumerator MoveHeadParts(bool open)
    {
        isOpened = open;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                Vector3 direction = (child.position - origin.position).normalized;
                Vector3 targetPosition = open
                    ? originalPositions[i] + direction * moveDistance
                    : originalPositions[i];
                child.localPosition = Vector3.Lerp(child.localPosition, targetPosition, elapsedTime / moveDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set
        SetFinalPositions(open);
    }

    private void SetFinalPositions(bool open)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 direction = (child.position - origin.position).normalized;
            child.localPosition = open
                ? originalPositions[i] + direction * moveDistance
                : originalPositions[i];
        }
    }
}
