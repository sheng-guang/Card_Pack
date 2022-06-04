using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pack;

public class Cam : MonoBehaviour
{
    static Cam() { NewGameClear.AddToNewGameClearList(() => Followers.NewGame_Clear()); }
    public Camera MainCamera;
    public Camera UI3DCamera;
    Transform group;
    public static Cam instance { get; private set; } = null;
    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        group = transform.GetChild(0);
        {

            tar_euler_angle = transform.localEulerAngles;

            if (tar_euler_angle.x > max_x) tar_euler_angle.x -=  360;
        }
    }


    // Update is called once per frame

    [Header("ro")]
    public float ro_speed = 1;
    public float max_x = 89;
    public float min_x = -89;

   public  Vector3 tar_euler_angle;
    void ro()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //tar_euler_angle = transform.eulerAngles;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 delta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * ro_speed;

            tar_euler_angle += new Vector3(-delta.y, delta.x);
            if (tar_euler_angle.x > max_x) { tar_euler_angle.x = max_x; }
            if (tar_euler_angle.x < min_x) { tar_euler_angle.x = min_x; }
            transform.eulerAngles = tar_euler_angle;

        }
    }
    [Header("dis")]
    public float tar_dis = -10;
    public float dis_speed = 5;
    public float back_limit = -100, foward_limit = 0;
    void dis()
    {
        var dis = Input.GetAxis("Mouse ScrollWheel");
        tar_dis += dis * dis_speed;
        if (tar_dis < back_limit) tar_dis = back_limit;
        if (tar_dis > foward_limit) tar_dis = foward_limit;
        group.transform.localPosition = new Vector3(0, 0, tar_dis);
    }
    [Header("follow")]
    public bool smoth = false;

    public float maxDis = 5;
    void follow()
    {
        if (HaveTarget == false) return;
        if (smoth)
        {
            Vector3 dis = SmothTar - transform.position;
            Vector3 dis_normal = dis.normalized;
            float dis_flo = dis.magnitude;
            if (dis_flo > maxDis) dis_flo = maxDis;
            float dis_2 = dis_flo * dis_flo;
            Vector3 tomove = dis_2 * dis_normal * Time.unscaledDeltaTime;
            if (tomove.magnitude >= dis.magnitude) tomove = dis;
            transform.position += tomove;
        }
        else
        {
            transform.position = SmothTar;
        }


    }
    [Header("MAn")]
    public bool followMA = false;

    public static void UpdatePoss()
    {
        instance.Fresh();
    }
    public void Fresh()
    {
        FigureV3();
        ro();
        dis();
        follow();
        Update_extra();
        FollowUpdate();
    }
    void Update_extra() { if (smoth == false) follow(); }
    void FollowUpdate()
    {
        Followers.ForEach(x => x.FollowCamUpdate_());
    }


    void FigureV3()
    {
        if (followMA == false || MA_N <= 1) { SmothTar = Tar; return; }

        points[toWrite_index] = Tar;

        toWrite_index++;
        if (toWrite_index < MA_N == false) toWrite_index = 0;
        SmothTar = Vector3.zero;
        for (int i = 0; i < MA_N; i++)
        {
            SmothTar += points[i];
        }
        SmothTar /= MA_N;

    }
    public int MA_N = 3;
    int toWrite_index = 0;
    public static Vector3[] points = new Vector3[100];
    public static Vector3 SmothTar = Vector3.zero;

    //static----------------------------------------------------------------------------------------------
    public static Camera MainCam => instance.MainCamera;
    public static Camera UI3DCam => instance.UI3DCamera;
    public static void AddToFollowerList(ICameraFollowe_update follower,bool Keep=false)
    {
        //Debug.Log("cam add " + follower);
       Followers.AddToList(follower,Keep);
    }
    static UpdCollection<ICameraFollowe_update> Followers { get;  set; } = new UpdCollection<ICameraFollowe_update>();

    static bool HaveTarget => Target != null && Target.Equals(null) == false;
    public static IRealPoss Target { get; set; } = null;
    public static Vector3 Tar { get { return HaveTarget ? Target.VisualPoss : Vector3.zero; } }

    public static Vector3 camPoss { get { return instance == null ? Vector3.zero : instance.group.position; } }
    public static Vector3 foward()
    {
        if (instance == null) return Vector3.forward;
        return instance.transform.forward;
    }
    public static Vector3 right()
    {
        if (instance == null) return Vector3.right;
        return instance.transform.right;
    }
    public static Vector3 foward_x0z()
    {
        if (instance == null) return Vector3.forward;
        Vector3 fo = foward();
        fo.y = 0;
        return fo;
    }
    public static Vector3 eularangle_xyz()
    {
        if (instance == null) return Vector3.forward;
        return instance.transform.eulerAngles;
    }
    public static Vector3 eularangle_0y0()
    {
        if (instance == null) return Vector3.forward;
        var re = new Vector3(0, eularangle_xyz().y);

        return re;
    }


    public static Vector3 eula_in_cam_space(Vector3 eula)
    {
        if (instance == null) return Vector3.zero;
        return instance.transform.localToWorldMatrix.MultiplyVector(eula);
        //return instance.transform.InverseTransformDirection(eula);
    }
    public static void look_at(Vector3 t)
    { if (instance == null) return; instance.transform.LookAt(t, Vector3.up); }
}

//public static class CamExtra
//{
//    public static void AddToFollowCamList (this ICameraFollowe_update follower) { Cam.AddFollower(follower); }
//}
public interface ICameraFollowe_update
{
    void FollowCamUpdate_();
}
public struct V3 : IRealPoss
{
    public Vector3 RealPoss { get; set; }

    public Vector3 VisualPoss { get => RealPoss; set { RealPoss = value; } }
    
}
public static class CamExtra
{
    public static void AddToCamFollowerList(this ICameraFollowe_update upd,bool keep = false)
    {
        Cam.AddToFollowerList(upd,keep);
    }
}