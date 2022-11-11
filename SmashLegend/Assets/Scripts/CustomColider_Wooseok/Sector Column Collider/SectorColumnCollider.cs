using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR

using UnityEditor.SceneManagement;

#endif


namespace Wooseok
{
    [ExecuteInEditMode]
    public class SectorColumnCollider : CustomCollider
    {
        // Start is called before the first frame update

        public Stick PivotSectorCol;
        public Skill_Colider StickCol;
        [SerializeField]
        private int StickNumber;
        int ex_StickNumber;
        [SerializeField]
        private float Angle;
        float ex_angle;

        [SerializeField]
        float mininterval;

        [SerializeField]
        public List<GameObject> ColliderList;
        [SerializeField]
        GameObject BaseObj;

        bool somethingchanged = false;
        


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (BaseObj == null)
            {
                BaseObj = Instantiate(BaseObject, this.transform); // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ø¼ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½âº» ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ® ï¿½ï¿½ï¿½ï¿½
                BaseObj.SetActive(false);
            }
            if (somethingchanged)
            {
                MakeCollider();
                UpdateEx();
                somethingchanged = false;
            }
        }

        public override void MakeCollider()
        {

#if UNITY_EDITOR



            var stage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            //Debug.Log("ItHappens");
            //Debug.Log("position: " + position);
            //Debug.Log("ex_position: " + ex_position);
            //Debug.Log("rotation: " + rotation);
            //Debug.Log("ex_rotation: " + ex_rotation);
            //Debug.Log("ex_scale: " + ex_scale);
            //Debug.Log("scale: " + scale);
            //Debug.Log("ex_pivot: " + ex_pivot);
            //Debug.Log("pivot: " + pivot);
            //Debug.Log("axis: " + axis);
            //Debug.Log("ex_axis: " + ex_axis);
            //Debug.Log("BaseObject: " + BaseObject);
            //Debug.Log("ex_BaseObject: " + ex_BaseObject);
            //Debug.Log("StickNumber: " + StickNumber);
            //Debug.Log("ex_StickNumber: " + ex_StickNumber);
            //Debug.Log("Angle: " + Angle);
            //Debug.Log("ex_angle: " + ex_angle);
            //Debug.Log("Vector3.Distance(ex_position,position)" + Vector3.Distance(ex_position, position));
            //Debug.Log("Vector3.Distance(ex_position,position) < mininterval" + (Vector3.Distance(ex_position, position) < mininterval));
            //Debug.Log("Vector3.Distance(ex_rotation,rotation)" + Vector3.Distance(ex_rotation, rotation));
            //Debug.Log("Vector3.Distance(ex_scale, scale)" + Vector3.Distance(ex_scale, scale));
            //Debug.Log("Vector3.Distance(ex_pivot, pivot)" + Vector3.Distance(ex_pivot, pivot));
            //Debug.Log("axis != ex_axis " + (axis != ex_axis));
            //Debug.Log("BaseObject != ex_BaseObject " + (BaseObject != ex_BaseObject));
            //Debug.Log("StickNumber != ex_StickNumber " + (StickNumber != ex_StickNumber));
            //Debug.Log("Mathf.Abs(Angle-ex_angle)" + Mathf.Abs(Angle - ex_angle));
            Stick pivotPoint = BaseObj.GetComponent<Stick>(); // ï¿½ï¿½ï¿½ï¿½ï¿?ï¿½ï¿½ï¿½ï¿½ & ï¿½ï¿½ï¿½ï¿½
            pivotPoint.PivotSectorCol = this;
            BaseObj.transform.localPosition = Position; // ¸·´ë±â ·ÎÄÃ Æ®·£½ºÆû ÁöÁ¤ÇØÁÖ±â1
            BaseObj.transform.localEulerAngles = Rotation; // 2
            BaseObj.transform.localScale = Scale; // 3



            if(stage != null) // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Î°ï¿½?
            {
                foreach (GameObject stick in ColliderList) // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿?ï¿½Ä±ï¿½
                {
                    GameObject.DestroyImmediate(stick);
                }
            }
            else // ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Î°ï¿½?
            {
                if(
                    PrefabUtility.GetCorrespondingObjectFromSource(this.gameObject) != null &&
                    PrefabUtility.GetPrefabInstanceHandle(this.gameObject) != null
                    )
                {
                    PrefabUtility.UnpackPrefabInstance(this.gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
                }

                foreach (GameObject stick in ColliderList) // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿?ï¿½Ä±ï¿½
                {
                    GameObject.DestroyImmediate(stick);
                }
            }




            for(int i = 0; i < ColliderList.Count; i++)
            {
                ColliderList[i] = null;
            }
            ColliderList.Clear();
            GameObject tempgo;
            //Debug.Log("b4: " + pivotPoint.StickCol.transform.localPosition);
            if(pivotPoint.gameObject != null)
            {
                pivotPoint.StickCol.gameObject.transform.localPosition = new Vector3(this.Scale.x * this.Pivot.x / 2, this.Scale.y * this.Pivot.y / 2, this.Pivot.z * this.Scale.z / 2);
            }
            //Debug.Log("aftr: " + pivotPoint.StickCol.transform.localPosition);
            if (StickNumber % 2 == 0) // Â¦ï¿½ï¿½
            {

                for (int i = 0; i < StickNumber; i++)
                {
                    tempgo = Instantiate(BaseObj, this.transform.position + BaseObj.transform.localPosition, Quaternion.Euler(this.transform.localEulerAngles + BaseObj.transform.localEulerAngles), transform);
                    switch (Axis)
                    {
                        case AXIS.X:
                            tempgo.transform.Rotate(Vector3.right, (((float)i - ((float)StickNumber / 2.0f)) * Angle / (StickNumber - 1)));
                            break;
                        case AXIS.Y:
                            tempgo.transform.Rotate(Vector3.up, (((float)i - ((float)StickNumber / 2.0f)) * Angle / (StickNumber - 1)));
                            break;
                        case AXIS.Z:
                            tempgo.transform.Rotate(Vector3.forward, (((float)i - ((float)StickNumber / 2.0f)) * Angle / (StickNumber - 1)));
                            break;
                        default:
                            break;
                    }
                    ColliderList.Add(tempgo);
                    tempgo.SetActive(true);

                }
            }
            else // È¦ï¿½ï¿½
            {
                for (int i = 0; i < StickNumber; i++)
                {
                    tempgo = Instantiate(BaseObj, this.transform.position + BaseObj.transform.localPosition, Quaternion.Euler(this.transform.localEulerAngles + BaseObj.transform.localEulerAngles), transform);
                    switch (Axis)
                    {
                        case AXIS.X:
                            tempgo.transform.Rotate(Vector3.right, (((float)i - ((float)StickNumber / 2.0f)) * Angle / (StickNumber - 1)));
                            break;
                        case AXIS.Y:
                            tempgo.transform.Rotate(Vector3.up, (((float)i - ((float)StickNumber / 2.0f)) * Angle / (StickNumber - 1)));
                            break;
                        case AXIS.Z:
                            tempgo.transform.Rotate(Vector3.back, (((float)i - ((float)StickNumber / 2.0f)) * Angle / (StickNumber - 1)));
                            break;
                        default:
                            break;
                    }
                    ColliderList.Add(tempgo);
                    tempgo.SetActive(true);
                }
            }
#endif

        }

        private void OnValidate()
        {
            somethingchanged = true;
        }

        public override void UpdateEx()
        {
            if (Vector3.Distance(ex_position, Position) < mininterval)
            {
                ex_position = Position;
            }
            if(Vector3.Distance(ex_rotation, Rotation) < mininterval)
            {
                ex_rotation = Rotation;
            }
            if(Vector3.Distance(ex_scale, Scale) < mininterval)
            {
                ex_scale = Scale;
            }
            if(Vector3.Distance(ex_pivot, Pivot) < mininterval)
            {
                ex_pivot = Pivot;
            }
            if(Axis != ex_axis)
            {
                ex_axis = Axis;
            }
            if(ex_BaseObject != BaseObject)
            {
                ex_BaseObject = BaseObject;
            }
            if(ex_StickNumber != StickNumber)
            {
                ex_StickNumber = StickNumber;
            }
            if(Mathf.Abs(360 - (ex_angle - Angle % 360)) < mininterval)
            {
                ex_angle = Angle;
            }
        }
    }
}
