using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GestureControllerSimple : MonoBehaviour, IPointerClickHandler
{
    private Transform m_SpaceCamTrans;
    private Transform target;
    private float minVertical = 15f;
    private float maxVertical = 85;//85f;
    private float x = 0.0f;
    private float y = 0.0f;
    private float distance = 0.0f;

    private float newdis = 0;
    private float olddis = 0;
    public float m_3D_ScaleRatio = 1f; //3d模式下当前模型大小

    private float m_3D_ScaleSensitivity = 0.0025f; //3d模式下缩放灵敏度 //0.01
    private float m_3D_MovePositionSensitivity = 0.1f;//200;//3d状态下双指拖拽模型移动灵敏度//10  4
    private float m_3D_RotateSensitivity = 0.1f;//3d模式下旋转灵敏度 //

    private float m_T1;
    private float m_T2;
    private bool isTap = false;

    private float spaceCamDistanceRatio = 1;

    private Vector3 m_EditingWorldPos;

    public Vector3 EditingWorldPos { get => m_EditingWorldPos; set => m_EditingWorldPos = value; }
    public float SpaceCamDistanceRatio { get => spaceCamDistanceRatio; set => spaceCamDistanceRatio = value; }

    public bool isOpenFloorMatEdit = false;

    public Transform originPos;

    public float rotateSpeed=0.1f;
    public float moveSpeed=0.01f;
    public float zoomSpeed=0.05f;
    public Slider rotateSlider;
    public Slider moveSlider;
    public Slider zoomSlider;
    public Text rotateSpeedText;
    public Text moveSpeedText;
    public Text zoomSpeedText;

    private Vector3 defaultPosition;
    private Vector3 defaultRotation;

    void Awake()
    {
        target = GameObject.Find("Main Camera").transform;//WallParent
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.localPosition;
        defaultRotation = transform.localRotation.eulerAngles;

        m_SpaceCamTrans = Camera.main.transform;
        Debug.Log("GestureController Start");
        OpenFG();
    }

    private void OnEnable()
    {
    }

    public void RotateSpeedChange()
    {
        rotateSpeed = rotateSlider.value;
        rotateSpeedText.text = rotateSlider.value.ToString();
    }

    public void MoveSpeedChange()
    {
        moveSpeed = moveSlider.value;
        moveSpeedText.text = moveSlider.value.ToString();

    }

    public void ZoomSpeedChange()
    {
        zoomSpeed = zoomSlider.value;
        zoomSpeedText.text = zoomSlider.value.ToString();

    }

    public void OpenFG()
    {
        FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
        FingerGestures.OnFingerDragMove += OnFingerDragMove;
        FingerGestures.OnFingerDragEnd += OnFingerDragEnd;
        FingerGestures.OnPinchMove += OnPinchMove;
        FingerGestures.OnPinchBegin += OnPinchBegin;
        FingerGestures.OnPinchEnd += OnPinchEnd;
        FingerGestures.OnTwoFingerDragBegin += OnTwoFingerDragBegin;
        FingerGestures.OnTwoFingerDragMove += OnTwoFingerDragMove;
        FingerGestures.OnTwoFingerDragEnd += OnTwoFingerDragEnd;
        //   FingerGestures.OnFingerTap += OnFingerTap;
        FingerGestures.OnFingerUp += OnFingerUp;
        FingerGestures.OnFingerDown += OnFingerDown;
        FingerGestures.OnFingerStationaryEnd += OnFingerStationaryEnd;
    }

    public void CloseFG()
    {
        FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd;
        FingerGestures.OnPinchMove -= OnPinchMove;
        FingerGestures.OnPinchBegin -= OnPinchBegin;
        FingerGestures.OnPinchEnd -= OnPinchEnd;
        FingerGestures.OnTwoFingerDragBegin -= OnTwoFingerDragBegin;
        FingerGestures.OnTwoFingerDragMove -= OnTwoFingerDragMove;
        FingerGestures.OnTwoFingerDragEnd -= OnTwoFingerDragEnd;
        //   FingerGestures.OnFingerTap += OnFingerTap;
        FingerGestures.OnFingerUp -= OnFingerUp;
        FingerGestures.OnFingerDown -= OnFingerDown;
        FingerGestures.OnFingerStationaryEnd -= OnFingerStationaryEnd;
        FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd;
    }

    private void OnFingerStationaryEnd(int fingerIndex, Vector2 fingerPos, float elapsedTime)
    {
        //   Debug.Log("OnFingerStationaryEnd");
    }

    private void OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        //    Debug.Log("抬起");
        if (isTap)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Input.mousePosition
            if (Physics.Raycast(ray, out hit))//检测到有点击到物体
            {
                GameObject furniture_chosed = hit.collider.gameObject;
                Debug.Log("ges== layer --" + furniture_chosed.layer);
            }
        }
    }

    private void OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        // Debug.Log("按下");
        isTap = true;
    }

    public void SetDistance(Vector3 t1, Vector3 t2)
    {
        //Debug.Log("t1" + t1);
        //Debug.Log("t2" + t2);
        distance = 5000 * SpaceCamDistanceRatio; //8000 *3.9f;//(t1 - t2).magnitude;
                                                                            //Debug.Log("==SpaceCamDistanceRatio==" + SpaceCamDistanceRatio);
        Debug.Log("==distance==" + distance);
        QualitySettings.shadowDistance = distance * 2;
        Debug.Log("==shadowDistance==" + distance * 2);
    }
    private void OnTwoFingerDragBegin(Vector2 fingerPos, Vector2 startPos)
    {
        isTap = false;
       
        Debug.Log("xxxxxxx");
        FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd;

    }

    private void OnTwoFingerDragMove(Vector2 fingerPos, Vector2 delta)
    {
        //Debug.Log("OnTwoFingerDragMove");
        //Debug.Log("==Time.deltaTime==" + Time.deltaTime);
        //var horizontal = delta.x * m_3D_MovePositionSensitivity;
        //var vertical = delta.y * m_3D_MovePositionSensitivity;
        //var motion = m_SpaceCamTrans.transform.rotation * new Vector3(horizontal, vertical, 0);
        //var mag = motion.magnitude;
        //// motion.y = 0;
        //Debug.Log("==motion.normalized * mag==" + motion.normalized * mag);
        //target.transform.position += motion.normalized;

        transform.Translate(Vector3.left * (delta.x * moveSpeed));
        transform.Translate(Vector3.up * (-delta.y * moveSpeed));


    }

    private void OnTwoFingerDragEnd(Vector2 fingerPos)
    {
        FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
        FingerGestures.OnFingerDragMove += OnFingerDragMove;
        FingerGestures.OnFingerDragEnd += OnFingerDragEnd;
    }

    public void OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
    {
        isTap = false;
    }

    void OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        /*

        //  Debug.Log("=SpaceView=Original_LookAtTarget==" + GlobalController.m_CameraController.Original_LookAtTarget);
        //m_SpaceCamTrans.LookAt(target);
        float dt = Time.deltaTime;
        x = m_SpaceCamTrans.eulerAngles.y;
        y = m_SpaceCamTrans.eulerAngles.x;

        //float x1 = Input.GetAxis("Mouse X");
        //float y1 = Input.GetAxis("Mouse Y");
        //x += x1 * dt * m_3D_RotateSensitivity;
        //y += -y1 * dt * m_3D_RotateSensitivity;
        float x1 = delta.x;
        float y1 = delta.y;
        x += x1 * m_3D_RotateSensitivity;
        y += -y1 * m_3D_RotateSensitivity;
        //    Debug.Log("===x,y==="+ x+" ====  "+ y);
        SetPos(x, y);

        */

        transform.RotateAround(originPos.transform.position, Vector3.up, delta.x * rotateSpeed);
        transform.RotateAround(originPos.transform.position, transform.right, delta.y * rotateSpeed);



    }

    void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
        Debug.Log("==OnFingerDragEnd==");
    }

    void OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        isTap = false;
    }

    void OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        /*
        m_3D_ScaleRatio += delta * m_3D_ScaleSensitivity;
        //       Debug.Log(m_3D_ScaleRatio);
        m_3D_ScaleRatio = Mathf.Clamp(m_3D_ScaleRatio, 0.6f, 2f);
        target.transform.localScale = Vector3.one * m_3D_ScaleRatio;
        */

        transform.Translate(Vector3.forward * delta * zoomSpeed);


    }

    void OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {

    }

    public void SetPos(float x, float y)
    {
        y = ClampAngle(y, minVertical, maxVertical);
        var rotation = Quaternion.Euler(y, x, 0.0f);
        var position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;//target.position;
        m_SpaceCamTrans.rotation = rotation;
        //m_SpaceCamTrans.position = position;

    }

    static float ClampAngle(float angle, float min, float max)
    {

        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }


    public void ResetRatio()
    {
        m_3D_ScaleRatio = 1;
    }



    public void OnPointerClick(PointerEventData eventData)
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
#if !UNITY_EDITOR//UNITY_ANDROID || UNITY_IPHONE
            //移动端判断如下
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            //PC端判断如下
            if (EventSystem.current.IsPointerOverGameObject())
