using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junpyo
{
    public class CameraManager : MonoBehaviour
    {
        private static CameraManager m_Instance;
        public static CameraManager Instance()
        {

            if (m_Instance == null)
            {
                m_Instance = GameObject.Find("CameraManager").GetComponent<CameraManager>();
            }

            return m_Instance;
        }

        [SerializeField] 
        public Transform target;

        [SerializeField] 
        private float distance = 5f;

        [SerializeField] 
        private float CameraSpeed = 3.0f;

        [SerializeField]
        private GameObject MainCamera_Root;
        private Camera MainCamera;

        [SerializeField]
        private GameObject DeadCamera_Root;
        private Camera DeadCamera;

        [HideInInspector]
        public bool IsDead;

        [SerializeField]
        private float ZoomInSpeed;

        [SerializeField]
        private float ZoomOutSpeed;

        [SerializeField]
        private float BattleZoomInSpeed;

        [SerializeField]
        private float BattleZoomOutSpeed;

        private Vector3 TargetCameraPos;

        public bool IsBattleMode;
        public float BattleDelay;

        public bool IsHilight;

        public float Test;

        public float ShakeDuration = 0.2f;
        public float magnitude;
        public bool Right = false;

        private Vector3 StartPos;

        private void Awake()
        {
            MainCamera = Camera.main;
            DeadCamera = DeadCamera_Root.GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            Vector3 TargetCameraPos = transform.forward * -distance + target.position;
            transform.position = Vector3.Lerp(transform.position, TargetCameraPos, CameraSpeed * Time.deltaTime);

            if(Input.GetKeyDown(KeyCode.O))
            {
                CameraShaking();
            }
        }

        private void Angle_Y()
        {
        }

        public void SetCameraPos()
        {    
            transform.position = transform.forward * -distance + target.position;
        }

        //�÷��̾ ���� ���
        public void CameraChange()
        {
            IsDead = !IsDead;
        
            //IsDead���°� true�� ��� Mainī�޶� ���� Deadī�޶� Ų��.
            MainCamera_Root.SetActive(!IsDead);
            DeadCamera_Root.SetActive(IsDead);
            //����ī�޶� �䵵 �Ȱ��� 30���� ����

            if (IsDead)
            {
                //���̶���Ʈ�� ��� ���� �����ߴ� ��ȿ���� ����
                IsHilight = true;
                StopCoroutine("BattleZoomIn");
                StopCoroutine("BattleZoomOut");

                MainCamera.fieldOfView = 35.0f;
                StartCoroutine(ZoomIn());
            }
            else
            {
                DeadCamera.fieldOfView = 50.0f;
                StartCoroutine(ZoomOut());
            }
        }

        IEnumerator ZoomOut()
        {
            target.GetComponent<CameraTarget>().BattleMode = false;

            while (true)
            {
                MainCamera.fieldOfView += ZoomOutSpeed * Time.deltaTime;

                if (MainCamera.fieldOfView >= 50.0f)
                {
                    MainCamera.fieldOfView = 50.0f;
                    IsHilight = false;
                    yield break;
                }
                yield return null;
            }
        }

        IEnumerator ZoomIn()
        {
            while (true)
            {
                DeadCamera.fieldOfView -= ZoomInSpeed * Time.deltaTime;

                if (DeadCamera.fieldOfView <= 35.0f)
                {
                    DeadCamera.fieldOfView = 35.0f;
                    yield break;
                }
                yield return null;
            }
        }

        public void BattleModeOn()
        {
            if (!IsHilight)
            {
                //IEnumerator temp = BattleZoomIn();
                StopCoroutine("BattleZoomIn");
                StartCoroutine("BattleZoomOut");
            }
        }

        public void BattleModeOff()
        {
            if (!IsHilight)
            {
                //IEnumerator temp = BattleZoomOut();
                StopCoroutine("BattleZoomOut");
                StartCoroutine("BattleZoomIn");
            }
        }

        IEnumerator BattleZoomIn()
        {
            yield return new WaitForSeconds(BattleDelay);

            target.GetComponent<CameraTarget>().BattleMode = false;

            while (true)
            {
                MainCamera.fieldOfView -= BattleZoomInSpeed * Time.deltaTime;

                if (MainCamera.fieldOfView <= 50.0f)
                {
                    MainCamera.fieldOfView = 50.0f;
                    yield break;
                }

                yield return null;
            }
        }
        IEnumerator BattleZoomOut()
        {
            while (true)
            {
                MainCamera.fieldOfView += BattleZoomOutSpeed * Time.deltaTime;

                if (MainCamera.fieldOfView >= 70.0f)
                {
                    MainCamera.fieldOfView = 70.0f;
                    yield break;
                }

                yield return null;
            }
        }

        public void CameraShaking()
        {
            StartPos = transform.position;
            StartCoroutine(Shaking());
        }

        IEnumerator Shaking()
        {
            float timer = 0;

            while (timer <= ShakeDuration)
            {
                transform.localPosition = (Vector3)Random.insideUnitSphere * magnitude + StartPos;

                timer += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = StartPos;
            yield break;
        }
    }
}