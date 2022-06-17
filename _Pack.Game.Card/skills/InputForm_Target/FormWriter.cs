using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class FormWriter : MonoBehaviour,ICameraFollowe_update
    {
        [RuntimeInitializeOnLoadMethod]
        static void CreatOne()
        {
            Debug.Log("[Load]"+nameof(FormWriter));

        }
         //static void VoidInvoke() { int i = 0; }
        static FormWriter()
        {
            var ne = new GameObject().AddComponent<FormWriter>();
            ne.name = "[" + nameof(FormWriter) + "]";
            DontDestroyOnLoad(ne);
            Cam.AddToFollowerList(ne, true);
        }

        //public static void SetOnPointForm(IFormGetter g) { OnPointFormGetter = g; }

        //public static IFormGetter OnPointFormGetter { get; private set; }
        protected static IPreView pre = PreView.GetPreView();
        public void ClearSkill()
        {
            Form = null;
            pre.Clear();
        }
        public static bool HaveForm=>(Form!=null);
        static InputForm Form;
        public static ITarget OnPoint => OnMousePoint.Target;

        public bool Tonext1=false;

        public static void TrySetFormGetter(IFormGetter ToSet)
        {
            //Debug.Log("try Set" + ToSet.GetCopyForm());
            if (Form != null) return;
            if (ToSet.NotNull_and_NotEqualNull() == false) return;
            if (ToSet.Useful == false) return;
            Form = ToSet.GetCopyForm();
            if (Form == null) return;
            pre.SetNewMaster(Form);
            Form.Write(OnPoint.GetData());
        }
        //public void TrySetForm()
        //{
        //    if (InputInfo.SetInputForm == false) return;
        //    if (Form != null) return;
        //    if (OnPointFormGetter.NotNull_and_NotEqualNull() == false) return;
        //    Form = OnPointFormGetter.GetCopyForm();
        //    if (Form == null) return;

        //    pre.SetNewMaster(Form);
        //    Form.Write(OnPoint.GetData());
        //    //Debug.Log("set form");
        //}
        public void ExtraToNext(ref int info)
        {
            if (Form == null) { Tonext1 = false;return; }
            if (InputInfo.ClickTarStep1) { Tonext1 = true; }
            if (InputInfo.ClickTarStep2) { if (Tonext1) { info |= InputInfo.ToNext; } Tonext1 = false; }
        }
        public void ExtraCancel(ref int info)
        {
            if (InputInfo.ClickCancel) info |= InputInfo.CanceAlllInput;
        }
        public void FollowCamUpdate_() { Fresh(); }
        public static bool IsWriting => Form != null;
        private void Fresh()
        {
            FreshOnPoint();
            if (Form == null) FreshAllInput();
            else FreshAllTarget();
            
            //TrySetForm();
            int info = InputInfo.None;
            ExtraToNext(ref info);
            ExtraCancel(ref info);

            if (Form != null)
            {
                Form.Write(OnPoint.GetData(),info);
                if (Form.finished) { Form.OnFinish(); ClearSkill(); }
            }
            pre.Fresh();
        }

        public void FreshAllInput()
        {

            HighLightSet<IHighLightTarget>.TurnOffAll();

            GameList.ForEachCallList(x => x.FreshAllUsefulForm());
            
            HighLightSet<IHighLightInput>.TurnOffOld();
        }
        public void FreshAllTarget()
        {
            HighLightSet<IHighLightInput>.TurnOffAll();
            
            //skill.FindUsefulTarget();
            HighLightSet<IHighLightTarget>.TurnOffOld();
        }
        public void FreshOnPoint()
        {
            OnMousePoint.Fresh3DPoint();
        }

 
    }


    public interface IHighLightTarget:IHighLightAble
    {

    }
    public interface IHighLightInput:IHighLightAble
    {

    }
    public static class InputInfo
    {
        public static bool SetInputForm => Input.GetMouseButtonUp(0);

        public static bool ClickTarStep1 => Input.GetMouseButtonDown(0);
        public static bool ClickTarStep2 => Input.GetMouseButtonUp(0);

        public static bool ClickCancel => Input.GetKeyDown(KeyCode.X);

        public const int None = 0;
        public const int ToNext = 1;
        public const int CanceAlllInput= 2;
        public static bool MeansToNext(this int info) { return info.MaskContain(InputInfo.ToNext); }

    }



