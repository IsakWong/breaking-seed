using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanObtainSeed
{
    GameObject GetGO();

    void ObtainSeed(SeedBehaviour seed);

    void DiscardSeed(SeedBehaviour seed, Vector3 discardDirection);

    void GetSeed(SeedBehaviour seed);

    void OwningSeed(SeedBehaviour seed);
}