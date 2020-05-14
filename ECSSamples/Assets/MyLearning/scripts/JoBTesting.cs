using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;
public class JoBTesting : MonoBehaviour
{

    public bool useJob;
    public Transform pfZombie;
    private List<Transform> zombieTransformList;
    private TransformAccessArray transformAccessArray;

    private void Start()
    {
        zombieTransformList = new List<Transform>();
        for (int i = 0; i < 10000; i++)
        {
            Transform zombieTransform = Instantiate(pfZombie, new Vector3(UnityEngine.Random.Range(-8f, 8f),
                UnityEngine.Random.Range(-5f, 5f)), Quaternion.identity);
            zombieTransformList.Add(zombieTransform);

        }
        transformAccessArray = new TransformAccessArray(zombieTransformList.ToArray());
    }
    void Update()
    {
        float startTime = Time.realtimeSinceStartup;
        if (useJob)
        {

            ParallelJobTransform parallelJobTransform = new ParallelJobTransform
            {
                deltaTime = Time.deltaTime,             
            };


            //使用更牛逼的，让worker帮你更新transform
            // JobHandle jobHandle =  parallelComputePos.Schedule(zombieList.Count,100);//设计每个batch
            JobHandle jobHandle = parallelJobTransform.Schedule(transformAccessArray);
            jobHandle.Complete();


        }
        else
        {


        }



        Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
    }
    private JobHandle ReallyToughTask()
    {
        ReallyToughJob2 job = new ReallyToughJob2();
        return job.Schedule();
    }

    private void OnDestroy()
    {
        transformAccessArray.Dispose();
    }
    private void ReallyToughTasl()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
    
}
[BurstCompile]
public struct ReallyToughJob2 : IJob
{
    public void Execute()
    {
        float value = 0f;
        for (int i = 0; i < 50000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}

[BurstCompile]
public struct ParallelComputePos : IJobParallelFor
{
    public NativeArray<float3> positionArray;
    public NativeArray<float> moveYArray;
    [ReadOnly] public float deltaTime;
    public void Execute(int index)
    {
        positionArray[index] += new float3(0, moveYArray[index] * deltaTime, 0f);
        if (positionArray[index].y > 5f)
        {
            moveYArray[index] = -math.abs(moveYArray[index]);
        }
        if (positionArray[index].y < -5f)
        {
            moveYArray[index] = +math.abs(moveYArray[index]);
        }

        float value = 0f;
        for (int i = 0; i < 1000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }

    }
}
[BurstCompile]
public struct ParallelJobTransform : IJobParallelForTransform
{
  
    //public NativeArray<float> moveYArray;
    [ReadOnly] public float deltaTime;
    public void Execute(int index,TransformAccess transformAccess)
    {
        transformAccess.position += new Vector3(0, 2f * deltaTime, 0f);


    }
}