using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.Import;
using Campus.DocumentValidator;
using FISCA.UDT;
using K12.Data;

namespace Ischool.Booking.Room
{
    class ImportMeetingRoomData :ImportWizard
    {
        //設定檔
        private ImportOption mOption;

        MeetingRoomImportBot importBot = new MeetingRoomImportBot();

        Dictionary<string, ImputLog> Log_Dic = new Dictionary<string, ImputLog>();

        public override string GetValidateRule()
        {
            return Properties.Resources.MeetingRoomValidate;
        }
        /// <summary>
        /// 匯入場地與設備清單。 
        /// 0. 解析資料
        /// 1.處理場地 
        /// 2. 處理各場地的設備
        /// 設計邏輯
        /// </summary>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public override string Import(List<Campus.DocumentValidator.IRowStream> Rows)
        {
            if (mOption.Action == ImportAction.InsertOrUpdate)
            {
                List<UDT.MeetingRoom> listRoomInsert = new List<UDT.MeetingRoom>();
                List<UDT.MeetingRoom> listRoomUpdate = new List<UDT.MeetingRoom>();
                Dictionary<string, List<UDT.MeetingRoomEquipment>> dicEquipmentsInRoom = new Dictionary<string, List<UDT.MeetingRoomEquipment>>();   // <場地key, List<MeetingRoomEquipment> >

                // 0.解析資料, (會把資料整理成 listRoomInsert, listRoomUpdate, dicEquipmentsInRoom)
                parseData(Rows, listRoomInsert, listRoomUpdate, dicEquipmentsInRoom);

                //1. 處理場地
                saveRooms(listRoomInsert, listRoomUpdate);

                //2. 處理設備
                saveEquipments(dicEquipmentsInRoom);
            }

            return "";
        }


        /// <summary>
        /// 此函數的目的是將 Rows 中的每一筆場地設備資料，整理成:
        /// 1. 要新增的場地 (listRoomInsert )
        /// 2. 要修改的場地 (listRoomUpdate )
        /// 3. 每個場地的設備清單 (dicEquipment )
        /// </summary>
        /// <param name="Rows"></param>
        /// <param name="listRoomInsert"></param>
        /// <param name="listRoomUpdate"></param>
        /// <param name="dicEquipmentInRoom"></param>
        private void parseData(List<Campus.DocumentValidator.IRowStream> Rows,
                                List<UDT.MeetingRoom> listRoomInsert,
                                List<UDT.MeetingRoom> listRoomUpdate,
                                Dictionary<string, List<UDT.MeetingRoomEquipment>> dicEquipmentInRoom )
        {
            // 對於每一筆場地的設備資料
            foreach (IRowStream Row in Rows)
            {
                //a. 確認場地是新增或修改
                string building = Row.GetValue("所屬大樓名稱");
                string roomName = Row.GetValue("會議室名稱");
                string equipName = Row.GetValue("設備名稱");
                string keyRoom = string.Format("{0}_{1}", roomName, building); // 會議室key
                if (importBot.GetMeetingRoomDic().ContainsKey(keyRoom)) //更新
                {
                    UDT.MeetingRoom room = importBot.GetMeetingRoomDic()[keyRoom];

                    // ????
                    if (!Log_Dic.ContainsKey(room.UID))
                    {
                        ImputLog i_n = new ImputLog();
                        i_n.lo_MeetingRoom = room.CopyExtension();
                        Log_Dic.Add(room.UID, i_n);
                    }

                    importBot.FillMeetingRoomData(Row, room);
                    listRoomUpdate.Add(room);
                }
                else
                {
                    //新增
                    UDT.MeetingRoom room = new UDT.MeetingRoom();
                    room.Name = roomName;
                    room.Building = building;

                    importBot.FillMeetingRoomData(Row, room);
                    listRoomInsert.Add(room);
                }

                //b. 確認設備所屬場地
                //string keyEquip = string.Format("{0}_{1}", keyRoom, equipName); // 設備key
                if (!dicEquipmentInRoom.ContainsKey(keyRoom))
                {
                    dicEquipmentInRoom.Add(keyRoom, new List<UDT.MeetingRoomEquipment>());
                }
                UDT.MeetingRoomEquipment equip = new UDT.MeetingRoomEquipment();
                importBot.FillEquipmentData(Row, equip);
                dicEquipmentInRoom[keyRoom].Add(equip);

            }
        }