#endif
            {
                return;
            }
            m_T2 = Time.realtimeSinceStartup;
            if (m_T2 - m_T1 < 0.2)// &&!EventSystem.current.IsPointerOverGameObject()
            {
                Debug.Log("3d双击");
                ResetRatio();
            }
            m_T1 = m_T2;
            if (isOpenFloorMatEdit)
            {
                DetectGroundArea();
            }
        }
    }
    public void DetectGroundArea()
    {
        if (Camera.main != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);//鼠标的屏幕坐标转化为一条射线
            RaycastHit hit;

            //距离为5
            //if(Physics.Raycast(ray, out hit, 5)) {
            //    var hitObj = hit.collider.gameObject;
            //    Debug.Log(hitObj);
            //}
            //无距离限制
            if (Physics.Raycast(ray, out hit))
            {
                var hitObj = hit.collider.gameObject;
                Debug.Log(hitObj);
            }
            else
            {
            }
        }
        else {
            Debug.Log("==DetectGroundArea== failed");
        }
    }
    public void OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
    {
        Debug.Log("ges==OnFingerTap==");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Input.mousePosition
        if (Physics.Raycast(ray, out hit))//检测到有点击到物体
        {
            GameObject furniture_chosed = hit.collider.gameObject;
            Debug.Log("ges== layer --" + furniture_chosed.layer);
        }
    }
    void OnDestroy()
    {
        Debug.Log("OnDestroy");
        CloseFG();
    }

    public void ResetCamera()
    {
        transform.position = defaultPosition;
        transform.rotation = Quaternion.Euler(defaultRotation);
    }
}
