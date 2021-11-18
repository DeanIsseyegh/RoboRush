using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePopupManager : MonoBehaviour
{
    public GameObject scorePrefab;
    private float scorePopupLength = 3f;

    internal void showScorePopup(Transform transform, float scoreChange)
    {
        GameObject scoreIndicator = Instantiate(scorePrefab, transform.position, new Quaternion());
        TextMeshPro textMeshPro = scoreIndicator.GetComponentInChildren<TextMeshPro>();
        if (scoreChange < 0)
        {
            textMeshPro.color = Color.red;
            textMeshPro.SetText("" + scoreChange);
        } else
        {
            textMeshPro.color = Color.green;
            textMeshPro.SetText("+" + scoreChange);
        }
        Destroy(scoreIndicator, scorePopupLength);
        scoreIndicator.transform.position = transform.position;
    }
}
