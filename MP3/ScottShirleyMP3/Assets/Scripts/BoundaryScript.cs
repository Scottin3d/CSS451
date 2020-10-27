
using UnityEngine;


public class BoundaryScript : MonoBehaviour
{
    private MeshFilter meshFilter;
    
    void Awake()
    {
        TryGetComponent<MeshFilter>(out meshFilter);
    }

    // Update is called once per frame
    void Start()
    {
        if (meshFilter) {
            Plane plane = new Plane(meshFilter.mesh, 3, 3);
        }
    }
}

public abstract class ProceduralShape {
    protected Mesh mesh;
    protected Vector3[] vertices;
    protected int[] triangles;
    protected Vector2[] UVs;
    public ProceduralShape(Mesh _mesh) {
        mesh = _mesh;
    }
}

public class Plane : ProceduralShape {
    private int sizeX;
    private int sizeY;

    public Plane(Mesh _mesh, int _sizeX, int _sizeY) : base(_mesh) {
        sizeX = _sizeX;
        sizeY = _sizeY;
    }

    private void CreateMesh() {
        CreateVertices();
        CreateTrangles();
        CreateUVs();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = UVs;
        mesh.RecalculateNormals();
    }

    private void CreateVertices() {
        vertices = new Vector3[sizeX * sizeY];

        for (int y = 0; y < sizeY; y++) {
            for (int x = 0; x < sizeX; x++) {
                vertices[x + y * sizeX] = new Vector3(x, 0.0f, y);
            }
        }
    }
    private void CreateTrangles() {
        triangles = new int[3 * 2 * (sizeX * sizeY - sizeX - sizeY + 1)];
        int triangleVertexCount = 0;
        for (int vertex = 0; vertex < sizeX * sizeY - sizeX; vertex++) {
            if (vertex % sizeX != (sizeX - 1)) {

                int A = vertex;
                int B = A + sizeX;
                int C = B + 1;
                triangles[triangleVertexCount] = A;
                triangles[triangleVertexCount + 1] = B;
                triangles[triangleVertexCount + 2] = C;

                B += 1;
                C = A + 1;
                triangles[triangleVertexCount + 3] = A;
                triangles[triangleVertexCount + 4] = B;
                triangles[triangleVertexCount + 5] = C;

                triangleVertexCount += 6;
            }
        }
    }
    private void CreateUVs() {
        UVs = new Vector2[sizeX * sizeY];
        int uvIndexCoutner = 0;
        foreach (var vertex in vertices) {
            UVs[uvIndexCoutner] = new Vector2(vertex.x, vertex.z);
            uvIndexCoutner++;
        }
    }
}
