using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LevelUpSystem1 : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LevelComponent1 levelComponent) =>
        {
            levelComponent.level += 1f * Time.DeltaTime;
        });
    }
}
