using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour {

    private Transform thisTranform;
    private ParticleSystem.Particle[] points;
    private float starDistanceSqr;
    private float starClipDistanceSqr;

    public Color starColor;
    public int starsMax = 150;
    public float starSize = 1;
    public float starDistance = 120f;
    public float starClipDistance = 10f;

	// Use this for initialization
	void Start () {
        thisTranform = GetComponent<Transform>();
        starDistanceSqr = starDistance * starDistance;
        starClipDistanceSqr = starClipDistance * starClipDistance;
	}

    private void CreateStars() {

        points = new ParticleSystem.Particle[starsMax];

        // Generate all stars until max is reached
        for (int i = 0; i < starsMax; i++) {
            points[i].position = Random.insideUnitSphere * starDistance + thisTranform.position;
            //points[i].position.Set(points[i].position.x, points[i].position.y, 0);
            points[i].startColor = new Color(starColor.r, starColor.g, starColor.b, starColor.a);
            points[i].startSize = starSize;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (points == null)
            CreateStars();

        for (int i = 0; i < starsMax; i++) {

            if ((points[i].position - thisTranform.position).sqrMagnitude > starDistanceSqr) {
                points[i].position = Random.insideUnitSphere.normalized * starDistance + thisTranform.position;
                //points[i].position.Set(points[i].position.x, points[i].position.y, 0);
            }

            if ((points[i].position - thisTranform.position).sqrMagnitude <= starClipDistanceSqr) {
                float percentage = (points[i].position - thisTranform.position).sqrMagnitude / starClipDistanceSqr;
                points[i].startColor = new Color(1, 1, 1, percentage);
                points[i].startSize = percentage * starSize;
            }
            GetComponent<ParticleSystem>().SetParticles(points, points.Length);
        }

    }
}
