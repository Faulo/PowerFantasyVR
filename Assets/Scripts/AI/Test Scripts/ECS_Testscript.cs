using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;

/**
 * <summary>Test class for trying out the ECS architecture.</summary>
 **/
public class ECS_Testscript : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    // Start is called before the first frame update
    void Start()
    {
        EntityManager manager = World.Active.EntityManager;

        EntityArchetype archetype = manager.CreateArchetype(
            typeof(RotatorComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(SpeedComponent)
        );

        NativeArray<Entity> array = new NativeArray<Entity>(5, Allocator.Temp);
        manager.CreateEntity(archetype, array);

        for (int i = 0; i < array.Length; i++)
        {
            Entity entity = array[i];
            manager.SetComponentData(entity, 
                new RotatorComponent {
                    level = UnityEngine.Random.Range(10,20)
                }
            );
            manager.SetComponentData(entity, 
                new SpeedComponent {
                    speed = UnityEngine.Random.Range(1f, 2f)
                }
            );
            manager.SetComponentData(entity, 
                new Translation {
                    Value = new Unity.Mathematics.float3(UnityEngine.Random.Range(-8, 8f), UnityEngine.Random.Range(-5, 5f), 0)
                }
            );
            //manager.SetComponentData(entity,
            //    new RigidbodyComponent
            //    {
            //        rigidbody = new float2(0f,0f)
            //    });

            manager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material
            });
        }

        array.Dispose();
    }
}

public struct RotatorComponent : IComponentData
{
    public float level;
}

public struct SpeedComponent : IComponentData
{
    public float speed;
}

public struct RigidbodyComponent : IComponentData
{
    public float3 rigidbody;
}

public class LevelSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref RotatorComponent rotatorComponent) =>
        {
            rotatorComponent.level += 1f * Time.deltaTime;
        });
    }
}

public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref SpeedComponent speed) =>
        {
            translation.Value.y += speed.speed * Time.deltaTime;
        });
    }
}

// Tried to move objects with Rigidbody and AddRelative Force, but didn't work
//public class PhysicsSystem : ComponentSystem
//{
//    protected override void OnUpdate()
//    {
//        Entities.ForEach((Rigidbody rigidbody) =>
//        {
//            rigidbody.AddRelativeForce(Vector3.up * 2.0f);
//            //translation.Value.y += speed.speed * Time.deltaTime;
//        });
//    }
//}
