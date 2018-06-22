using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;
using FISCA.DSAUtil;
using K12.Data;
using System.Xml;
using FISCA.UDT;

namespace Ischool.Booking.Room
{
    class MeetingRoomImportBot
    {
        private Dictionary<string, UDT.MeetingRoom> MeetingRoomDic { get; set; }

        public Dictionary<string, UDT.MeetingRoomUnit> UnitNameDic { get; set; }

        private Dictionary<string, UDT.MeetingRoomUnit> UnitIDDic { get; set; }

        public Dictionary<string, TeacherRecord> TeacherAccountDic { get; set; }

        public MeetingRoomImportBot()
        {
            getMeetingRooms();
            getUnits();
        }

        private void getMeetingRooms()
        {
            this.MeetingRoomDic = new Dictionary<string, UDT.MeetingRoom>();

            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoom> MeetingRoomList = access.Select<UDT.MeetingRoom>();
            foreach (UDT.MeetingRoom each in MeetingRoomList)
            {
                string roomKey = string.Format("{0}_{1}", each.Name , each.Building);

                if (!this.MeetingRoomDic.ContainsKey(roomKey))
                {
                    this.MeetingRoomDic.Add(roomKey, each);
                }
            }
        }

        private void getUnits()
        {
            this.UnitIDDic = new Dictionary<string, UDT.MeetingRoomUnit>();
            this.UnitNameDic = new Dictionary<string, UDT.MeetingRoomUnit>();

            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoomUnit> UnitList = access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit each in UnitList)
            {
                if (!this.UnitNameDic.ContainsKey(each.Name))
                {
                    this.UnitNameDic.Add(each.Name, each);
                }

                if (!this.UnitIDDic.ContainsKey(each.UID))
                {
                    this.UnitIDDic.Add(each.UID, each);
                }
            }
        }


