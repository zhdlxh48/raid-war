using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : MonoBehaviour
{
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    Vector3[] warriorStet;
    Vector3[] archerStet;
    Vector3[] wizardStet;

    Vector3[] pentagramPoint;
    Vector3[] tempPoint;


    // Start is called before the first frame update
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        Mesh mesh = new Mesh();

        warriorStet = new Vector3[5]
            {
                new Vector3(-70, 21,0), new Vector3(-8f, 80f,0), new Vector3(60f,21,0),
                new Vector3(25,-40,0), new Vector3(-60, -75f, 0)
            };

        archerStet = new Vector3[5]
            {
                new Vector3(-90, 21,0), new Vector3(-8f, 55f,0), new Vector3(80f,25,0),
                new Vector3(25,-43,0), new Vector3(-35, -48f, 0)
            };

        wizardStet = new Vector3[5]
            {
                new Vector3(-80, 21,0), new Vector3(-8f, 40f,0), new Vector3(40f,25,0),
                new Vector3(50,-80,0), new Vector3(-50, -60f, 0)
            };

        pentagramPoint = new Vector3[5]
            {
                Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero
            };

        tempPoint = new Vector3[5]
            {
                Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero
            };

        mesh.vertices = pentagramPoint;
        pentagramPoint = warriorStet;

        mesh.uv = new Vector2[]
            {
                new Vector2(0.0f, 1.0f),  new Vector2(0.5f, 1.0f),  new Vector2(1.0f, 0.5f)
                , new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.0f)
            };

        int[] triangle = new int[] { 0, 1, 2, 2, 3, 0, 0, 3, 4 };

        mesh.triangles = triangle;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            tempPoint[i] =
                Vector3.MoveTowards(tempPoint[i], pentagramPoint[i], 180.0f * Time.deltaTime);
        }
        meshFilter.mesh.vertices = tempPoint;
    }

    public void SetStetPentagram(ClassIcon classIcon)
    {
        switch (classIcon)
        {
            case ClassIcon.Warrior:
                pentagramPoint = warriorStet;
                break;

            case ClassIcon.Archer:
                pentagramPoint = archerStet;
                break;

            case ClassIcon.Wizard:
                pentagramPoint = wizardStet;
                break;
        }
    }
}
