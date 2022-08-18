﻿using System.Collections.Generic;
using UnityEngine;

namespace Tools.MeshCombinerTool
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshCombiner : MonoBehaviour
    {
        [SerializeField] private List<MeshFilter> sourceMeshFilters;
        [SerializeField] private MeshFilter targetMeshFilter;

        [ContextMenu("Combine Meshes")]
        private void CombineMeshes()
        {
            targetMeshFilter = transform.GetComponent<MeshFilter>();
            var combine = new CombineInstance[sourceMeshFilters.Count];

            for (var i = 0; i < sourceMeshFilters.Count; i++)
            {
                combine[i].mesh = sourceMeshFilters[i].sharedMesh;
                combine[i].transform = sourceMeshFilters[i].transform.localToWorldMatrix;
            }

            var mesh = new Mesh();
            mesh.CombineMeshes(combine);
            targetMeshFilter.mesh = mesh;
        }
    }
}