        public string GetLogString(UDT.MeetingRoom each)
        {
            StringBuilder log = new StringBuilder();
            try
            {
                
                log.AppendLine(string.Format("場地名稱「{0}」所屬大樓「{1}」", each.Name, each.Building));

                if (!string.IsNullOrEmpty(each.Picture))
                {
                    log.AppendLine(string.Format("照片URL「{0}」", each.Picture));
                }

                if (!string.IsNullOrEmpty("" + each.Capacity))
                {
                    log.AppendLine(string.Format("容納人數「{0}」", each.Capacity));
                }

                if (!string.IsNullOrEmpty(each.Status))
                {
                    log.AppendLine(string.Format("會議室目前狀態「{0}」", each.Status));
                }

                if (!string.IsNullOrEmpty("" + each.RefUnitID))
                {
                    if (UnitIDDic.ContainsKey("" + each.RefUnitID))
                    {
                        log.AppendLine(string.Format("管理單位名稱「{0}」", UnitIDDic["" + each.RefUnitID]));
                    }
                }

                if (!string.IsNullOrEmpty("" + each.IsSpecial))
                {
                    log.AppendLine(string.Format("是否為特殊場地「{0}」", each.IsSpecial));
                }

                if (!string.IsNullOrEmpty(each.CreateTime.ToShortDateString()))
                {
                    log.AppendLine(string.Format("建立日期「{0}」", each.CreateTime.ToShortDateString()));
                }

                // 建立者帳號
                if (!string.IsNullOrEmpty(each.CreatedBy))
                {
                    //if (TeacherAccountDic.ContainsKey(each.CreatedBy))
                    //{
                    //    log.AppendLine(string.Format("建立者「{0}」", GetTeacherName(TeacherAccountDic[each.CreatedBy])));
                    //}
                    log.AppendLine(string.Format("建立者「{0}」", Actor.Account));
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            

            return log.ToString();
        }

        /// <summary>
        /// 把 Row 中的場地資料填入 MeetingRoom 物件。
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="room"></param>
        public void FillMeetingRoomData(IRowStream Row, UDT.MeetingRoom room)
        {
            room.Name = Row.GetValue("會議室名稱");
            room.Picture = Row.GetValue("照片");
            room.Building = Row.GetValue("所屬大樓名稱");

            int x = 0;
            if (int.TryParse("" + Row.GetValue("容納人數"), out x))
            {
                room.Capacity = x;
            }

            room.Status = Row.GetValue("會議室目前狀態");

            room.RefUnitID = int.Parse(CheckUnitName("" + Row.GetValue("管理單位名稱")));
            room.IsSpecial = Row.GetValue("是否為特殊場地") == "是" ? true : false;
            room.CreateTime = DateTime.Now;
            room.CreatedBy = Actor.Account;
        }

        public void FillEquipmentData(IRowStream Row, UDT.MeetingRoomEquipment equip)
        {
            equip.Name = Row.GetValue("設備名稱");
            equip.Status = Row.GetValue("設備狀態");
            equip.Count = Row.GetValue("設備數量") == "" ? 0 :int.Parse(Row.GetValue("設備數量"));
        }


            /// <summary>
            /// 檢查管理單位名稱是否存在，回傳管理單位UID
            /// </summary>
            public string CheckUnitName(string name)
        {
            if (UnitNameDic.ContainsKey(name))
            {
                return UnitNameDic[name].UID;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 取得XML結構之科別清單狀態
        /// </summary>
        public string GetDeptXML(string Dept)
        {
            if (!string.IsNullOrEmpty(Dept))
            {
                string[] DeptList = Dept.Split('/');
                DSXmlHelper dsXml = new DSXmlHelper("Department");
                foreach (string each in DeptList)
                {
                    dsXml.AddElement("Dept");
                    dsXml.AddText("Dept", each);
                }
                return dsXml.BaseElement.OuterXml;
            }
            else
            {
                DSXmlHelper dsXml = new DSXmlHelper("Department");
                return dsXml.BaseElement.OuterXml;
            }
        }

        public string GetDeptName(string xml)
        {
            List<string> list = new List<string>();
            DSXmlHelper dsXml = new DSXmlHelper();
            dsXml.Load(xml);
            foreach (XmlElement each in dsXml.BaseElement.SelectNodes("Dept"))
            {
                list.Add(each.InnerText);
            }

            return string.Join("/", list);

        }

        /// <summary>
        /// 確認建立者帳號是否存在
        /// </summary>
        public string CheckTeacherAccount(string account)
        {
            if (TeacherAccountDic.ContainsKey(account))
            {
                return TeacherAccountDic[account].ID;
            }
            else
            {
                return "";
            }
        }

        //public Dictionary<string, UDT.MeetingRoomUnit> GetUnitDic()
        //{
        //    if (this.UnitIDDic == null)
        //    {
        //        this.UnitIDDic = new Dictionary<string, UDT.MeetingRoomUnit>();
        //        this.UnitNameDic = new Dictionary<string, UDT.MeetingRoomUnit>();
                
        //        AccessHelper access = new AccessHelper();
        //        List<UDT.MeetingRoomUnit> UnitList = access.Select<UDT.MeetingRoomUnit>();
        //        foreach (UDT.MeetingRoomUnit each in UnitList)
        //        {
        //            if (!this.UnitNameDic.ContainsKey(each.Name))
        //            {
        //                this.UnitNameDic.Add(each.Name, each);
        //            }

        //            if (!this.UnitIDDic.ContainsKey(each.UID))
        //            {
        //                this.UnitIDDic.Add(each.UID, each);
        //            }
        //        }
        //        return dic;
        //    }
        //    else
        //    {
        //        return this.UnitIDDic;
        //    }
            
        //}

        /// <summary>
        /// 傳入老師Record,回傳包含老師暱稱的名字
        /// </summary>
        public string GetTeacherName(TeacherRecord tr)
        {
            if (string.IsNullOrEmpty(tr.Nickname))
            {
                return tr.Name;
            }
            else
            {
                return tr.Name + "(" + tr.Nickname + ")";
            }
        }

        /// <summary>
        /// 取得場地清單 school + semester + name:Record
        /// </summary>
        public Dictionary<string, UDT.MeetingRoom> GetMeetingRoomDic()
        {
            return this.MeetingRoomDic;
        }

        public string SetLog(ImputLog log)
        {
            //檢查與確認資料是否被修改
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("會議室名稱「{0}」 所屬大樓「{1}」", log.New_MeetingRoom.Name,log.New_MeetingRoom.Building));
            if (log.lo_MeetingRoom.Name != log.New_MeetingRoom.Name)
                sb.AppendLine(ByOne("會議室名稱", log.lo_MeetingRoom.Name, log.New_MeetingRoom.Name));

            if (log.lo_MeetingRoom.Picture != log.New_MeetingRoom.Picture)
                sb.AppendLine(ByOne("照片", log.lo_MeetingRoom.Picture, log.New_MeetingRoom.Picture));

            if (log.lo_MeetingRoom.Building != log.New_MeetingRoom.Building)
                sb.AppendLine(ByOne("所屬大樓名稱", log.lo_MeetingRoom.Building, log.New_MeetingRoom.Building));

            if (log.lo_MeetingRoom.Capacity != log.New_MeetingRoom.Capacity)
                sb.AppendLine(ByOne("容納人數","" + log.lo_MeetingRoom.Capacity,"" + log.New_MeetingRoom.Capacity));

            if (log.lo_MeetingRoom.Status != log.New_MeetingRoom.Status)
                sb.AppendLine(ByOne("會議室目前狀態", log.lo_MeetingRoom.Status, log.New_MeetingRoom.Status));

            if (log.lo_MeetingRoom.RefUnitID != log.New_MeetingRoom.RefUnitID)
                sb.AppendLine(ByOne("管理單位編號", ""+ log.lo_MeetingRoom.RefUnitID, "" + log.New_MeetingRoom.RefUnitID));

            if (log.lo_MeetingRoom.IsSpecial != log.New_MeetingRoom.IsSpecial)
                sb.AppendLine(ByOne("是否為特殊場地", "" + log.lo_MeetingRoom.IsSpecial, "" + log.New_MeetingRoom.IsSpecial));

            if (log.lo_MeetingRoom.CreateTime != log.New_MeetingRoom.CreateTime)
                sb.AppendLine(ByOne("建立日期", log.lo_MeetingRoom.CreateTime.ToShortDateString(), log.New_MeetingRoom.CreateTime.ToShortDateString()));

            if (log.lo_MeetingRoom.CreatedBy != log.New_MeetingRoom.CreatedBy)
                sb.AppendLine(ByOne("建立者帳號", GetDeptName(log.lo_MeetingRoom.CreatedBy), GetDeptName(log.New_MeetingRoom.CreatedBy)));

            sb.AppendLine("");
            return sb.ToString();

        }

        public string ByInet(int? a)
        {
            if (a.HasValue)
                return a.Value.ToString();
            else
                return "";
        }

        public string ByOne(string a, string b, string c)
        {
            return string.Format("「{0}」由「{1}」修改為「{2}」", a, b, c);
        }

    }
}
