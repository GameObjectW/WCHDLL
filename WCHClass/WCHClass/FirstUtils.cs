using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    }
}
