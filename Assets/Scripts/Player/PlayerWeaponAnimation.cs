using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeaponAnimation : MonoBehaviour
{
    public List<WeaponAnimEntry> animEntries;

    private Dictionary<string, AnimatorOverrideController> animMap;

    void Awake()
    {
        animMap = animEntries.ToDictionary(e => e.weaponType, e => e.overrideController);
    }

    public void Apply(string type)
    {
        if (!animMap.TryGetValue(type, out var controller))
        {
            Debug.LogWarning($"No anim override for {type}");
            return;
        }
        Animator animator = GameManager.Instance.GetCurrentPlayer().GetComponent<Animator>();
        animator.runtimeAnimatorController = controller;
    }
}

