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

    public partial class SkillComp //set up
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
            ImgMesh.LoadTexture("_MainTex", ImageW_H(), skill.unit.FullName(), 0);

        }
    }
   
    public partial class SkillComp // LineRender
    {
        [Header("line")]
        public BezierLineMesh line;
        public Transform startPoint;
        public Transform Tail1;
        public Transform Tail2;

        public void FreshLine()
        {
            line.SetLineActive(skill.IsStackTop);
            if (skill.IsStackTop == false) return;

            line.SetPoint(startPoint.position, 0);
            line.SetPoint(Tail1.position, 1);
            line.SetPoint(Tail2.position, 2);

            var p = skill.unit.RealPoss;
             //p = p.ShowInNewCam(Cam.MainCam, Cam.UI3DCam);
            line.SetPoint(p, 3);
            
            line.FreshLine();
        }
  
    }
    public partial class SkillComp // LineRender2
    {
        [Header("lineTar")]
        public BezierLineMesh lineTar;
        public Transform startPointt;
        public Transform Tail1t;
        public Transform Tail2t;

        public void FreshLineTar()
        {
            bool show = skill.HasMoreThanOneTar && skill.IsStackTop;
            lineTar.SetLineActive(show);
            if (show == false) return;
            
            lineTar.SetPoint(startPointt.position, 0);
            lineTar.SetPoint(Tail1t.position, 1);
            lineTar.SetPoint(Tail2t.position, 2);
            lineTar.SetPoint(skill.TarV3Visual(), 3);
            //skill.TarID
            //var UnitPoss = skill.unit.RealPoss;
            //var p = UnitPoss.ShowInNewCam(Cam.MainCam, Cam.UI3DCam);
            //lineTar.SetPoint(p, 3);

            lineTar.FreshLine();
        }

    }


    public partial class SkillComp : ICameraFollowe_update//update
    {
        public void FollowCamUpdate_()
        {
            FreshPoss();
            if (gameObject.activeInHierarchy == false) return;
            FreshLine();
            FreshLineTar();
            FreshIcon();
        }
    }
    partial class SkillComp//freshPoss
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
    public partial class SkillComp//pausing icon
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
    public partial class SkillComp : IPointerMoveHandler,IPointerDownHandler//pause
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

    public partial class SkillComp : MonoBehaviour, IResGetter<SkillComp>, IRes//res
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
