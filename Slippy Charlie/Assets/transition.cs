using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transition : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField]
    float transitionDuration = 4;

    private float transitionElapsedTime = 0;
    private float transitionAmount
    {
        get
        {
            return transitionElapsedTime / transitionDuration;
        }
    }

    [SerializeField]
    TransitionDirection currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (currentDirection == TransitionDirection.In)
        {
            rectTransform.localScale = new Vector3(2, 2, 1);
        }
        else
        {
            rectTransform.localScale = new Vector3(0, 0, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDirection == TransitionDirection.In)
        {
            rectTransform.localScale = Vector3.Lerp(
                rectTransform.localScale,
                new Vector3(0, 0, 1),
                transitionAmount
            );
        }
        else
        {
            rectTransform.localScale = Vector3.Lerp(
                rectTransform.localScale,
                new Vector3(2, 2, 1),
                transitionAmount
            );
        }
        transitionElapsedTime += Time.deltaTime;

        // If you want to alternate between in and out
        // if (rectTransform.localScale.x <= 0.01f && currentDirection == TransitionDirection.In)
        // {
        //     currentDirection = TransitionDirection.Out;
        //     transitionElapsedTime = 0;
        // }
        // else if (rectTransform.localScale.x >= 2f && currentDirection == TransitionDirection.Out)
        // {
        //     currentDirection = TransitionDirection.In;
        //     transitionElapsedTime = 0;
        // }
    }
}

public enum TransitionDirection
{
    In,
    Out
};
