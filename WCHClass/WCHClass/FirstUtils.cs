using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Math
{
    public static class FirstUtils {
        /// <summary>
        /// 将多个层的layerMask合并为一个
        /// </summary>
        /// <param name="hitCheckLayerNames">需要计算的Layer的名称数组</param>
        /// <returns></returns>
        public static int GetLayerMask(string[] hitCheckLayerNames)
        {
            List<int> hitCheckLayerIndexs = new List<int>();
            foreach (string hitCheckLayerName in hitCheckLayerNames)
            {
                hitCheckLayerIndexs.Add(LayerMask.NameToLayer(hitCheckLayerName));
            }
            return 
                hitCheckLayerIndexs.Aggregate(0, (current, i) => current | 1 << i);
        }
        /// <summary>
        /// 给定初始方向向量，并返回改向量绕给定轴旋转0-360度后的方向向量
        /// </summary>
        /// <param name="startVector">初始方向向量</param>
        /// <param name="aix">旋转轴</param>
        /// <returns></returns>
        public static Vector3 RotateVector3WithAixsRandom(Vector3 startVector, Vector3 aix)
        {
            return Quaternion.AngleAxis(Random.Range(0f, 360f), aix) * startVector.normalized;
        }
        public static float[] GetIntsFromRangeNoRepeat(float Mix, float Max, int Num)
        {
            float[] value = new float[Num];
            List<float> list = new List<float>();
            for (int i = 0; i < value.Length; i++)
            {
                while (true)
                {
                    float Rvalue = Random.Range(Mix, Max);
                    if (!list.Contains(t => Math.Abs(t - Rvalue) > 0.01f)) continue;
                    list.Add(Rvalue);
                    value[i] = Rvalue;
                    break;
                }
            }
            return value;
        }
        /// <summary>
        /// 在一定范围内随机获取N个数（float）
        /// </summary>
        /// <param name="Mix">随机最小边界（包含）</param>
        /// <param name="Max">随机最大边界（未包含）</param>
        /// <param name="Num">获取数量</param>
        /// <returns></returns>
        public static int[] GetIntsFromRangeNoRepeat(int Mix, int Max, int Num)
        {
            int[] value = new int[Num];
            List<int> list = new List<int>();
            for (int i = 0; i < value.Length; i++)
            {
                while (true)
                {
                    int Rvalue = Random.Range(Mix, Max);
                    if (!list.Contains(t => t - Rvalue == 0)) continue;
                    list.Add(Rvalue);
                    value[i] = Rvalue;
                    break;
                }
            }
            return value;
        }

        /// <summary>
        /// 清理内存(一般在切换场景的时候调用)
        /// </summary>
        public static void ClearMemory()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        /// <summary>
        /// 查找子对象
        /// </summary>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static Transform FindTheChild(GameObject goParent, string childName)
        {
            Transform searchTrans = goParent.transform.Find(childName);
            if (searchTrans == null)
            {
                foreach (Transform trans in goParent.transform)
                {
                    searchTrans = FindTheChild(trans.gameObject, childName);
                    if (searchTrans != null)
                    {
                        return searchTrans;
                    }
                }
            }
            return searchTrans;
        }

        /// <summary>
        /// 获取子物体的脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T GetTheChildComponent<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTrans = FindTheChild(goParent, childName);
            if (searchTrans != null)
            {
                return searchTrans.gameObject.GetComponent<T>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 给子物体添加父对象
        /// </summary>
        /// <param name="parentTrs">父对象的方位</param>
        /// <param name="childTrs">子对象的方位</param>
        public static void AddChildToParent(Transform parentTrs, Transform childTrs)
        {
            //childTrs.parent = parentTrs; //Original Method
            childTrs.SetParent(parentTrs, false);
            childTrs.localPosition = Vector3.zero;
            childTrs.localScale = Vector3.one;
            childTrs.localEulerAngles = Vector3.zero;
        }
    }
}
