using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;
using UnityEngine.EventSystems;
namespace Pack
{
    partial class SkillComp : IPointerEnterHandler//target
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMousePoint.SetTarget(skill);
        }
    }

    public partial class SkillComp //����skill ����ͼƬ set up
    {
        public Skill skill { get; private set; }
        public void SetSkill(Skill s)
        {
            skill = s;
            OnSetSkill_Load();
        }
        public float ImageW_H()
        {
            var scale = ImgMesh.transform.lossyScale;
            return scale.x / scale.y;
        }
        public MeshRenderer ImgMesh;
        public virtual void OnSetSkill_Load()
        {
            StackSkillSpace.AddtoStackSpace(transform);
            this.AddToCamFollowerList();
            FollowCamUpdate_();
            ImgMesh.LoadTexture("_MainTex", ImageW_H(), skill.FullName, 0);

        }
    }
   
    public partial class SkillComp // ���� LineRender
    {
        public BezierLineMeshPoints LineStart;
        public BezierLineMeshPoints LineTar;
    
    }


    public partial class SkillComp : ICameraFollowe_update//ˢ��  update
    {
        public void FollowCamUpdate_()
        {
            FreshPoss();
            if (gameObject.activeInHierarchy == false) return;
            LineStart.Fresh(skill.IsStackTop, skill.unit.VisualPoss);
            bool show = skill.HasMoreThanOneTar && skill.IsStackTop;
            LineTar.Fresh(show, skill.TarV3Visual());
            FreshIcon();
        }
    }
    partial class SkillComp//ˢ��λ�� freshPoss
    {
        [Header("poss")]
        public Transform LittleOffset;
        public float AngleNormal = 30f;
        public float AngleTotal = 60f;
        VarChangeEvent<bool> activeEvent = new VarChangeEvent<bool>();
        public void FreshPoss()
        {

            bool active=true;
            //if(skill.IsPausing==false)active=false;
            if(skill.InStack==false)active=false;
            bool change = false;
            activeEvent.NewData(active, ref change);
            if (change) { gameObject.SetActive(active); }
            if (active == false) return;
            int to = skill.StackTotal - skill.StackInde-1;
            float TotalPre = AngleNormal * skill.StackTotal;
            float Sigle = AngleNormal;
            if (TotalPre > AngleTotal)
            {
                float OverTimes = TotalPre / AngleTotal;
                Sigle = AngleNormal / OverTimes;
            }

            float toAngle = Sigle * to;
            transform.localEulerAngles = new Vector3(0, 0, toAngle);
            LittleOffset.transform.localEulerAngles=new Vector3(0,-15,0);

        }
    }
    public partial class SkillComp//��ͣ��־  pausing icon
    {
        [Header("Pausing Color")]
        public string ColorName;
        public Color Pass=Color.cyan;
        public Color Pausing = Color.white;
        public float PassPower = 1.5f;
        public MaterialController SelfIcon;
        public List<MaterialController> PausingIcon;
        public void FreshIcon()
        {
            int ToPausingIndex = 0;
            for (int i = 0; i < GameList.playerList.Count; i++)
            {
                int id = GameList.playerList[i].ID;
                bool IsPausing=skill.TestIsPausing(id);
                if (id == skill.player.ID)
                {
                    if(IsPausing) SelfIcon.SetColor(ColorName, Pausing);
                    else SelfIcon.SetColor_Power(ColorName, Pass, PassPower);

                }
                else
                {
                    if (IsPausing) 
                        PausingIcon[ToPausingIndex].SetColor(ColorName, Pausing);
                    else PausingIcon[ToPausingIndex].SetColor_Power(ColorName, Pass, PassPower);
                    ToPausingIndex++;
                }
                
            }

        }
    }
    public partial class SkillComp : IPointerMoveHandler,IPointerDownHandler//��ͣ pause
    {
        public void Fresh()
        {
            if (FormWriter.IsWriting) return;
            if (Input.GetMouseButton(0)) skill.ClientCancelPause();
            if (Input.GetMouseButton(1)) skill.ClientPause();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Fresh();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            //Fresh();
        }
    }

    public partial class SkillComp : MonoBehaviour, IResGetter<SkillComp>, IRes//��Դ���� res 
    {
        public string DirectoryName => "Assets/"+nameof(SkillComp);

        public string PackName => "-";
        public virtual string KindName => nameof(SkillComp);

        public SkillComp GetNew(ResArgs a)
        {
            return Instantiate(this);
        }

        public object GetNewObject(ResArgs a)
        {
            return GetNew(a);
        }
        //

    }
}
