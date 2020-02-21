using _2019_nCoV.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace _2019_nCoV.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ApplyQuery()
        {
            return View();
        }
        public ActionResult Zone()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult DongGuanIndex1()
        {
            return View();
        }
        public ActionResult DongGuanIndex2()
        {
            return View();
        }
        public ActionResult DongGuanIndex3()
        {
            return View();
        }
        public ActionResult JiangXiIndex1()
        {
            return View();
        }
        public ActionResult JiangXiIndex2()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        
        public ActionResult Add(string username, string userno, string sex, string bornyear, string phone, string dept, string province, string city, string county, string q1, string q2, string incity, string q3, string q4, string q5, string q6, string symptom, string q77symptom, string q8, string nowcity, string returndate, string vehicle, string q11trainno, string ridingdate, string q13,string deptonelevel, string depttwolevel, string deptthreelevel)
        {
            string sqlstr = "insert into nCoVRecordInfo(username,userno,sex,bornyear,phone,dept,province,city,county,q1, q2, incity, q3, q4, q5, q6, symptom, q77symptom, q8,nowcity,returndate,vehicle,q11trainno,RecordDate,ridingdate,q13,deptonelevel,depttwolevel,deptthreelevel)"
                + "values('"+ username + "', '" + userno+ "','"+sex + "','" + bornyear + "','" + phone + "','" + dept+"','" + province + "','" + city + "','" + county + "','" +  q1 + "','" + q2 + "','" + incity + "','" + q3+ "','"+q4+ "','"+q5+ "','"+q6+ "','"+symptom+ "','"+q77symptom+ "','"+q8 + "','" + nowcity + "','"+returndate+ "','"+vehicle+ "','"+q11trainno + "',getdate(),'"+ ridingdate + "','"+ q13 + "','" + deptonelevel + "','" + depttwolevel + "','" + deptthreelevel + "')";
            SQLHelper.ExcuteSQL(sqlstr);
            return null;
        }

        public ActionResult Query(string userno)
        {
            string sqlstr = "select username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county From nCoVRecordInfo where ID=(select max(ID) From nCoVRecordInfo where userno='" + userno + "')";
            var dt= SQLHelper.GetTable(sqlstr);
            if(dt.Rows.Count>0)
                return Json(new { username = dt.Rows[0]["username"], userno = dt.Rows[0]["userno"], sex = dt.Rows[0]["sex"], bornyear = dt.Rows[0]["bornyear"], phone = dt.Rows[0]["phone"], deptonelevel = dt.Rows[0]["deptonelevel"], depttwolevel = dt.Rows[0]["depttwolevel"], deptthreelevel = dt.Rows[0]["deptthreelevel"], province = dt.Rows[0]["province"], city = dt.Rows[0]["city"], county = dt.Rows[0]["county"] }, "text/html", JsonRequestBehavior.AllowGet);
            else
                return null;
        }
        public ActionResult DeptQueryOne()
        {
            string sqlstr = "select  onelevel,max(ID) ID From DepartmentInfo group by onelevel order by ID";
            var dt = SQLHelper.GetTable(sqlstr);
            if (dt.Rows.Count > 0)
            {
                List<DepartmentInfo> data = new List<DepartmentInfo>(); //实例化JSON数组，不实例化要报错                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DepartmentInfo objData = new DepartmentInfo(); //实例化JSON数组对象，用于添加JSON数组集合
                    objData.onelevel = dt.Rows[i]["onelevel"].ToString();
                    data.Add(objData); //添加JSON数组集合
                }
               string res = JsonConvert.SerializeObject(data); //转JSON数组，演示用，下面讲方法使用
                return Json(res);
            }
            else
                return null;
        }
        public ActionResult DeptQueryTwo(string onelevel)
        {
            string sqlstr = "select  twolevel,max(ID) ID From DepartmentInfo where  onelevel='"+ onelevel + "'group by twolevel order by ID";
            var dt = SQLHelper.GetTable(sqlstr);
            if (dt.Rows.Count > 0)
            {
                List<DepartmentInfo> data = new List<DepartmentInfo>(); //实例化JSON数组，不实例化要报错                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DepartmentInfo objData = new DepartmentInfo(); //实例化JSON数组对象，用于添加JSON数组集合
                    objData.twolevel = dt.Rows[i]["twolevel"].ToString();
                    data.Add(objData); //添加JSON数组集合
                }
                string res = JsonConvert.SerializeObject(data); //转JSON数组，演示用，下面讲方法使用
                return Json(res);
            }
            else
                return null;
        }
        public ActionResult DeptQueryThree(string twolevel)
        {
            string sqlstr = "select  threelevel,max(ID) ID From DepartmentInfo where  twolevel='"+ twolevel + "'group by threelevel order by ID";
            var dt = SQLHelper.GetTable(sqlstr);
            if (dt.Rows.Count > 0)
            {
                List<DepartmentInfo> data = new List<DepartmentInfo>(); //实例化JSON数组，不实例化要报错                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DepartmentInfo objData = new DepartmentInfo(); //实例化JSON数组对象，用于添加JSON数组集合
                    objData.threelevel = dt.Rows[i]["threelevel"].ToString();
                    data.Add(objData); //添加JSON数组集合
                }
                string res = JsonConvert.SerializeObject(data); //转JSON数组，演示用，下面讲方法使用
                return Json(res);
            }
            else
                return null;
        }
        public ActionResult ApplyQueryData(string deptonelevel, string depttwolevel, string deptthreelevel, string begindate, string enddate,bool dgfactory, bool jxfactory, bool dgchoose1, bool dgchoose2, bool dgchoose3, bool jxchoose1, bool jxchoose2)
        {
            string sqlstr = "";
            if (dgfactory)
            {
                if (dgchoose1)
                {
                    sqlstr = "select ROW_NUMBER() over(order by ID) as ID,userno,username,sex,convert(varchar(23),RecordDate,121) RecordDate From nCoVRecordInfoZoom where 1=1";
                }
                else if (dgchoose2)
                {
                    sqlstr = "select ROW_NUMBER() over(order by ID) as ID,userno,username,sex,convert(varchar(23),RecordDate,121) RecordDate From nCoVRecordInfo_DGNoReturnZoom where 1=1";
                }
                else if (dgchoose3)
                {
                    sqlstr = "select ROW_NUMBER() over(order by ID) as ID,userno,username,sex,convert(varchar(23),RecordDate,121) RecordDate From nCoVRecordInfo_DGNoReturn where 1=1";
                }
            }
            else if(jxfactory)
            {
                if (jxchoose1)
                {
                    sqlstr = "select ROW_NUMBER() over(order by ID) as ID,userno,username,sex,convert(varchar(23),RecordDate,121) RecordDate From nCoVRecordInfoZoom_JX where 1=1";
                }
                else if (jxchoose2)
                {
                    sqlstr = "select ROW_NUMBER() over(order by ID) as ID,userno,username,sex,convert(varchar(23),RecordDate,121) RecordDate From nCoVRecordInfo_JXNoReturn where 1=1";
                }
            }
            sqlstr += string.IsNullOrEmpty(deptonelevel) ? "" : " and deptonelevel='" + deptonelevel + "' ";
            sqlstr += string.IsNullOrEmpty(depttwolevel) ? "" : " and depttwolevel='" + depttwolevel + "' ";
            sqlstr += string.IsNullOrEmpty(deptthreelevel) ? "" : " and deptthreelevel='" + deptthreelevel + "' ";
            sqlstr += string.IsNullOrEmpty(begindate) ? "" : " and RecordDate>='" + begindate + "' ";
            sqlstr += string.IsNullOrEmpty(enddate) ? "" : " and RecordDate<dateadd(day,1,'" + enddate + "') ";
            sqlstr += " order by RecordDate";
            var dt = SQLHelper.GetTable(sqlstr);
            if (dt.Rows.Count > 0)
            {
                List<nCoVRecordInfo> data = new List<nCoVRecordInfo>(); //实例化JSON数组，不实例化要报错                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    nCoVRecordInfo objData = new nCoVRecordInfo(); //实例化JSON数组对象，用于添加JSON数组集合
                    objData.ID = dt.Rows[i]["ID"].ToString();
                    objData.userno = dt.Rows[i]["userno"].ToString();
                    objData.username = dt.Rows[i]["username"].ToString();
                    objData.sex = dt.Rows[i]["sex"].ToString();
                    objData.RecordDate = dt.Rows[i]["RecordDate"].ToString();
                    data.Add(objData); //添加JSON数组集合
                }
                string res = JsonConvert.SerializeObject(data); //转JSON数组，演示用，下面讲方法使用
                return Json(res);
            }
            else
                return Json("");
        }
        public ActionResult AddZoom(string username, string userno, string sex, string bornyear, string phone, string dept, string province, string city, string county, string q1, string q1room, string q2mntemperature, string q2antemperature, string q3symptom, string q3othersymptom, string q4, string q4place, string q4time, string q4relationship, string q5symptom, string q5name,string q6from,string q6name, string q7, string q7name, string q8, string q8name, string remark, string deptonelevel, string depttwolevel, string deptthreelevel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into nCoVRecordInfoZoom(username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city" +
                ",county,q1,q1room,q2mntemperature,q2antemperature,q3symptom,q3othersymptom,q4,q4place" +
                ",q4time,q4relationship,q5symptom,q5name,q6from,q6name,q7,q7name,q8,q8name,remark" +
                ",RecordDate)");
            strSql.Append(" VALUES(@username,@userno,@sex,@bornyear,@phone,@deptonelevel,@depttwolevel,@deptthreelevel,@province,@city" +
                ",@county,@q1,@q1room,@q2mntemperature,@q2antemperature,@q3symptom,@q3othersymptom,@q4,@q4place" +
                ",@q4time,@q4relationship,@q5symptom,@q5name,@q6from,@q6name,@q7,@q7name,@q8,@q8name,@remark" +
                ",getdate())");
            SqlParameter[] parameters = {
                new SqlParameter("@username", SqlDbType.VarChar,255),
                new SqlParameter("@userno", SqlDbType.VarChar,255),
                new SqlParameter("@sex", SqlDbType.VarChar,255),
                new SqlParameter("@bornyear", SqlDbType.VarChar,255),
                new SqlParameter("@phone", SqlDbType.VarChar,255),
                new SqlParameter("@deptonelevel", SqlDbType.VarChar,255),
                new SqlParameter("@depttwolevel", SqlDbType.VarChar,255),
                new SqlParameter("@deptthreelevel", SqlDbType.VarChar,255),
                new SqlParameter("@province", SqlDbType.VarChar,255),
                new SqlParameter("@city", SqlDbType.VarChar,255),
                new SqlParameter("@county", SqlDbType.VarChar,255),
                new SqlParameter("@q1", SqlDbType.VarChar,255),
                new SqlParameter("@q1room", SqlDbType.VarChar,255),
                new SqlParameter("@q2mntemperature", SqlDbType.VarChar,255),
                new SqlParameter("@q2antemperature", SqlDbType.VarChar,255),
                new SqlParameter("@q3symptom", SqlDbType.VarChar,255),
                new SqlParameter("@q3othersymptom", SqlDbType.VarChar,255),
                new SqlParameter("@q4", SqlDbType.VarChar,255),
                new SqlParameter("@q4place", SqlDbType.VarChar,255),
                new SqlParameter("@q4time", SqlDbType.VarChar,255),
                new SqlParameter("@q4relationship", SqlDbType.VarChar,255),
                new SqlParameter("@q5symptom", SqlDbType.VarChar,255),
                new SqlParameter("@q5name", SqlDbType.VarChar,255),
                new SqlParameter("@q6from", SqlDbType.VarChar,255),
                new SqlParameter("@q6name", SqlDbType.VarChar,255),
                new SqlParameter("@q7", SqlDbType.VarChar,255),
                new SqlParameter("@q7name", SqlDbType.VarChar,255),
                new SqlParameter("@q8", SqlDbType.VarChar,255),
                new SqlParameter("@q8name", SqlDbType.VarChar,255),
                new SqlParameter("@remark", SqlDbType.VarChar,255)
            };
            parameters[0].Value = username;
            parameters[1].Value = userno;
            parameters[2].Value = sex;
            parameters[3].Value = bornyear;
            parameters[4].Value = phone;
            parameters[5].Value = deptonelevel;
            parameters[6].Value = depttwolevel;
            parameters[7].Value = deptthreelevel;
            parameters[8].Value = province;
            parameters[9].Value = city;
            parameters[10].Value = county;
            parameters[11].Value = q1;
            parameters[12].Value = q1room;
            parameters[13].Value = q2mntemperature;
            parameters[14].Value = q2antemperature;
            parameters[15].Value = q3symptom;
            parameters[16].Value = q3othersymptom;
            parameters[17].Value = q4;
            parameters[18].Value = q4place;
            parameters[19].Value = q4time;
            parameters[20].Value = q4relationship;
            parameters[21].Value = q5symptom;
            parameters[22].Value = q5name;
            parameters[23].Value = q6from;
            parameters[24].Value = q6name;
            parameters[25].Value = q7;
            parameters[26].Value = q7name;
            parameters[27].Value = q8;
            parameters[28].Value = q8name;
            parameters[29].Value = remark;
            SQLHelper.ExcuteSQL(strSql.ToString(), parameters);
            return null;
        }

        public ActionResult DGNoReturnZoomAdd(string username, string userno, string sex, string bornyear, string phone, string deptonelevel, string depttwolevel, string deptthreelevel, string province, string city, string county, string q1, string q1city, string q2, string q3, string q4, string q5, string q6symptom, string q6othersymptom, string q7nowplace, string q8personnel, string q8otherpersonnel, string q9, string q10site, string q10othersite, string q11vehicle, string q11othervehicle, string q12, string q12mark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into nCoVRecordInfo_DGNoReturnZoom(username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county,q1,q1city,q2,q3,q4,q5,q6symptom,q6othersymptom,q7nowplace,q8personnel,q8otherpersonnel,q9,q10site,q10othersite,q11vehicle,q11othervehicle,q12,q12mark,RecordDate)");
            strSql.Append(" VALUES(@username,@userno,@sex,@bornyear,@phone,@deptonelevel,@depttwolevel,@deptthreelevel,@province,@city,@county,@q1,@q1city,@q2,@q3,@q4,@q5,@q6symptom,@q6othersymptom,@q7nowplace,@q8personnel,@q8otherpersonnel,@q9,@q10site,@q10othersite,@q11vehicle,@q11othervehicle,@q12,@q12mark,getdate())");
            SqlParameter[] parameters = {
                new SqlParameter("@username", SqlDbType.VarChar,255),
                new SqlParameter("@userno", SqlDbType.VarChar,255),
                new SqlParameter("@sex", SqlDbType.VarChar,255),
                new SqlParameter("@bornyear", SqlDbType.VarChar,255),
                new SqlParameter("@phone", SqlDbType.VarChar,255),
                new SqlParameter("@deptonelevel", SqlDbType.VarChar,255),
                new SqlParameter("@depttwolevel", SqlDbType.VarChar,255),
                new SqlParameter("@deptthreelevel", SqlDbType.VarChar,255),
                new SqlParameter("@province", SqlDbType.VarChar,255),
                new SqlParameter("@city", SqlDbType.VarChar,255),
                new SqlParameter("@county", SqlDbType.VarChar,255),
                new SqlParameter("@q1", SqlDbType.VarChar,255),
                new SqlParameter("@q1city", SqlDbType.VarChar,255),
                new SqlParameter("@q2", SqlDbType.VarChar,255),
                new SqlParameter("@q3", SqlDbType.VarChar,255),
                new SqlParameter("@q4", SqlDbType.VarChar,255),
                new SqlParameter("@q5", SqlDbType.VarChar,255),
                new SqlParameter("@q6symptom", SqlDbType.VarChar,255),
                new SqlParameter("@q6othersymptom", SqlDbType.VarChar,255),
                new SqlParameter("@q7nowplace", SqlDbType.VarChar,255),
                new SqlParameter("@q8personnel", SqlDbType.VarChar,255),
                new SqlParameter("@q8otherpersonnel", SqlDbType.VarChar,255),
                new SqlParameter("@q9", SqlDbType.VarChar,255),
                new SqlParameter("@q10site", SqlDbType.VarChar,255),
                new SqlParameter("@q10othersite", SqlDbType.VarChar,255),
                new SqlParameter("@q11vehicle", SqlDbType.VarChar,255),
                new SqlParameter("@q11othervehicle", SqlDbType.VarChar,255),
                new SqlParameter("@q12", SqlDbType.VarChar,255),
                new SqlParameter("@q12mark", SqlDbType.VarChar,255)
            };
            parameters[0].Value = username; 
            parameters[1].Value = userno; 
            parameters[2].Value = sex; 
            parameters[3].Value = bornyear; 
            parameters[4].Value = phone; 
            parameters[5].Value = deptonelevel; 
            parameters[6].Value = depttwolevel; 
            parameters[7].Value = deptthreelevel; 
            parameters[8].Value = province; 
            parameters[9].Value = city; 
            parameters[10].Value = county; 
            parameters[11].Value = q1;
            parameters[12].Value = q1;
            parameters[13].Value = q2; 
            parameters[14].Value = q3; 
            parameters[15].Value = q4; 
            parameters[16].Value = q5; 
            parameters[17].Value = q6symptom; 
            parameters[18].Value = q6othersymptom; 
            parameters[19].Value = q7nowplace; 
            parameters[20].Value = q8personnel; 
            parameters[21].Value = q8otherpersonnel; 
            parameters[22].Value = q9; 
            parameters[23].Value = q10site; 
            parameters[24].Value = q10othersite; 
            parameters[25].Value = q11vehicle; 
            parameters[26].Value = q11othervehicle; 
            parameters[27].Value = q12; 
            parameters[28].Value = q12mark;
            SQLHelper.ExcuteSQL(strSql.ToString(), parameters);
            return null;
        }
        public ActionResult DGNoReturnZoomQuery(string userno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county");
            strSql.Append(" From nCoVRecordInfo_DGNoReturnZoom");
            strSql.Append(" where ID=(select max(ID) From nCoVRecordInfo_DGNoReturnZoom where userno=@userno)");
            SqlParameter[] parameters = {
                    new SqlParameter("@userno", SqlDbType.VarChar,255),
            };
            parameters[0].Value = userno;
            DataTable dt = SQLHelper.GetTable(strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
                return Json(new { username = dt.Rows[0]["username"], userno = dt.Rows[0]["userno"], sex = dt.Rows[0]["sex"], bornyear = dt.Rows[0]["bornyear"], phone = dt.Rows[0]["phone"], deptonelevel = dt.Rows[0]["deptonelevel"], depttwolevel = dt.Rows[0]["depttwolevel"], deptthreelevel = dt.Rows[0]["deptthreelevel"], province = dt.Rows[0]["province"], city = dt.Rows[0]["city"], county = dt.Rows[0]["county"] }, "text/html", JsonRequestBehavior.AllowGet);
            else
                return null;
        }
        public ActionResult ZoomQuery(string userno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county");
            strSql.Append(" From nCoVRecordInfoZoom");
            strSql.Append(" where ID=(select max(ID) From nCoVRecordInfoZoom where userno=@userno)");
            SqlParameter[] parameters = {
                    new SqlParameter("@userno", SqlDbType.VarChar,255),
            };
            parameters[0].Value = userno;
            DataTable dt = SQLHelper.GetTable(strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
                return Json(new { username = dt.Rows[0]["username"], userno = dt.Rows[0]["userno"], sex = dt.Rows[0]["sex"], bornyear = dt.Rows[0]["bornyear"], phone = dt.Rows[0]["phone"], deptonelevel = dt.Rows[0]["deptonelevel"], depttwolevel = dt.Rows[0]["depttwolevel"], deptthreelevel = dt.Rows[0]["deptthreelevel"], province = dt.Rows[0]["province"], city = dt.Rows[0]["city"], county = dt.Rows[0]["county"] }, "text/html", JsonRequestBehavior.AllowGet);
            else
                return null;
        }
        public ActionResult DGNoReturnAdd(string username, string userno, string sex, string bornyear, string phone, string deptonelevel, string depttwolevel, string deptthreelevel, string province, string city, string county, string q1, string q1city, string q2, string q3, string q4, string q5, string q6symptom, string q6othersymptom, string q7, string q8nowplace, string q9reason, string q9otherreason, string q10returndate, string q11vehicle, string q11trainno, string q12, string q13, string q14,string q13mark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into nCoVRecordInfo_DGNoReturn(username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county,q1,q1city,q2,q3,q4,q5,q6symptom,q6othersymptom,q7,q8nowplace,q9reason,q9otherreason,q10returndate,q11vehicle,q11trainno,q12,q13,q14,q13mark,RecordDate)");
            strSql.Append(" VALUES(@username,@userno,@sex,@bornyear,@phone,@deptonelevel,@depttwolevel,@deptthreelevel,@province,@city,@county,@q1,@q1city,@q2,@q3,@q4,@q5,@q6symptom,@q6othersymptom,@q7,@q8nowplace,@q9reason,@q9otherreason,@q10returndate,@q11vehicle,@q11trainno,@q12,@q13,@q14,@q13mark,getdate())");
            SqlParameter[] parameters = {
                new SqlParameter("@username", SqlDbType.VarChar,255),
                new SqlParameter("@userno", SqlDbType.VarChar,255),
                new SqlParameter("@sex", SqlDbType.VarChar,255),
                new SqlParameter("@bornyear", SqlDbType.VarChar,255),
                new SqlParameter("@phone", SqlDbType.VarChar,255),
                new SqlParameter("@deptonelevel", SqlDbType.VarChar,255),
                new SqlParameter("@depttwolevel", SqlDbType.VarChar,255),
                new SqlParameter("@deptthreelevel", SqlDbType.VarChar,255),
                new SqlParameter("@province", SqlDbType.VarChar,255),
                new SqlParameter("@city", SqlDbType.VarChar,255),
                new SqlParameter("@county", SqlDbType.VarChar,255),
                new SqlParameter("@q1", SqlDbType.VarChar,255),
                new SqlParameter("@q1city", SqlDbType.VarChar,255),
                new SqlParameter("@q2", SqlDbType.VarChar,255),
                new SqlParameter("@q3", SqlDbType.VarChar,255),
                new SqlParameter("@q4", SqlDbType.VarChar,255),
                new SqlParameter("@q5", SqlDbType.VarChar,255),
                new SqlParameter("@q6symptom", SqlDbType.VarChar,255),
                new SqlParameter("@q6othersymptom", SqlDbType.VarChar,255),
                new SqlParameter("@q7", SqlDbType.VarChar,255),
                new SqlParameter("@q8nowplace", SqlDbType.VarChar,255),
                new SqlParameter("@q9reason", SqlDbType.VarChar,255),
                new SqlParameter("@q9otherreason", SqlDbType.VarChar,255),
                new SqlParameter("@q10returndate", SqlDbType.VarChar,255),
                new SqlParameter("@q11vehicle", SqlDbType.VarChar,255),
                new SqlParameter("@q11trainno", SqlDbType.VarChar,255),
                new SqlParameter("@q12", SqlDbType.VarChar,255),
                new SqlParameter("@q13", SqlDbType.VarChar,255),
                new SqlParameter("@q14", SqlDbType.VarChar,255),
                new SqlParameter("@q13mark", SqlDbType.VarChar,255)
            };
            parameters[0].Value = username;
            parameters[1].Value = userno;
            parameters[2].Value = sex;
            parameters[3].Value = bornyear;
            parameters[4].Value = phone;
            parameters[5].Value = deptonelevel;
            parameters[6].Value = depttwolevel;
            parameters[7].Value = deptthreelevel;
            parameters[8].Value = province;
            parameters[9].Value = city;
            parameters[10].Value = county;
            parameters[11].Value = q1; 
            parameters[12].Value = q1city; 
            parameters[13].Value = q2; 
            parameters[14].Value = q3; 
            parameters[15].Value = q4; 
            parameters[16].Value = q5; 
            parameters[17].Value = q6symptom; 
            parameters[18].Value = q6othersymptom; 
            parameters[19].Value = q7; 
            parameters[20].Value = q8nowplace; 
            parameters[21].Value = q9reason; 
            parameters[22].Value = q9otherreason; 
            parameters[23].Value = q10returndate; 
            parameters[24].Value = q11vehicle; 
            parameters[25].Value = q11trainno; 
            parameters[26].Value = q12; 
            parameters[27].Value = q13; 
            parameters[28].Value = q14;
            parameters[29].Value = q13mark;
            SQLHelper.ExcuteSQL(strSql.ToString(), parameters);
            return null;
        }
        public ActionResult DGNoReturnQuery(string userno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county");
            strSql.Append(" From nCoVRecordInfo_DGNoReturn");
            strSql.Append(" where ID=(select max(ID) From nCoVRecordInfo_DGNoReturn where userno=@userno)");
            SqlParameter[] parameters = {
                    new SqlParameter("@userno", SqlDbType.VarChar,255),
            };
            parameters[0].Value = userno;
            DataTable dt = SQLHelper.GetTable(strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
                return Json(new { username = dt.Rows[0]["username"], userno = dt.Rows[0]["userno"], sex = dt.Rows[0]["sex"], bornyear = dt.Rows[0]["bornyear"], phone = dt.Rows[0]["phone"], deptonelevel = dt.Rows[0]["deptonelevel"], depttwolevel = dt.Rows[0]["depttwolevel"], deptthreelevel = dt.Rows[0]["deptthreelevel"], province = dt.Rows[0]["province"], city = dt.Rows[0]["city"], county = dt.Rows[0]["county"] }, "text/html", JsonRequestBehavior.AllowGet);
            else
                return null;
        }
        public ActionResult AddZoomJX(string username, string userno, string sex, string bornyear, string phone, string dept, string province, string city, string county, string q1, string q1room, string q2mntemperature, string q2antemperature, string q3symptom, string q3othersymptom, string q4, string q4place, string q4time, string q4relationship, string q5symptom, string q5name, string q6from, string q6name, string q7, string q7name, string q8, string q8name, string remark, string deptonelevel, string depttwolevel, string deptthreelevel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into nCoVRecordInfoZoom_JX(username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city" +
                ",county,q1,q1room,q2mntemperature,q2antemperature,q3symptom,q3othersymptom,q4,q4place" +
                ",q4time,q4relationship,q5symptom,q5name,q6from,q6name,q7,q7name,q8,q8name,remark" +
                ",RecordDate)");
            strSql.Append(" VALUES(@username,@userno,@sex,@bornyear,@phone,@deptonelevel,@depttwolevel,@deptthreelevel,@province,@city" +
                ",@county,@q1,@q1room,@q2mntemperature,@q2antemperature,@q3symptom,@q3othersymptom,@q4,@q4place" +
                ",@q4time,@q4relationship,@q5symptom,@q5name,@q6from,@q6name,@q7,@q7name,@q8,@q8name,@remark" +
                ",getdate())");
            SqlParameter[] parameters = {
                new SqlParameter("@username", SqlDbType.VarChar,255),
                new SqlParameter("@userno", SqlDbType.VarChar,255),
                new SqlParameter("@sex", SqlDbType.VarChar,255),
                new SqlParameter("@bornyear", SqlDbType.VarChar,255),
                new SqlParameter("@phone", SqlDbType.VarChar,255),
                new SqlParameter("@deptonelevel", SqlDbType.VarChar,255),
                new SqlParameter("@depttwolevel", SqlDbType.VarChar,255),
                new SqlParameter("@deptthreelevel", SqlDbType.VarChar,255),
                new SqlParameter("@province", SqlDbType.VarChar,255),
                new SqlParameter("@city", SqlDbType.VarChar,255),
                new SqlParameter("@county", SqlDbType.VarChar,255),
                new SqlParameter("@q1", SqlDbType.VarChar,255),
                new SqlParameter("@q1room", SqlDbType.VarChar,255),
                new SqlParameter("@q2mntemperature", SqlDbType.VarChar,255),
                new SqlParameter("@q2antemperature", SqlDbType.VarChar,255),
                new SqlParameter("@q3symptom", SqlDbType.VarChar,255),
                new SqlParameter("@q3othersymptom", SqlDbType.VarChar,255),
                new SqlParameter("@q4", SqlDbType.VarChar,255),
                new SqlParameter("@q4place", SqlDbType.VarChar,255),
                new SqlParameter("@q4time", SqlDbType.VarChar,255),
                new SqlParameter("@q4relationship", SqlDbType.VarChar,255),
                new SqlParameter("@q5symptom", SqlDbType.VarChar,255),
                new SqlParameter("@q5name", SqlDbType.VarChar,255),
                new SqlParameter("@q6from", SqlDbType.VarChar,255),
                new SqlParameter("@q6name", SqlDbType.VarChar,255),
                new SqlParameter("@q7", SqlDbType.VarChar,255),
                new SqlParameter("@q7name", SqlDbType.VarChar,255),
                new SqlParameter("@q8", SqlDbType.VarChar,255),
                new SqlParameter("@q8name", SqlDbType.VarChar,255),
                new SqlParameter("@remark", SqlDbType.VarChar,255)
            };
            parameters[0].Value = username;
            parameters[1].Value = userno;
            parameters[2].Value = sex;
            parameters[3].Value = bornyear;
            parameters[4].Value = phone;
            parameters[5].Value = deptonelevel;
            parameters[6].Value = depttwolevel;
            parameters[7].Value = deptthreelevel;
            parameters[8].Value = province;
            parameters[9].Value = city;
            parameters[10].Value = county;
            parameters[11].Value = q1;
            parameters[12].Value = q1room;
            parameters[13].Value = q2mntemperature;
            parameters[14].Value = q2antemperature;
            parameters[15].Value = q3symptom;
            parameters[16].Value = q3othersymptom;
            parameters[17].Value = q4;
            parameters[18].Value = q4place;
            parameters[19].Value = q4time;
            parameters[20].Value = q4relationship;
            parameters[21].Value = q5symptom;
            parameters[22].Value = q5name;
            parameters[23].Value = q6from;
            parameters[24].Value = q6name;
            parameters[25].Value = q7;
            parameters[26].Value = q7name;
            parameters[27].Value = q8;
            parameters[28].Value = q8name;
            parameters[29].Value = remark;
            SQLHelper.ExcuteSQL(strSql.ToString(), parameters);
            return null;
        }
        public ActionResult ZoomQueryJX(string userno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county");
            strSql.Append(" From nCoVRecordInfoZoom_JX");
            strSql.Append(" where ID=(select max(ID) From nCoVRecordInfoZoom_JX where userno=@userno)");
            SqlParameter[] parameters = {
                    new SqlParameter("@userno", SqlDbType.VarChar,255),
            };
            parameters[0].Value = userno;
            DataTable dt = SQLHelper.GetTable(strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
                return Json(new { username = dt.Rows[0]["username"], userno = dt.Rows[0]["userno"], sex = dt.Rows[0]["sex"], bornyear = dt.Rows[0]["bornyear"], phone = dt.Rows[0]["phone"], deptonelevel = dt.Rows[0]["deptonelevel"], depttwolevel = dt.Rows[0]["depttwolevel"], deptthreelevel = dt.Rows[0]["deptthreelevel"], province = dt.Rows[0]["province"], city = dt.Rows[0]["city"], county = dt.Rows[0]["county"] }, "text/html", JsonRequestBehavior.AllowGet);
            else
                return null;
        }
        public ActionResult JXNoReturnAdd(string username, string userno, string sex, string bornyear, string phone, string deptonelevel, string depttwolevel, string deptthreelevel, string province, string city, string county, string q1, string q1city, string q2, string q3, string q4, string q5, string q6symptom, string q6othersymptom, string q7, string q8nowplace, string q9reason, string q9otherreason, string q10returndate, string q11, string q12,string q11mark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into nCoVRecordInfo_JXNoReturn(username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county,q1,q1city,q2,q3,q4,q5,q6symptom,q6othersymptom,q7,q8nowplace,q9reason,q9otherreason,q10returndate,q11,q12,q11mark,RecordDate)");
            strSql.Append(" VALUES(@username,@userno,@sex,@bornyear,@phone,@deptonelevel,@depttwolevel,@deptthreelevel,@province,@city,@county,@q1,@q1city,@q2,@q3,@q4,@q5,@q6symptom,@q6othersymptom,@q7,@q8nowplace,@q9reason,@q9otherreason,@q10returndate,@q11,@q12,@q11mark,getdate())");
            SqlParameter[] parameters = {
                new SqlParameter("@username", SqlDbType.VarChar,255),
                new SqlParameter("@userno", SqlDbType.VarChar,255),
                new SqlParameter("@sex", SqlDbType.VarChar,255),
                new SqlParameter("@bornyear", SqlDbType.VarChar,255),
                new SqlParameter("@phone", SqlDbType.VarChar,255),
                new SqlParameter("@deptonelevel", SqlDbType.VarChar,255),
                new SqlParameter("@depttwolevel", SqlDbType.VarChar,255),
                new SqlParameter("@deptthreelevel", SqlDbType.VarChar,255),
                new SqlParameter("@province", SqlDbType.VarChar,255),
                new SqlParameter("@city", SqlDbType.VarChar,255),
                new SqlParameter("@county", SqlDbType.VarChar,255),
                new SqlParameter("@q1", SqlDbType.VarChar,255),
                new SqlParameter("@q1city", SqlDbType.VarChar,255),
                new SqlParameter("@q2", SqlDbType.VarChar,255),
                new SqlParameter("@q3", SqlDbType.VarChar,255),
                new SqlParameter("@q4", SqlDbType.VarChar,255),
                new SqlParameter("@q5", SqlDbType.VarChar,255),
                new SqlParameter("@q6symptom", SqlDbType.VarChar,255),
                new SqlParameter("@q6othersymptom", SqlDbType.VarChar,255),
                new SqlParameter("@q7", SqlDbType.VarChar,255),
                new SqlParameter("@q8nowplace", SqlDbType.VarChar,255),
                new SqlParameter("@q9reason", SqlDbType.VarChar,255),
                new SqlParameter("@q9otherreason", SqlDbType.VarChar,255),
                new SqlParameter("@q10returndate", SqlDbType.VarChar,255),
                new SqlParameter("@q11", SqlDbType.VarChar,255),
                new SqlParameter("@q12", SqlDbType.VarChar,255),
                new SqlParameter("@q11mark", SqlDbType.VarChar,255)

            };
            parameters[0].Value = username;
            parameters[1].Value = userno;
            parameters[2].Value = sex;
            parameters[3].Value = bornyear;
            parameters[4].Value = phone;
            parameters[5].Value = deptonelevel;
            parameters[6].Value = depttwolevel;
            parameters[7].Value = deptthreelevel;
            parameters[8].Value = province;
            parameters[9].Value = city;
            parameters[10].Value = county;
            parameters[11].Value = q1;
            parameters[12].Value = q1city;
            parameters[13].Value = q2;
            parameters[14].Value = q3;
            parameters[15].Value = q4;
            parameters[16].Value = q5;
            parameters[17].Value = q6symptom;
            parameters[18].Value = q6othersymptom;
            parameters[19].Value = q7;
            parameters[20].Value = q8nowplace;
            parameters[21].Value = q9reason;
            parameters[22].Value = q9otherreason;
            parameters[23].Value = q10returndate;
            parameters[24].Value = q11;
            parameters[25].Value = q12;
            parameters[26].Value = q11mark;
            SQLHelper.ExcuteSQL(strSql.ToString(), parameters);
            return null;
        }
        public ActionResult JXNoReturnQuery(string userno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select username,userno,sex,bornyear,phone,deptonelevel,depttwolevel,deptthreelevel,province,city,county");
            strSql.Append(" From nCoVRecordInfo_JXNoReturn");
            strSql.Append(" where ID=(select max(ID) From nCoVRecordInfo_JXNoReturn where userno=@userno)");
            SqlParameter[] parameters = {
                    new SqlParameter("@userno", SqlDbType.VarChar,255),
            };
            parameters[0].Value = userno;
            DataTable dt = SQLHelper.GetTable(strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
                return Json(new { username = dt.Rows[0]["username"], userno = dt.Rows[0]["userno"], sex = dt.Rows[0]["sex"], bornyear = dt.Rows[0]["bornyear"], phone = dt.Rows[0]["phone"], deptonelevel = dt.Rows[0]["deptonelevel"], depttwolevel = dt.Rows[0]["depttwolevel"], deptthreelevel = dt.Rows[0]["deptthreelevel"], province = dt.Rows[0]["province"], city = dt.Rows[0]["city"], county = dt.Rows[0]["county"] }, "text/html", JsonRequestBehavior.AllowGet);
            else
                return null;
        }
    }
}