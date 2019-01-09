using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmework {
    public class SetObject{
        #region LocalPosition
        public static void SetLocalPosition(Transform transform, float x,float y,float z) {
            SetLocalPositionX(transform,x);
            SetLocalPositionY(transform, y);
            SetLocalPositionZ(transform, z);
        }

        public static void SetLocalPositionX(Transform transform,float x) {
            var demo = transform.localPosition;
            demo.x = x;
            transform.localPosition = demo;
        }

        public static void SetLocalPositionY(Transform transform, float y){
            var demo = transform.localPosition;
            demo.y = y;
            transform.localPosition = demo;
        }

        public static void SetLocalPositionZ(Transform transform, float z){
            var demo = transform.localPosition;
            demo.z= z;
            transform.localPosition = demo;
        }
        #endregion

        #region Position
        public static void SetPosition(Transform transform, float x, float y, float z)
        {
            SetPositionX(transform, x);
            SetPositionY(transform, y);
            SetPositionZ(transform, z);
        }

        public static void SetPositionX(Transform transform, float x){
            var demo = transform.position;
            demo.x = x;
            transform.position = demo;
        }

        public static void SetPositionY(Transform transform, float y){
            var demo = transform.position;
            demo.y = y;
            transform.position = demo;
        }

        public static void SetPositionZ(Transform transform, float z){
            var demo = transform.position;
            demo.z = z;
            transform.position = demo;
        }
        #endregion

        #region Rotate
        public static void SetRotate(Transform transform, float x,float y,float z)
        {
            SetRotateX(transform,x);
            SetRotateY(transform, y);
            SetRotateZ(transform, z);
        }

        public static void SetRotateX(Transform transform, float x){
            var demo = transform.rotation;
            demo.x = x;
            transform.rotation = demo;
        }

        public static void SetRotateY(Transform transform, float y){
            var demo = transform.rotation;
            demo.y = y;
            transform.rotation = demo;
        }

        public static void SetRotateZ(Transform transform, float z){
            var demo = transform.rotation;
            demo.z = z;
            transform.rotation = demo;
        }
        #endregion

        #region Scale

        static public void SetScale(Transform transform,float x,float y,float z) {
            SetScaleX(transform,x);
            SetScaleY(transform,y);
            SetScaleZ(transform,z);
        }

        static public void SetScaleX(Transform transform,float x) {
            var demo = transform.localScale;
            demo.x = x;
            transform.localScale = demo;
        }

        static public void SetScaleY(Transform transform, float y)
        {
            var demo = transform.localScale;
            demo.y = y;
            transform.localScale = demo;
        }

        static public void SetScaleZ(Transform transform, float z)
        {
            var demo = transform.localScale;
            demo.z = z;
            transform.localScale = demo;
        }

        #endregion

        //矩阵归一化
        static public void Identity(Transform transform) {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }

        static public void Hide(GameObject obj) {
            obj.SetActive(false);
        }

        static public void Show(GameObject obj) {
            obj.SetActive(true);
        }


    }

    public class Tool {
        /// <summary>
        /// 判断命中率
        /// </summary>
        /// <param name="num">传入0-1的数</param>
        /// <returns></returns>
        static public bool Precent(float num){
            num *= 100;
            return (Random.Range(0, 100) < num);
        }

        /// <summary>
        /// 获取数组里面的随机内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        static public T RandomForArray<T>(T[] values) {
            return values[Random.Range(0,values.Length)];
        }

        static public T RandomForList<T>(List<T> values) {
            return values[Random.Range(0, values.Count)];
        }

    }

}


