﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionIndicator : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
