using UnityEngine;
using UnityEngine.UI;

public class MovementIndicatorHandler : AbstractPlayerHandler
{
    private Canvas _indicatorContainer;
    private Image _indicator;

    public bool IndicatorEnabled
    {
        get { return _indicatorContainer.gameObject.activeSelf; }
        set { _indicatorContainer.gameObject.SetActive(value); }
    }

    void Start()
    {
        _indicatorContainer = Player.GetComponentInChildren<Canvas>(true);
        _indicator = Player.GetComponentInChildren<Image>(true);
    }

    public void DrawIndicator(Vector3 targetPosition)
    {
        var playerPosition = Player.transform.position;
        var direction = targetPosition - playerPosition;

        // Move the container to target
        _indicatorContainer.transform.position = targetPosition;

        // Rotate the container
        _indicatorContainer.transform.up = direction;

        // Clip the arrow
        var arrowSize = (_indicator.transform as RectTransform).rect.height;
        var length = direction.magnitude - Player.IndicatorSpaceFromPlayerToArrowStart;
        if (length < 0f)
            length = 0f;
        var showPercentage = length / arrowSize;
        _indicator.fillAmount = showPercentage;
    }
}