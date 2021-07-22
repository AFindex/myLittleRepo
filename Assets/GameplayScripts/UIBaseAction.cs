using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseAction : MonoBehaviour
{
    [Flags]
    public enum ChooseType
    {
        XAxis = 0,
        YAxis = 1<<0,
        ZAxis = 1<<1,
        
    }
    /// <summary>
    /// 返回startPos 和 endPos 之间，规约化的坐标集
    /// </summary>
    /// <param name="startPos"> 空间中任意位置，需要规约化 </param>
    /// <param name="endPos"> 空间中任意位置，需要规约化 </param>
    /// <param name="chooseType"> 坐标集考虑的坐标 </param>
    /// <returns> startPos 和 endPos 之间，规约化的坐标集 </returns>
    public static List<Vector3> ChooseMultiObjectBase(Vector3 startPos, Vector3 endPos, ChooseType chooseType)
    {
        Vector3 start = startPos;
        Vector3 end = endPos;
        Vector3 len = end - start;
        Vector3 dir = len.normalized;
        dir.x = dir.x > 0 ? 1 : -1;
        dir.y = dir.y > 0 ? 1 : -1;
        dir.z = dir.z > 0 ? 1 : -1;
        
        float step = 0.4f;
        len /= step;
        len.x = len.x > 0 ? len.x : -len.x;
        len.y = len.y > 0 ? len.y : -len.y;
        len.z = len.z > 0 ? len.z : -len.z;

        len.x = len.x == 0 ? 1 : len.x;
        len.y = len.y == 0 ? 1 : len.y;
        len.z = len.z == 0 ? 1 : len.z;
        
        List<Vector3> ChoosedPos = new List<Vector3>();
        var isX = chooseType & ChooseType.XAxis;
        var isY = chooseType & ChooseType.YAxis;
        var isZ = chooseType & ChooseType.ZAxis;
        
        //start += (step * dir);
        for (int x = 0;x<len.x; x++)
        {
            for (int y = 0;y<len.y; y++)
            {
                for (int z = 0;z<len.z; z++)
                {
                    Vector3 eachPos = startPos;
                    if (isY == ChooseType.YAxis)
                    {
                        eachPos.y += (y * step * dir.y);
                    }               
                    if (isX == ChooseType.XAxis)
                    {
                        eachPos.x += (x * step * dir.x);
                    }
                    if (isZ == ChooseType.ZAxis)
                    {
                        eachPos.z += (z * step * dir.z);
                    }
                    //add
                    ChoosedPos.Add(eachPos);
                    if (isZ != ChooseType.ZAxis)
                    {
                        break;
                    }
                }
                if (isY != ChooseType.YAxis)
                {
                    break;
                }
            }
            if (isX != ChooseType.XAxis)
            {
                break;
            }
        }
        return ChoosedPos;
    }
    /// <summary>
    /// 多个物体选择的显示
    /// </summary>
    /// <param name="chooseShow"> 显示选择框的GameObj </param>
    /// <param name="realScale"> 原始大小 </param>
    /// <param name="startPos"> 空间中任意位置，需要规约化 </param>
    /// <param name="endPos"> 空间中任意位置，需要规约化 </param>
    /// <param name="chooseType"> 坐标集考虑的坐标 </param>
    /// <param name="OnReset"> 变更选择平面 </param>
    /// <returns> 返回startPos 和 endPos 之间，规约化的坐标集 </returns>
    public static List<Vector3> ChooseMultiObjectShow(GameObject chooseShow, Vector3 realScale, Vector3 startPos, Vector3 endPos, ChooseType chooseType, Action OnReset = null)
    {
        OnReset?.Invoke();
        
        Vector3 len = endPos - startPos;
        float step = 0.4f;
        len /= step;
        
        float lenx = len.x == 0 ? 1 : len.x;
        float lenz = len.z == 0 ? 1 : len.z;
        float leny = len.y == 0 ? 1 : len.y;
        
        var isX = chooseType & ChooseType.XAxis;
        var isY = chooseType & ChooseType.YAxis;
        var isZ = chooseType & ChooseType.ZAxis;

        Vector3 objPosDiff = len;
        if(len.x != 0) objPosDiff.x = objPosDiff.x > 0 ? objPosDiff.x - 1 : objPosDiff.x + 1;
        if(len.y != 0) objPosDiff.y = objPosDiff.y > 0 ? objPosDiff.y - 1 : objPosDiff.y + 1;
        if(len.z != 0) objPosDiff.z = objPosDiff.z > 0 ? objPosDiff.z - 1 : objPosDiff.z + 1;
        Vector3 objPos = startPos + (objPosDiff*step) / 2;
        Vector3 temp = realScale;
        // xAxis
        if (isX == ChooseType.XAxis)
        {
            temp.x = step * lenx;
        }else
        {
            temp.x = realScale.x;
            objPos.x = startPos.x;
        }
        // yAxis
        if (isY == ChooseType.YAxis)
        {
            temp.y = step * leny;
        }
        else
        {
            temp.y = realScale.y;
            objPos.y = startPos.y;
        }
        // zAxis
        if (isZ == ChooseType.ZAxis)
        {
            temp.z = step * lenz;
        }else
        {
            temp.z = realScale.z;
            objPos.z = startPos.z;
        }
        chooseShow.transform.localScale = temp;
        chooseShow.transform.position = objPos;
        
        List<Vector3> choosedPos = ChooseMultiObjectBase(startPos,endPos,chooseType);
        return choosedPos;
    }
    
    /// <summary>
    /// 单个物体选择的显示
    /// </summary>
    /// <param name="chooseShow"> 显示选择框的GameObj </param>
    /// <param name="startPos"> 空间中任意位置，不需要规约化 </param>
    /// <returns> 返回规约化后的坐标位置 </returns>
    public static Vector3 ChooseSingleObjectBase(GameObject chooseShow, Vector3 startPos)
    {
        Vector3 ChoosedPos = startPos;
        ChoosedPos /= 0.4f;
        chooseShow.transform.position = ChoosedPos;
        return ChoosedPos;
    }

    /// <summary>
    /// 生成多个(单个)物体
    /// </summary>
    /// <param name="GenPos">规约化后的坐标集</param>
    /// <param name="beGenedObj">被生成的物体</param>
    /// <param name="OnGenTodo">生成物体时的回调</param>
    /// <returns>生成的集合</returns>
    public static List<GameObject> GenMultiObj(List<Vector3> GenPos, GameObject beGenedObj, Action<GameObject> OnGenTodo = null)
    {
        List<GameObject> objs = new List<GameObject>();
        foreach (var pos in GenPos)
        {
            GameObject temp = Instantiate(beGenedObj, pos, Quaternion.identity);
            OnGenTodo?.Invoke(temp);
            objs.Add(temp);
        }
        return objs;
    }
    /// <summary>
    ///  三维坐标判断
    /// </summary>
    /// <param name="hitDir"></param>
    /// <param name="OnRightDirTodo"> 最相关得维度回调 </param>
    /// <param name="OnleftDirTodo"> 最不相关得维度回调 </param>
    public static void CheckVectorDir(Vector3 hitDir, Action<ChooseType,bool> OnRightDirTodo = null, Action<ChooseType,bool> OnleftDirTodo = null)
    {
        hitDir = hitDir.normalized;
        float xDirDot = Vector3.Dot(hitDir, Vector3.right);
        float xDirDotAbs = xDirDot > 0 ? xDirDot : -xDirDot;
        float yDirDot = Vector3.Dot(hitDir, Vector3.up);
        float yDirDotAbs = yDirDot > 0 ? yDirDot : -yDirDot;
        float zDirDot = Vector3.Dot(hitDir, Vector3.forward);
        float zDirDotAbs = zDirDot > 0 ? zDirDot : -zDirDot;
        // rightDir
        if (xDirDotAbs >= yDirDotAbs && xDirDotAbs >= zDirDotAbs)
        {
            OnRightDirTodo?.Invoke(ChooseType.XAxis,xDirDot>0);
        }
        else if (yDirDotAbs >= xDirDotAbs && yDirDotAbs >= zDirDotAbs)
        {
            OnRightDirTodo?.Invoke(ChooseType.YAxis,yDirDot>0);
        }
        else 
        {
            OnRightDirTodo?.Invoke(ChooseType.ZAxis,zDirDot>0);
        }
        // leftDir
        //Debug.Log($"yDot:{yDirDotAbs}");
        if (yDirDot < 0.3 && yDirDot > -1)
        {
            OnleftDirTodo?.Invoke(ChooseType.YAxis,yDirDot>0);
        }
        else
        {
            Vector3 xzDir = hitDir;
            xzDir.y = 0;
            float xPanelDirDot = Vector3.Dot(xzDir, Vector3.right);
            float xPanelDirDotAbs = xPanelDirDot > 0 ? xPanelDirDot : -xPanelDirDot;
            float zPanelDirDot = Vector3.Dot(xzDir, Vector3.forward);
            float zPanelDirDotAbs = zPanelDirDot > 0 ? zPanelDirDot : -zPanelDirDot;
            if (xPanelDirDotAbs > zPanelDirDotAbs)
            {
                OnleftDirTodo?.Invoke(ChooseType.XAxis,xPanelDirDot>0);
            }
            else
            {
                OnleftDirTodo?.Invoke(ChooseType.ZAxis,zPanelDirDot>0);
            }
        }
        
        
        //if (xDirDotAbs <= yDirDotAbs && xDirDotAbs <= zDirDotAbs)
        //{
        //    OnleftDirTodo?.Invoke(ChooseType.XAxis,xDirDot>0);
        //}
        //else if (yDirDotAbs <= xDirDotAbs && yDirDotAbs <= zDirDotAbs)
        //{
        //    OnleftDirTodo?.Invoke(ChooseType.YAxis,xDirDot>0);
        //}
        //else 
        //{
        //    OnleftDirTodo?.Invoke(ChooseType.ZAxis,xDirDot>0);
        //}
    }
}
