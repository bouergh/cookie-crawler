using UnityEngine;


public class FireBall : MonoBehaviour {
	int speed = 2;
    Vector3 dir;

    void Start()
    {
        dir = Vector3.Normalize(CookieController.singleton.transform.position - gameObject.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

	// Update is called once per frame
	void Update () {
        transform.Translate(Time.deltaTime * speed, 0, 0);

        if (!CookieController.singleton.traversableMap.HasTile(Vector3Int.CeilToInt(gameObject.transform.position)))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CookieController.singleton.transform.position = CookieController.singleton.initialPosition;
    }
}
