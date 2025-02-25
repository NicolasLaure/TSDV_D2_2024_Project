using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FrustumController : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] float fieldOfViewAngle;
    public float verticalfieldOfViewAngle;
    [SerializeField] float renderingDistance;

    Vector3 farLimit;

    Vector3 farUpperLeftVertex;
    Vector3 farUpperRightVertex;
    Vector3 farLowerLeftVertex;
    Vector3 farLowerRightVertex;

    public List<Vector3> vertices = new List<Vector3>();

    void Start()
    {
        SetVertices();
    }

    void Update()
    {
        UpdateVertices();

        farLimit = transform.position + transform.forward * renderingDistance;

        float farPlaneHalfWidth = Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance;
        float farPlaneHalfHeight = Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance;

        Vector3 fixedFarCenterX = transform.right * farPlaneHalfWidth;
        Vector3 fixedFarCenterY = transform.up * farPlaneHalfHeight;

        farUpperLeftVertex = farLimit - fixedFarCenterX + fixedFarCenterY;
        farUpperRightVertex = farLimit + fixedFarCenterX + fixedFarCenterY;
        farLowerLeftVertex = farLimit - fixedFarCenterX - fixedFarCenterY;
        farLowerRightVertex = farLimit + fixedFarCenterX - fixedFarCenterY;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawCube(farLimit, new Vector3(0.3f, 0.3f, 0.3f));
            DrawFrustumLines();

            for (int faceIndex = 0; faceIndex <= vertices.Count - 3; faceIndex += 3)
            {
                Vector3 point;
                point.x = (vertices[faceIndex].x + vertices[faceIndex + 1].x + vertices[faceIndex + 2].x) / 3;
                point.y = (vertices[faceIndex].y + vertices[faceIndex + 1].y + vertices[faceIndex + 2].y) / 3;
                point.z = (vertices[faceIndex].z + vertices[faceIndex + 1].z + vertices[faceIndex + 2].z) / 3;

                Gizmos.DrawCube(point, new Vector3(0.1f, 0.1f, 0.1f));
                Gizmos.color = new Color(1, 1, 1, 1f);
                Gizmos.DrawLine(point, point + GetFaceNormal(faceIndex));
            }
        }
    }

    void DrawFrustumLines()
    {
        Gizmos.DrawLine(transform.position, farUpperLeftVertex);
        Gizmos.DrawLine(transform.position, farUpperRightVertex);
        Gizmos.DrawLine(transform.position, farLowerLeftVertex);
        Gizmos.DrawLine(transform.position, farLowerRightVertex);

        Gizmos.DrawLine(farUpperLeftVertex, farUpperRightVertex);
        Gizmos.DrawLine(farUpperRightVertex, farLowerRightVertex);
        Gizmos.DrawLine(farLowerRightVertex, farLowerLeftVertex);
        Gizmos.DrawLine(farLowerLeftVertex, farUpperLeftVertex);
    }

    void SetVertices()
    {
        vertices.Add(transform.position);
        vertices.Add(farLowerRightVertex);
        vertices.Add(farLowerLeftVertex);

        vertices.Add(transform.position);
        vertices.Add(farUpperLeftVertex);
        vertices.Add(farUpperRightVertex);

        vertices.Add(transform.position);
        vertices.Add(farLowerLeftVertex);
        vertices.Add(farUpperLeftVertex);

        vertices.Add(transform.position);
        vertices.Add(farLowerRightVertex);
        vertices.Add(farUpperRightVertex);

        vertices.Add(farUpperLeftVertex);
        vertices.Add(farLowerRightVertex);
        vertices.Add(farUpperRightVertex);
    }

    void UpdateVertices()
    {
        vertices[0] = transform.position;
        vertices[1] = farLowerRightVertex;
        vertices[2] = farLowerLeftVertex;

        vertices[3] = transform.position;
        vertices[4] = farUpperLeftVertex;
        vertices[5] = farUpperRightVertex;

        vertices[6] = transform.position;
        vertices[7] = farLowerLeftVertex;
        vertices[8] = farUpperLeftVertex;

        vertices[9] = transform.position;
        vertices[10] = farUpperRightVertex;
        vertices[11] = farLowerRightVertex;

        vertices[12] = farUpperLeftVertex;
        vertices[13] = farLowerRightVertex;
        vertices[14] = farUpperRightVertex;
    }

    private Vector3 GetFaceNormal(int index)
    {
        // https://www.khronos.org/opengl/wiki/Calculating_a_Surface_Normal#:~:text=A%20surface%20normal%20for%20a,of%20the%20face%20w.r.t.%20winding).
        Vector3 firstVertex = vertices[index];
        Vector3 secondVertex = vertices[index + 1];
        Vector3 thirdVertex = vertices[index + 2];

        Vector3 normal;
        Vector3 firstSecond = secondVertex - firstVertex;
        Vector3 firstThird = thirdVertex - firstVertex;

        //Vector3 normal = Vector3.zero;
        //Vector3 normal = Vector3.Cross(secondVertex - firstVertex, thirdVertex - firstVertex).normalized;

        normal.x = (firstThird.y * firstSecond.z) - (firstThird.z * firstSecond.y);
        normal.y = (firstThird.z * firstSecond.x) - (firstThird.x * firstSecond.z);
        normal.z = (firstThird.x * firstSecond.y) - (firstThird.y * firstSecond.x);

        float magnitude = Mathf.Sqrt(Mathf.Pow(normal.x, 2) + Mathf.Pow(normal.y, 2) + Mathf.Pow(normal.z, 2));
        Vector3 normalizedNormal = normal / magnitude;

        return normalizedNormal;
    }


    private bool IsPointOnPositiveSide(Vector3 point, int faceIndex)
    {
        Vector3 normal = GetFaceNormal(faceIndex);
        Vector3 facePoint = GetFacePoint(faceIndex);
        return Vector3.Dot(normal, point - facePoint) > 0;
    }

    private Vector3 GetFacePoint(int faceIndex)
    {
        Vector3 point;
        point.x = (vertices[faceIndex].x + vertices[faceIndex + 1].x + vertices[faceIndex + 2].x) / 3;
        point.y = (vertices[faceIndex].y + vertices[faceIndex + 1].y + vertices[faceIndex + 2].y) / 3;
        point.z = (vertices[faceIndex].z + vertices[faceIndex + 1].z + vertices[faceIndex + 2].z) / 3;
        return point;
    }

    public bool CheckPointInside(Vector3 point)
    {
        for (int faceIndex = 0; faceIndex <= vertices.Count - 3; faceIndex += 3)
        {
            if (!IsPointOnPositiveSide(point, faceIndex))
                return false;
        }

        return true;
    }
}