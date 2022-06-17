using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    public interface IFormGetter 
    {
        InputForm GetCopyForm();
        bool Useful { get; }
    }

    //n->1
    public static class InputNodeExtra
    {
        static InputNode CreatNew(this InputNode n,int kind)
        {
            var ne = new InputNode();
            ne.MasterSkill = n.MasterSkill;
            ne.NodeKind = kind;
            return ne;
        }
        public static InputNode CreatChild(this InputNode n,int kind)
        {
            var ne = n.CreatNew(kind);
            n.LinkChild(ne);
            return ne;
        }
        public static InputNode LinkChild(this InputNode n, InputNode ne)
        {
            n.Child = ne;
            ne.Parent = n;
            ne.UPViewNode = n;
            return ne;
        }
        public static InputNode CreatNextBro(this InputNode n, int kind)
        {
            var ne = n.CreatNew(kind);
            n.LinkNextBro(ne);
            return ne;
        }
        public static InputNode LinkNextBro(this InputNode n, InputNode ne)
        {
            n.NextBroNode = ne;
            ne.UpBro = n;
            ne.UPViewNode = n.UPViewNode;
            return ne;
        }
    }
    public class InputNode : IPreViewMasterNode
    {
        public InputSkill MasterSkill;
        public int NodeKind=0;
        public IPreViewMasterNode UPViewNode { get; set; }
        public InputNode UpBro { get; set; }
        public InputNode NextBroNode { get; set; }
        public InputNode Parent { get; set; }
        public InputNode Child { get; set; }
        public IEnumerable<string> Nodes => MasterSkill.NodeForm(NodeKind);




        //V3
        N<Vector3> _Point;
        public virtual N<Vector3> Point
        {
            get => LayerID.HasValue ? IDs<IIDTarget>.Get(LayerID.Value).RealPoss : _Point;

            set { _Point = value; }
        }
        public virtual N<Vector3> GetV3(string DataName)
        {
            if (DataName == nn.Point) return Point;
            return MasterSkill.GetV3(DataName, NodeKind);
        }
        //int
        public virtual N<int> LayerID { get; set; }
        public virtual N<int> GetInt(string DataName)
        {
            if (DataName == nn.LayerID) return LayerID;
            return MasterSkill.GetInt(DataName, NodeKind);
        }
        //float
        public virtual N<float> GetFloat(string DataName)
        {
         return MasterSkill.GetFloat(DataName,NodeKind);
        }
        
        //bool
        public virtual N<bool> GetBool(string DataName)
        {
            if (DataName == nn.Useful) return MasterSkill.TestNodeUseful(this);
            return MasterSkill.GetBool(DataName, NodeKind);
        }


        //static
        public static InputNode FinishNode = new InputNode();
        public static InputNode Canceled = new InputNode();
    }


    public class InputForm : InputNode,IPreViewMaster, IPreViewMasterNode // IInputForm
    {
        //基础数据
        public LayerID up { get; private set; }
        public virtual byte SkillKind { get; set; }
        public InputForm SetMasterSkill(InputSkill m)
        {
            up = m.up;
            MasterSkill = m;
            SkillKind = m.SkillKind;
            //Debug.Log(SkillKind);
            TryAddNode(this);
            ToWrite = this;
            return this;
        }
        public InputNode ToWrite;
        public void Write(IInputData data, int Extrainfo = InputInfo.None)
        {
            if (finished) return;
            ToWrite = MasterSkill.Write_GetNext(data, ToWrite, Extrainfo);
            TryAddNode(ToWrite);
        }
        public bool finished => ToWrite == FinishNode || (ToWrite == Canceled);
        void TryAddNode(InputNode n)
        {
            if (n == null) return;
            if (Created.Contains(n)) return;
            Created.Add(n);
            CreatedLis.Add(n);
        }
        HashSet<InputNode> Created = new HashSet<InputNode>();
        List<InputNode> CreatedLis = new List<InputNode>();




        //IPreViewMaster
        public override N<Vector3> Point => up.RealPoss;
        public override N<int> LayerID => up.ID;
        public int Count => CreatedLis.Count;
        public IPreViewMasterNode GetNode(int index) { return CreatedLis.GetElement(index); }

        public void OnFinish()
        {
            if (ToWrite != FinishNode) return;
            this.Send();
        }
    }




    public static class InputFormFunctions
    {
        public static void Serialize(this NetworkWriter w, InputForm value)
        {
            byte LastOption = 1;
            InputNode ToWrite = value;
            w.Serialize(ToWrite);
            while (LastOption != 5)
            {
                var Moved = MoveToNext(ref ToWrite, ref LastOption);
                if (Moved == false) continue;
                if (LastOption == 1 || LastOption == 3)
                {
                    w.WriteByte(LastOption);
                    w.Serialize(ToWrite);
                }
                else if (LastOption == 2 || LastOption == 4) { w.WriteByte(LastOption); }
            }
            w.WriteByte(5);
        }
        public static bool MoveToNext(ref InputNode Writed, ref byte DoneOption)
        {
            InputNode from = Writed;
            //1表示从父节点初次到这个节点
            //3表示从上一个兄节点到这个节点
            if (DoneOption == 1||DoneOption==3)
            {
               //如果有子节点则开始遍历子节点
                if (Writed.Child != null)
                {
                    Writed = Writed.Child;
                    DoneOption = 1;
                    return from != Writed;
                }
                //否则状态变为子节点遍历结束
                else
                {
                    DoneOption = 2;
                    return from != Writed;
                }
            }
            //2表示子节点全部遍历结束
           else if (DoneOption == 2)
            {
                //如果有下个兄弟节点
                if (Writed.NextBroNode != null)
                {
                    Writed = Writed.NextBroNode;
                    DoneOption = 3;
                    return from != Writed;
                }
                //如果有没有下个兄弟节点则表示
                //回到父节点
                //或回到上个兄弟节点
                else
                {
                    DoneOption = 4; return from != Writed;
                }
            }
            //4表示子遍历结束&&下兄弟遍历结束
            //准备返回兄或父
            else if (DoneOption == 4)
            {
                //如果有上个兄弟则回到上个兄弟
                if (Writed.UpBro != null)
                {
                    Writed = Writed.UpBro;
                    DoneOption = 4; return from != Writed;
                }
                //如果有父节点则回到父节点
                else if (Writed.Parent != null)
                {
                    Writed = Writed.Parent;
                    DoneOption = 2; return from != Writed;
                }
                //如果都没有说明到了根节点
                else 
                { 
                    DoneOption = 5;
                    return from != Writed;
                }
            }
            throw new Exception("UnknowOption  " + DoneOption);
        }
        public static void Serialize(this NetworkWriter w,InputNode v)
        {
            w.WriteInt(v.NodeKind);
            w.WriteNInt(v.LayerID);
            w.WriteNV3(v.Point);
        }
        public static InputForm Deserialize(this NetworkReader r)
        {
            var re = new InputForm();
            InputNode to = re;
            r.DeserializeNode(to);
            while (true)
            {
                var OptionKind = r.ReadByte();
                //生成子节点
                if (OptionKind == 1)
                {
                    to.LinkChild(r.DeserializeNode(new InputNode()));
                    to = to.Child;
                }
                //返回父节点
                else if (OptionKind == 2)
                {
                    to = to.Parent;
                }
                //生成兄弟节点
                else if (OptionKind == 3)
                {
                    to.LinkNextBro(r.DeserializeNode(new InputNode()));
                    to = to.NextBroNode;
                }
                //返回兄弟节点
                else if (OptionKind == 4)
                {
                    to = to.UpBro;
                }
                else if (OptionKind == 5) break;
            }
            return re;
        }
        public static InputNode DeserializeNode(this NetworkReader r,InputNode re)
        {
            re.NodeKind = r.ReadInt();
            re.LayerID = r.ReadNInt();
            re.Point = r.ReadNV3();
            return re;
        }



       public static void Send(this InputForm f)
        {
            MsgManager.ClientSendInput(f);
            //Debug.Log("send ");
            //Debug.Log(Time.time);
        }
    }