        private void saveRooms(List<UDT.MeetingRoom> listRoomInsert,
                                List<UDT.MeetingRoom> listRoomUpdate)
        {
            AccessHelper access = new AccessHelper();

            if (listRoomInsert.Count > 0)
            {
                try
                {
                    StringBuilder mstrLog1 = new StringBuilder();
                    mstrLog1.AppendLine("新增匯入會議室：");
                    foreach (UDT.MeetingRoom each in listRoomInsert)
                    {
                        mstrLog1.AppendLine(importBot.GetLogString(each));
                    }
                    access.InsertValues(listRoomInsert);
                    FISCA.LogAgent.ApplicationLog.Log("會議室", "新增匯入", mstrLog1.ToString());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }

            }

            if (listRoomUpdate.Count > 0)
            {
                try
                {
                    StringBuilder mstrLog2 = new StringBuilder();
                    mstrLog2.AppendLine("更新匯入會議室：");
                    foreach (UDT.MeetingRoom each in listRoomUpdate)
                    {
                        if (Log_Dic.ContainsKey(each.UID))
                        {
                            Log_Dic[each.UID].New_MeetingRoom = each.CopyExtension();
                            mstrLog2.AppendLine(importBot.SetLog(Log_Dic[each.UID]));
                        }

                    }
                    access.UpdateValues(listRoomUpdate);
                    FISCA.LogAgent.ApplicationLog.Log(Actor.Account, "更新匯入", mstrLog2.ToString());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }

            }
        }

        /// <summary>
        /// 儲存設備。
        /// 邏輯為:
        /// 1.取得場地清單 整理成DIC場地key,場地ID
        /// 2.對於dicEquipmentInRoom 中的每個key 找出對應的場地ID
        /// 3.刪除該場地設備資料
        /// 4.新增該場地設備資料
        /// </summary>
        /// <param name="dicEquipmentsInRoom"></param>
        private void saveEquipments(Dictionary<string, List<UDT.MeetingRoomEquipment>> dicEquipmentsInRoom)
        {
            //1.取得場地清單 整理成DIC場地key,場地ID
            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoom> listRoom = access.Select<UDT.MeetingRoom>();
            Dictionary<string, string> dicRoom = new Dictionary<string, string>();
            foreach (UDT.MeetingRoom room in listRoom)
            {
                string key = string.Format("{0}_{1}",room.Name,room.Building);
                dicRoom.Add(key,room.UID);
            }

            //2.對於dicEquipmentInRoom 中的每個key 
            List<UDT.MeetingRoomEquipment> listTargetEquipData = new List<UDT.MeetingRoomEquipment>();
            List<string> targetRoomIDs = new List<string>();
            foreach (string key in dicEquipmentsInRoom.Keys)
            {
                //找出對應的場地ID
                if (dicRoom.ContainsKey(key))
                {
                    string roomID = dicRoom[key];
                    foreach (UDT.MeetingRoomEquipment equip in dicEquipmentsInRoom[key])
                    {
                        equip.RefMeetingroomID = int.Parse(roomID);
                        listTargetEquipData.Add(equip);
                    }
                    targetRoomIDs.Add(roomID);
                }
                
            }
            //3.刪除該場地設備資料
            string ids = string.Join(",",targetRoomIDs);
            string sql = string.Format(@"
DELETE
FROM
    $ischool.booking.meetingroom_equipment
WHERE
    ref_meetingroom_id IN( {0} )
                ", ids);
            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);

            //4.新增該場地設備資料
            access.InsertValues(listTargetEquipData);
            
        }

        public override ImportAction GetSupportActions()
        {
            //新增或更新
            return ImportAction.InsertOrUpdate;
        }

        public override void Prepare(ImportOption Option)
        {
            mOption = Option;
        }
    }
}
