using System.Collections;
using UnityEngine;

public class SlidingDoor : Door
{
    // Position when the door is opened
    [SerializeField]
    private Vector3 _openPosition;

    // Position when the door is closed
    [SerializeField]
    private Vector3 _closedPosition;

    // Override function Open to change door opening behavior
    public override void Open()
    {
        // Stop any currently running door animation
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }

        // Start sliding the door to the open position
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_openPosition));

        // Execute parent Door Open() logic
        base.Open();
    }

    // Override function Close to change door closing behavior
    public override void Close()
    {
        // Stop any currently running door animation
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }

        // Start sliding the door to the closed position
        _animatingDoorCoroutine = StartCoroutine(SlideDoor(_closedPosition));

        // Execute parent Door Close() logic
        base.Close();
    }

    // Coroutine to animate door movement
    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        // Mark animation as running
        _isAnimating = true;

        // Get current position
        Vector3 startPosition = _doorTransform.localPosition;

        // Timer for animation
        float time = 0f;

        // Animate over the specified duration
        while (time < _duration)
        {
            time += Time.deltaTime;

            Vector3 position = Vector3.Lerp(
                startPosition,
                targetPosition,
                time / _duration
            );

            _doorTransform.localPosition = position;

            yield return null;
        }

        // Ensure final position is exact
        _doorTransform.localPosition = targetPosition;

        // Mark animation as finished
        _isAnimating = false;
    }
}