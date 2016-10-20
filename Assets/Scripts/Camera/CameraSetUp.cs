using UnityEngine;
using System.Collections;
using DG.Tweening;

[ExecuteInEditMode]
public class CameraSetUp : MonoBehaviour
{

    [Range(1f, 20f)]
    public float Distance = 5f;
    [Range(0f, 85f)]
    public float Angle = 60f;

    [Header("Orthographic Setting")]
    public float OrthographicSize;
    [Header("Perspective Setting")]
    public float Fov = 60;
    public float Near = 0.3f,
                 Far = 1000f;


    private float _aspect;
    private Transform _target;
    private Camera _camera;
    private Matrix4x4 _ortho, _perspective;

    // Use this for initialization
    void Start()
    {
        _camera = GetComponent<Camera>();
        _target = transform.parent;
        CalcPos();

        _aspect = (float)Screen.width / (float)Screen.height;
        _ortho = Matrix4x4.Ortho(-OrthographicSize * _aspect, OrthographicSize * _aspect, -OrthographicSize, OrthographicSize, Near, Far);
        _perspective = Matrix4x4.Perspective(Fov, _aspect, Near, Far);
        _camera.projectionMatrix = _perspective;


        StartCoroutine(LerpFromTo(_camera.projectionMatrix, _ortho, 1.2f));
        ChangeCamera(13.61f, 0f, 1.2f);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (_camera.projectionMatrix == _perspective)
            {
                StartCoroutine(LerpFromTo(_camera.projectionMatrix, _ortho, 1.2f));
                ChangeCamera(13.61f, 0f, 1.2f);
            }
            else
            {
                StartCoroutine(LerpFromTo(_camera.projectionMatrix, _perspective, 1.2f));
                ChangeCamera(13.61f, 60f, 1.2f);
            }
        }
    }

    Tweener ChangeCamera(float distance, float angle, float time)
    {
        Distance = distance;
        Angle = angle;
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Sin(Angle * Mathf.Deg2Rad) * Distance;
        pos.z = Mathf.Cos(Angle * Mathf.Deg2Rad) * Distance * -1f;
        return transform.DOLocalMove(pos, time).SetEase(Ease.OutQuad).OnUpdate(() =>
        {
            transform.LookAt(_target);
        });
    }

    private void CalcPos()
    {
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Sin(Angle * Mathf.Deg2Rad) * Distance;
        pos.z = Mathf.Cos(Angle * Mathf.Deg2Rad) * Distance * -1f;
        transform.localPosition = pos;
        transform.LookAt(_target);
    }

    void OnValidate()
    {
        if (_target == null)
            _target = transform.parent;
        if (_camera == null)
            _camera = GetComponent<Camera>();
        CalcPos();
    }

    private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            ret[i] = Mathf.Lerp(from[i], to[i], time);
        return ret;
    }

    private IEnumerator LerpFromTo(Matrix4x4 src, Matrix4x4 dest, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            _camera.projectionMatrix = MatrixLerp(src, dest, (Time.time - startTime) / duration);
            yield return 1;
        }
        _camera.projectionMatrix = dest;
    }

}
