// <copyright file="QuestionUIManager.cs" company="PLM BSCS 4-1 (2026)">
// Copyright (c) PLM BSCS 4-1 (2026)". All rights reserved.
// </copyright>

using UnityEngine;
using TMPro;

/// <summary>
/// This script manages the UI for displaying quiz questions.
/// </summary>

public class QuestionUIManager : MonoBehaviour
{
    public static QuestionUIManager Instance;

    public TextMeshProUGUI questionText;

    private void Awake()
    {
        Instance = this;
    }
}
