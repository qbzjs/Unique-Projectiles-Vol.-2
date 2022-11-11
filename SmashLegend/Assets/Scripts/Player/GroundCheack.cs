using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junpyo
{
    public class GroundCheack : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        private LineRenderer lineRenderer;

        //그라운드 체크용
        public Ray GroundCheackRay = new Ray();
        public RaycastHit GroundHit = new RaycastHit();
        public LayerMask layerMask;
        private bool LineUpdate = true;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            GroundCheackRay.direction = -playerTransform.up;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            //Ray위치 초기화
            Vector3 GroundPos = playerTransform.position;
            GroundCheackRay.origin = new Vector3(GroundPos.x, GroundPos.y + 0.01f, GroundPos.z);

            //그라운드 체크
            if (Physics.Raycast(GroundCheackRay, out GroundHit, 100, layerMask))
            {
                //판넬위치 설정
                GroundPos = GroundHit.point;
                GroundPos.y += 0.01f;
                transform.position = GroundPos;

                if (Vector3.Distance(transform.position, playerTransform.position) > 0.1f)
                {
                    if (LineUpdate)
                    {
                        lineRenderer.enabled = true;
                    }

                    //공중점선 표현
                    lineRenderer.SetPosition(0, playerTransform.position);
                    lineRenderer.SetPosition(1, GroundPos);
                }
                else
                {
                    if (LineUpdate)
                    {
                        lineRenderer.enabled = false;
                    }
                }
            }
/*            else
            {
                //닿는게 없을 경우
                GroundPos = new Vector3(transform.position.x, GroundPos.y, transform.position.z);
                transform.position = GroundPos;
            }*/
        }

        public void LineSet()
        {
            LineUpdate = !LineUpdate;
        }

        public void ColorChange()
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
    }
}
