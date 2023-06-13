using UnityEngine;
using UnityEngine.UI;
using Klak.TestTools;
using MediaPipe.HandPose;

public sealed class HandAnimator : MonoBehaviour
{
    #region Editable attributes
    public GameObject[] handPoints;
    public GameObject[] handBones;
    [SerializeField] ImageSource _source = null;
    [Space]
    [SerializeField] ResourceSet _resources = null;
    [SerializeField] bool _useAsyncReadback = true;
    [Space]
    [SerializeField] Mesh _jointMesh = null;
    [SerializeField] Mesh _boneMesh = null;
    [Space]
    [SerializeField] Material _jointMaterial = null;
    [SerializeField] Material _boneMaterial = null;
    [Space]
    [SerializeField] RawImage _monitorUI = null;

    #endregion

    #region Private members

    HandPipeline _pipeline;

    static readonly (int, int, int)[] BonePairs =
    {
        (0, 1, 0), (1, 2, 1), (1, 2, 2), (2, 3, 3), (3, 4, 4),     // Thumb
        (5, 6, 5), (6, 7, 6), (7, 8, 7),                     // Index finger
        (9, 10, 8), (10, 11, 9), (11, 12, 10),                // Middle finger
        (13, 14, 11), (14, 15, 12), (15, 16, 13),               // Ring finger
        (17, 18, 14), (18, 19, 15), (19, 20, 16),               // Pinky
        (0, 17, 17), (2, 5, 18), (5, 9, 19), (9, 13, 20), (13, 17, 21)  // Palm
    };

    Matrix4x4 CalculateJointXform(Vector3 pos)
      => Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one * 0.07f);

    Matrix4x4 CalculateBoneXform(Vector3 p1, Vector3 p2)
    {
        var length = Vector3.Distance(p1, p2) / 2;
        var radius = 0.03f;

        var center = (p1 + p2) / 2;
        var rotation = Quaternion.FromToRotation(Vector3.up, p2 - p1);
        var scale = new Vector3(radius, length, radius);

        return Matrix4x4.TRS(center, rotation, scale);
    }

    #endregion

    #region MonoBehaviour implementation

    void Start()
      => _pipeline = new HandPipeline(_resources);

    void OnDestroy()
      => _pipeline.Dispose();

    void LateUpdate()
    {
        // Feed the input image to the Hand pose pipeline.
        _pipeline.UseAsyncReadback = _useAsyncReadback;
        _pipeline.ProcessImage(_source.Texture);

        var layer = gameObject.layer;

        // Joint balls
        for (var i = 0; i < HandPipeline.KeyPointCount; i++)
        {
            var xform = CalculateJointXform(_pipeline.GetKeyPoint(i));
            handPoints[i].transform.position = xform.ExtractPosition();
            handPoints[i].transform.rotation = xform.ExtractRotation();
            handPoints[i].transform.localScale = xform.ExtractScale();
            //Graphics.DrawMesh(_jointMesh, xform, _jointMaterial, layer);
        }

        // Bones
        foreach (var pair in BonePairs)
        {
            var p1 = _pipeline.GetKeyPoint(pair.Item1);
            var p2 = _pipeline.GetKeyPoint(pair.Item2);
            var xform = CalculateBoneXform(p1, p2);
            handBones[pair.Item3].transform.position = xform.ExtractPosition();
            handBones[pair.Item3].transform.rotation = xform.ExtractRotation();
            handBones[pair.Item3].transform.localScale = xform.ExtractScale();;
            //Graphics.DrawMesh(_boneMesh, xform, _boneMaterial, layer);
        }

        // UI update
        _monitorUI.texture = _source.Texture;
    }

    #endregion
}
