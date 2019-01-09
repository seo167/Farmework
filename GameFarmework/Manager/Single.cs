using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmework {
    public class Single<T>:MonoBehaviourSimply where T: MonoBehaviourSimply,new(){
       
        private static T m_Instance;

        public static T Instance {
            get {
                if (m_Instance == null) {
                    m_Instance = new T();
                }
                return m_Instance;
            }
        }



        protected override void OnBeforeDestroy() {
            
        }


    }
}


