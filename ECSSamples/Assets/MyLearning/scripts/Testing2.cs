using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class Testing2 : MonoBehaviour
{   
    [SerializeField] private Mesh mesh;
    [SerializeField] public Material mat;

    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(   
            typeof(Translation),
            typeof(RenderMesh) , 
            typeof(RenderBounds),
            typeof(MoveSpeedComponent),
         
            typeof(LocalToWorld));
        NativeArray<Entity> entityArray = new NativeArray<Entity>(100000, Allocator.Temp);
        
        entityManager.CreateEntity(entityArchetype, entityArray);
        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            //entityManager.SetComponentData(entity, new LevelComponent { level = UnityEngine.Random.Range(10,20) });
            entityManager.SetComponentData(entity, new MoveSpeedComponent { moveSpeed = UnityEngine.Random.Range(10, 15)});
            entityManager.SetComponentData(entity, new Translation {
                Value = new Unity.Mathematics.float3(UnityEngine.Random.Range(-80f, 80f),
                                                     UnityEngine.Random.Range(-50f, 50f),
                                                     UnityEngine.Random.Range(-80f, 80f)) });

            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = mat
            } ); 
            
        }
        entityArray.Dispose();
    }
}
