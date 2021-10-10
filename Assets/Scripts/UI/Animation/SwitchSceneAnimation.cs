using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UI.Animation
{
    [RequireComponent(typeof(Image))]
    public class SwitchSceneAnimation : MonoBehaviour
    {
        private Image ImageSource;
        private int mCurFrame = 0;
        private float mDelta = 0;
        public float FPS = 22;
        public List<Sprite> SpriteFrames;
        public bool IsPlaying = false;
        public bool Foward = true;
        public bool AutoPlay = false;
        public bool Loop = false;
        public delegate void CallBack();
        public CallBack CallBackFunction;
        public int HandleCallBackFrame = 7;
        private bool HasHandleCallBack = false;
        public int FrameCount
        {

            get { return SpriteFrames.Count; }
        }

        void Awake()
        {

            ImageSource = GetComponent<Image>();
        }

        private void SetSprite(int idx)
        {

            ImageSource.sprite = SpriteFrames[idx];
            //该部分为设置成原始图片大小，如果只需要显示Image设定好的图片大小，注释掉该行即可。
            if (mCurFrame < FrameCount)
            {
                ImageSource.SetNativeSize();
            }
        }

        public void Play()
        {
            UIManager.Instance.SwitchDarkBackGround(true);
            UIManager.Instance.SwitchSceneAnimation.SetActive(true);
            SetSprite(0);
            CallBackFunction = null;
            mCurFrame = 0;
            IsPlaying = true;
            Foward = true;
            HasHandleCallBack = false;
        }

        public void Play(CallBack callBack)
        {
            UIManager.Instance.SwitchDarkBackGround(true);
            UIManager.Instance.SwitchSceneAnimation.SetActive(true);
            SetSprite(0);
            CallBackFunction = callBack;
            mCurFrame = 0;
            IsPlaying = true;
            Foward = true;
            HasHandleCallBack = false;
        }

        public void PlayReverse()
        {

            IsPlaying = true;
            Foward = false;
        }

        void Update()
        {

            if (!IsPlaying || 0 == FrameCount)
            {

                return;
            }

            mDelta += Time.deltaTime;
            if (mDelta > 1 / FPS)
            {

                mDelta = 0;
                if (Foward)
                {

                    mCurFrame++;
                }
                else
                {

                    mCurFrame--;
                }

                if (!HasHandleCallBack && mCurFrame >= HandleCallBackFrame)
                {
                    HasHandleCallBack = true;
                    if (CallBackFunction != null)
                    {
                        CallBackFunction.Invoke();
                    }
                }

                if (mCurFrame >= FrameCount)
                {

                    if (Loop)
                    {

                        mCurFrame = 0;
                    }
                    else
                    {
                        IsPlaying = false;
                        SetSprite(0);
                        UIManager.Instance.SwitchSceneAnimation.SetActive(false);
                        return;
                    }
                }
                else if (mCurFrame < 0)
                {

                    if (Loop)
                    {

                        mCurFrame = FrameCount - 1;
                    }
                    else
                    {
                        if (CallBackFunction != null)
                        {
                            CallBackFunction.Invoke();
                        }
                        IsPlaying = false;
                        SetSprite(0);
                        UIManager.Instance.SwitchSceneAnimation.SetActive(false);
                        return;
                    }
                }

                SetSprite(mCurFrame);
            }
        }

        public void Pause()
        {

            IsPlaying = false;
        }

        public void Resume()
        {

            if (!IsPlaying)
            {

                IsPlaying = true;
            }
        }

        public void Stop()
        {

            mCurFrame = 0;
            SetSprite(mCurFrame);
            IsPlaying = false;
        }

        public void Rewind()
        {

            mCurFrame = 0;
            SetSprite(mCurFrame);
            Play();
        }
    }
}