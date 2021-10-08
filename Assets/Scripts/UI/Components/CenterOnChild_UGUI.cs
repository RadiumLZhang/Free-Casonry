/*function:自动居中最靠近中心的Item
           支持居中Item缩放
           支持非居中Item变暗
           支持点击Item居中
           带有居中Item改变时回调委托：_centerChanged
           */
 
 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using Logic;
using Manager;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif
public enum ScrollDir_UGUI
{
    Horizontal,
    Vertical
}
public enum ImageType_UGUI
{
    Image
}
/// <summary>
/// 用于显示在Inspector的类
/// </summary>
[Serializable]
public class ScrollViewImage
{
    public ImageType_UGUI type;
    public Image image;
}
#if UNITY_EDITOR
/// <summary>
/// 重绘Inspector类
/// </summary>
[CustomEditor(typeof(CenterOnChild_UGUI))]
public class OverDrawInspector : Editor
{
    private SerializedObject serial;//序列化
    private ReorderableList list;//数组
 
    private SerializedProperty TransformSpeed, ScaleSpeed, OpacitySpeed, CenterChildScale, AddMask, MaskOpacity;
    /// <summary>
    /// 初始化
    /// </summary>
    void OnEnable()
    {
        serial = new SerializedObject(target);
        list = new ReorderableList(serial, serial.FindProperty("ChildMasks"), true, true, true, true);
        TransformSpeed = serial.FindProperty("TransformSpeed");
        ScaleSpeed = serial.FindProperty("ScaleSpeed");
        OpacitySpeed = serial.FindProperty("OpacitySpeed");
        CenterChildScale = serial.FindProperty("CenterChildScale");
        AddMask = serial.FindProperty("AddMask");
        MaskOpacity = serial.FindProperty("MaskOpacity");
 
        //绘制List标题
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "MaskList");
        };
        //绘制List
        list.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) =>
        {       
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            //绘制一个单元
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("type"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("image"), GUIContent.none);
        };
    }
    /// <summary>
    /// 重绘
    /// </summary>
    public override void OnInspectorGUI()
    {
        serial.Update();
        EditorGUILayout.PropertyField(TransformSpeed);
        EditorGUILayout.PropertyField(ScaleSpeed);
        EditorGUILayout.PropertyField(CenterChildScale);
        EditorGUILayout.PropertyField(AddMask);
        if (AddMask.boolValue == true)
        {
            EditorGUILayout.PropertyField(OpacitySpeed);
            EditorGUILayout.PropertyField(MaskOpacity);
            list.DoLayoutList();
        }
        serial.ApplyModifiedProperties();//应用
    }
 
}
#endif
 
