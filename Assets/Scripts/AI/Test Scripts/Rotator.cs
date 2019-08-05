using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class Rotator : MonoBehaviour
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
                    level = Random.Range(10,20)
                }
            );
            manager.SetComponentData(entity, 
                new SpeedComponent {
                    speed = Random.Range(1f, 2f)
                }
            );
            manager.SetComponentData(entity, 
                new Translation {
                    Value = new Unity.Mathematics.float3(Random.Range(-8, 8f), Random.Range(-5, 5f), 0)
                }
            );

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

public class RotatorSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref SpeedComponent speed) =>
        {
            translation.Value.y += speed.speed * Time.deltaTime;
        });
    }
}
