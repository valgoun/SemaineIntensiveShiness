using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraSetUp : MonoBehaviour {

    [Range(1f, 20f)]
    public float Distance = 5f;
    [Range(35f, 85f)]
    public float Angle = 60f;
    private Transform _target;

	// Use this for initialization
	void Start () {
        _target = transform.parent;
        CalcPos();
    }

    void ChangeCamera(float distance, float angle)
    {

    }

    private void CalcPos()
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.Sin(Angle * Mathf.Deg2Rad) * Distance;
        pos.z = Mathf.Cos(Angle * Mathf.Deg2Rad) * Distance * -1f;
        transform.position = _target.position + pos;
        transform.LookAt(_target);
    }

    void OnValidate()
    {
        if (_target == null)
            _target = transform.parent;
        CalcPos();
    }

}
