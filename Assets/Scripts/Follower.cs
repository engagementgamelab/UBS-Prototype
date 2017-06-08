/*
  Created by: Juan Sebastian Munoz arango
  2015
  Simple particle system that follows certain o
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Follower : MonoBehaviour {

    public List<Vector3> nodes;
    private Vector3[] directions;

    void Start() {
        GetComponent<ParticleSystem>().startLifetime = nodes.Count;
        if(nodes.Count == 0)
            Debug.LogError("Nodes needs to have at least 1 item");
        directions = new Vector3[nodes.Count];
        for(int i = 0; i < nodes.Count; i++) {
            directions[i] = (nodes[i] - ((i-1 >= 0) ? nodes[i-1] : transform.position));
        }
    }

    void Update() {
        ParticleSystem p = (ParticleSystem) GetComponent<ParticleSystem>();
        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[p.particleCount];
        int partCount = GetComponent<ParticleSystem>().GetParticles(particleList);

        for(int i = 0; i < partCount; i++) {
            float timeAlive = particleList[i].startLifetime - particleList[i].remainingLifetime;
            float dist = GetAddedMagnitude((int)(timeAlive));
            int count = 0;

            while(dist > GetAddedMagnitude(count))
                count++;

            particleList[i].velocity = directions[count];
        }
        p.SetParticles(particleList, partCount);
    }

    private float GetAddedMagnitude(int count) {
        float addedMagnitude = 0;
        for(int i = 0; i < count; i++) {
            addedMagnitude += directions[i].magnitude;
        }
        return addedMagnitude;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, nodes[0]);
        for(int i = 1; i < nodes.Count; i++) {
            Gizmos.DrawLine(nodes[i], nodes[i-1]);
        }
    }
}