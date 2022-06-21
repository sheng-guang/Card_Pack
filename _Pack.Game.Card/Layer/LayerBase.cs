using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface IHaveID
    {
        int ID { get; }
    }
    //各个游戏实体的基类
    //游戏实体即  Driver     Host      Player     Unit      LayerComp
    //与ue4的gameplay框架类似
    //在其他partial中确定继承的基类
    //layer------------------------------------------------------------------------------------------------------------------------------------------------------
    partial class Layer:IHaveID //base
    {
        //唯一ID
        public  virtual int ID {  get => upID;set { } }

        //上级ID
        public virtual int upID { get { return 0; } set { } }
        public abstract Transform transf { get; }
        public virtual LayerID up { get => IDs<LayerID>.Get(upID); set { upID = value.ID; } }

        //获取上级Driver、Host、Unit
        //todo 增加up缓存 每次都用get方法可能会增加耗时
        //public static Driver _driver => GameList.driver;
        //public static Host _host => GameList.NowHost;

        public static Driver driver => GameList.driver;
        public virtual Host host =>up.host;
        public virtual Player player => up.player;
        public virtual Unit unit => up.unit;

        //static方法
        //创建各种游戏单位
        public static Host CreatHost(string n) { return driver.CreatHost(n); }
        public static Player CreatPlayer(string n) { return driver.CreatPlayer(n); }
        public static Unit CreatUnit(string n) { return driver.CreatUnit(n); }
        public static ILayerComp CreatComp(string FullName, LayerID master) { return driver.CreatComp(FullName, master); }
        public static SkillBuilder CreatSkill(string FullName) { return driver.CreatSkill(FullName); }
        public static SkillBuilder CreatSkill<T>() { return driver.CreatSkill(typeof(T).Name); }

    }

    //隐藏类用于写虚方法供子类实现
    public abstract class _LayerID : Layer 
    {
        public virtual void OnSetID_Awake() { } 
    }
    partial  class LayerID : IIDTarget
    {
    }
    //拥有ID的Layer节点
    public abstract partial class LayerID : _LayerID//set id
    {
        public virtual void SetID(int id)
        {
            ID= id;
            GameList.AddToIDs(this,id);
        }
    }