/// <summary>
/// 逻辑类
/// </summary>
public class CenterOnChild_UGUI : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    public ScrollDir_UGUI Dir = ScrollDir_UGUI.Horizontal;
    /// <summary>
    /// 未居中组件遮罩透明度
    /// </summary>
    [SerializeField, Range(0, 1)]
    public float MaskOpacity = 0.4f;
    /// <summary>
    /// 是否加入遮罩
    /// </summary>
    [Header("---开启遮罩（将每个子组件的遮罩手动拖入）")]
    public bool AddMask = false;
    //是否正在居中
    private bool _isCentering = false;
    //是否正在缩放
    private bool _isScaling = false;
    //是否需要变换透明度
    private bool _needChangingCapacity = false;
    //是否完成透明度还原
    //private bool _needReturnCapacity = false;
 
    /// <summary>
    /// 居中过程移动速度,超出范围可能会出bug
    /// </summary>
    [SerializeField, Range(5, 25)]
    public float TransformSpeed = 20f;
    /// <summary>
    /// 居中过程缩放，小于15不流畅，大于30没有明显变化
    /// </summary>
    [SerializeField, Range(15, 30)]
    public float ScaleSpeed = 20f;
    /// <summary>
    /// 居中过程明暗变化，小于5等待时间过长，大于40没有明显变化
    /// </summary>
    [SerializeField, Range(5, 40)]
    public float OpacitySpeed = 10f;
    /// <summary>
    /// 居中组件缩放
    /// </summary>
    public Vector3 CenterChildScale = new Vector3(1.3f, 1.3f, 1.3f);
    /// <summary>
    /// 组件的遮罩
    /// </summary>
    [SerializeField]
    public List<ScrollViewImage> ChildMasks = new List<ScrollViewImage>();
    //插值运算临时变量
    private Vector3 Temp_EnlargeScale;//放大
    private Vector3 Temp_ReduceScale;//缩小
    private float Temp_BrightenScale;//变亮
    private float Temp_DarkenScale;//变暗
 
    private ScrollRect _scrollView;
    //grid
    private Transform _content;
    //子节点的坐标
    private List<float> _childrenPos = new List<float>();
    //当前需要运动到的目标坐标
    private float _targetPos;
 
    /// <summary>
    /// 当前中心child索引
    /// </summary>
    private int _curCenterChildIndex = 0;
    /// <summary>
    /// 上一次中心child索引
    /// </summary>
    private int _lastCenterChildIndex;
    /// <summary>
    /// 是否改变了中心组件做引
    /// </summary>
    private bool _isChangeCenter = false;
    /// <summary>
    /// 中心组件改变委托
    /// 用于触发改变中心组件后切换对应数据
    /// </summary>
    public delegate void CenterChangedCallBack(int _index);
    public CenterChangedCallBack _centerChanged;
 
    /// <summary>
    /// 当前中心ChildItem
    /// </summary>
    public GameObject CurCenterChildItem
    {
        get
        {
            GameObject centerChild = null;
            if (_content != null && _curCenterChildIndex >= 0 && _curCenterChildIndex < _content.childCount)
            {
                centerChild = _content.GetChild(_curCenterChildIndex).gameObject;
            }
            return centerChild;
        }
    }
 
    void Awake()
    {
        //初始化
        _curCenterChildIndex = 0;
        _lastCenterChildIndex = _curCenterChildIndex;
        Temp_EnlargeScale = CenterChildScale;
        Temp_ReduceScale = CenterChildScale;
        Temp_BrightenScale = MaskOpacity;
        Temp_DarkenScale = MaskOpacity;
 
        _scrollView = GetComponent<ScrollRect>();
        _centerChanged = SwitchCat;
        if (_scrollView == null)
        {
            Debug.LogError("错误：ScrollRect为空");
            return;
        }
        _content = _scrollView.content;
 
        LayoutGroup layoutGroup = null;
        layoutGroup = _content.GetComponent<LayoutGroup>();
 
        if (layoutGroup == null)
        {
            Debug.LogError("错误：LayoutGroup Component为空");
        }
        _scrollView.movementType = ScrollRect.MovementType.Unrestricted;
        float spacing = 0f;
        //根据dir计算坐标，Horizontal：存x，Vertical：存y
        switch (Dir)
        {
            case ScrollDir_UGUI.Horizontal:
                if (layoutGroup is HorizontalLayoutGroup)
                {
                    float childPosX = _scrollView.GetComponent<RectTransform>().rect.width * 0.5f - GetChildItemWidth(0) * 0.5f;
                    spacing = (layoutGroup as HorizontalLayoutGroup).spacing;
                    _childrenPos.Add(childPosX);
                    for (int i = 1; i < _content.childCount; i++)
                    {
                        childPosX -= GetChildItemWidth(i) * 0.5f + GetChildItemWidth(i - 1) * 0.5f + spacing;
                        _childrenPos.Add(childPosX);
                    }
                }
                else if (layoutGroup is GridLayoutGroup)
                {
                    GridLayoutGroup grid = layoutGroup as GridLayoutGroup;
                    float childPosX = _scrollView.GetComponent<RectTransform>().rect.width * 0.5f - grid.cellSize.x * 0.5f;
                    _childrenPos.Add(childPosX);
                    for (int i = 0; i < _content.childCount - 1; i++)
                    {
                        childPosX -= grid.cellSize.x + grid.spacing.x;
                        _childrenPos.Add(childPosX);
                    }
                }
                else
                {
                    Debug.LogError("错误：Horizontal ScrollView正在使用VerticalLayoutGroup");
                }
                break;
            case ScrollDir_UGUI.Vertical:
                if (layoutGroup is VerticalLayoutGroup)
                {
                    float childPosY = -_scrollView.GetComponent<RectTransform>().rect.height * 0.5f + GetChildItemHeight(0) * 0.5f;
                    spacing = (layoutGroup as VerticalLayoutGroup).spacing;
                    _childrenPos.Add(childPosY);
                    for (int i = 1; i < _content.childCount; i++)
                    {
                        childPosY += GetChildItemHeight(i) * 0.5f + GetChildItemHeight(i - 1) * 0.5f + spacing;
                        _childrenPos.Add(childPosY);
                    }
                }
                else if (layoutGroup is GridLayoutGroup)
                {
                    GridLayoutGroup grid = layoutGroup as GridLayoutGroup;
                    float childPosY = -_scrollView.GetComponent<RectTransform>().rect.height * 0.5f + grid.cellSize.y * 0.5f;
                    _childrenPos.Add(childPosY);
                    for (int i = 1; i < _content.childCount; i++)
                    {
                        childPosY += grid.cellSize.y + grid.spacing.y;
                        _childrenPos.Add(childPosY);
                    }
                }
                else
                {
                    Debug.LogError("错误：Vertical ScrollView正在使用HorizontalLayoutGroup");
                }
                break;
        }
        //初始化ScrollView起始位置
        SetCenterChild(0);
        _isChangeCenter = true;
    }
 
    private float GetChildItemWidth(int index)
    {
        return (_content.GetChild(index) as RectTransform).sizeDelta.x;
    }
 
    private float GetChildItemHeight(int index)
    {
        return (_content.GetChild(index) as RectTransform).sizeDelta.y;
    }
 
    void Update()
    {
        //中心组件改变时触发委托
        if (_isChangeCenter)
        {
            CenterChanged(_curCenterChildIndex);
            _isChangeCenter = false;
            _lastCenterChildIndex = _curCenterChildIndex;
        }
        if (AddMask)
        {
            //开启遮罩标志
            _needChangingCapacity = true;
        }
        else
        {
            //关闭遮罩
            _needChangingCapacity = false;
        }
        if (_isScaling)
        {
            //插值缩放
            for (int i = 0; i < _content.childCount; i++)
            {
                if (i == _curCenterChildIndex)
                {
                    //居中的放大
                    Temp_EnlargeScale = new Vector3(Mathf.Lerp(CurCenterChildItem.transform.localScale.x, CenterChildScale.x, ScaleSpeed * Time.deltaTime),
                                                    Mathf.Lerp(CurCenterChildItem.transform.localScale.y, CenterChildScale.x, ScaleSpeed * Time.deltaTime),
                                                    Mathf.Lerp(CurCenterChildItem.transform.localScale.z, CenterChildScale.x, ScaleSpeed * Time.deltaTime));
                    CurCenterChildItem.transform.localScale = Temp_EnlargeScale;
                }
                else
                {
                    Temp_ReduceScale = new Vector3(Mathf.Lerp(_content.GetChild(i).gameObject.transform.localScale.x, 1f, ScaleSpeed * Time.deltaTime),
                                                   Mathf.Lerp(_content.GetChild(i).gameObject.transform.localScale.y, 1f, ScaleSpeed * Time.deltaTime),
                                                   Mathf.Lerp(_content.GetChild(i).gameObject.transform.localScale.z, 1f, ScaleSpeed * Time.deltaTime));
                    _content.GetChild(i).gameObject.transform.localScale = Temp_ReduceScale;
                }
            }
            //修改透明度
            if (AddMask)
            {
                if (_needChangingCapacity)
                {
                    if (MaskCheck() == false)
                    {
                        Debug.LogError("错误：没有找到遮罩资源或者部分子组件缺少遮罩资源");
                        AddMask = false;
                    }
                    else
                    {
                        for (int i = 0; i < ChildMasks.Count; i++)
                        {
                            if (i == _curCenterChildIndex)
                            {
                                //居中的变亮
                                Temp_BrightenScale = Mathf.Lerp(ChildMasks[i].image.color.a, 0f, OpacitySpeed * Time.deltaTime);
                                SetChildOpacity(ChildMasks[i].image, Temp_BrightenScale);
                            }
                            else
                            {
                                Temp_DarkenScale = Mathf.Lerp(ChildMasks[i].image.color.a, MaskOpacity, OpacitySpeed * Time.deltaTime);
                                SetChildOpacity(ChildMasks[i].image, Temp_DarkenScale);
                            }
                        }
                    }
                    if (ChildMasks[_curCenterChildIndex].image.color.a <= 0.01f)
                    {
                        _needChangingCapacity = false;
                        //_needReturnCapacity = true;
                        _isScaling = false;
                    }
                }
            }
            else
            {
                //去掉遮罩
                for (int i = 0; i < ChildMasks.Count; i++)
                {
                    if (ChildMasks[i].image != null)
                    {
                        SetChildOpacity(ChildMasks[i].image, 0f);
                    }                  
                }
            }
        }
        
        if (_isCentering)
        {
            Vector3 v = _content.localPosition;
         
            //插值居中
            switch (Dir)
            {
                case ScrollDir_UGUI.Horizontal:
                    v.x = Mathf.Lerp(_content.localPosition.x, _targetPos, TransformSpeed * Time.deltaTime);
                    _content.localPosition = v;
                    if (Math.Abs(_content.localPosition.x - _targetPos) < 0.01f)
                    {
                        _isCentering = false;
 
                    }
                    break;
                case ScrollDir_UGUI.Vertical:
                    v.y = Mathf.Lerp(_content.localPosition.y, _targetPos, TransformSpeed * Time.deltaTime);
                    _content.localPosition = v;
                    if (Math.Abs(_content.localPosition.y - _targetPos) < 0.01f)
                    {
                        _isCentering = false;
 
                    }
                    break;
            }
        }
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        _isScaling = true;
        switch (Dir)
        {
            case ScrollDir_UGUI.Horizontal:
                _targetPos = FindClosestChildPos(_content.localPosition.x, out _curCenterChildIndex);
                break;
            case ScrollDir_UGUI.Vertical:
                _targetPos = FindClosestChildPos(_content.localPosition.y, out _curCenterChildIndex);
                break;
        }
        _needChangingCapacity = true;
        _isChangeCenter = true;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        switch (Dir)
        {
            case ScrollDir_UGUI.Horizontal:
                _targetPos = FindClosestChildPos(_content.localPosition.x, out _curCenterChildIndex);
                break;
            case ScrollDir_UGUI.Vertical:
                _targetPos = FindClosestChildPos(_content.localPosition.y, out _curCenterChildIndex);
                break;
        }
        _isCentering = true;
        _needChangingCapacity = true;
        if (_lastCenterChildIndex != _curCenterChildIndex)
        {
            _isChangeCenter = true;
        }
        //_isScaling = false;
    }
 
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isCentering = false;
 
        _curCenterChildIndex = -1;
    }
    /// <summary>
    /// 计算离中心最近的子组件
    /// </summary>
    /// <param name="currentPos"></param>
    /// <param name="curCenterChildIndex"></param>
    /// <returns></returns>
    private float FindClosestChildPos(float currentPos, out int curCenterChildIndex)
    {
        float closest = 0;
        float distance = Mathf.Infinity;
        curCenterChildIndex = -1;
        for (int i = 0; i < _childrenPos.Count; i++)
        {
            float p = _childrenPos[i];
            float d = Mathf.Abs(p - currentPos);
            if (d < distance)
            {
                distance = d;
                closest = p;
                curCenterChildIndex = i;
            }
        }
        return closest;
    }
    /// <summary>
    /// 检查遮罩是否丢失,True-未丢失，False-丢失
    /// </summary>
    /// <returns></returns>
    public bool MaskCheck()
    {
        if (ChildMasks == null || ChildMasks.Count == 0 || ChildMasks.Count != _content.childCount)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < ChildMasks.Count; i++)
            {
                if (ChildMasks[i].image == null)
                {
                    return false;
                }               
            }
        }
        return true;
    }
    /// <summary>
    /// 设置组件的遮罩透明度
    /// </summary>
    public void SetChildOpacity(Image img, float opacity)
    {
        if (img != null)
        {
            img.color = new Color(1, 1, 1, opacity);
        }
    }
    /// <summary>
    /// 强制设置当前居中的子物体
    /// 用于点击事件
    /// </summary>
    /// <param name="_index"></param>
    public void SetCenterChild(int _index) 
    {      
        _curCenterChildIndex = _index;
        _isCentering = true;
        _needChangingCapacity = true;
        _isScaling = true;
        _targetPos = _childrenPos[_index];
        if (_curCenterChildIndex != _lastCenterChildIndex)
        {
            _isChangeCenter = true;
        }
    }
    /// <summary>
    /// 中心组件改变（委托）
    /// 用于根据中心组件变化触发对应数据变化
    /// </summary>
    public void CenterChanged(int _index)
    {
        if (_centerChanged != null)
        {
            _centerChanged(_index);          
        }
    }

    public void SwitchCat(int _index)
    {
        Cat cat = CatManager.Instance.GetCat(61001);
        switch (_index)
        {
            case 0:
                cat = CatManager.Instance.GetCat(61001);break;
            case 1:
                cat = CatManager.Instance.GetCat(61002);break;
            case 2:
                cat = CatManager.Instance.GetCat(61003);break;
            case 3:
                cat = CatManager.Instance.GetCat(61004);break;
        }
        UIManager.Instance.panelCouncil.GetComponent<CouncilView>().SwitchCatDisplay(cat);
    }

    public void ButtonSwitchLeft_OnClick()
    {
        if (_curCenterChildIndex != 0)
        {
            SetCenterChild(_curCenterChildIndex - 1);
            _isChangeCenter = true;
        }
    }
    public void ButtonSwitchRight_OnClick()
    {
        if (_curCenterChildIndex != 3)
        {
            SetCenterChild(_curCenterChildIndex + 1);
            _isChangeCenter = true;
        }
    }
}