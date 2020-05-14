using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation,ref MoveSpeedComponent moveSpeed) =>
        {
            translation.Value.y += moveSpeed.moveSpeed * Time.DeltaTime;
        });
    }
}
