using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevComponents.DotNetBar.Controls;
using System.Windows.Forms;
using K12.Data;

namespace Ischool.Booking.Room.DAO
{
    class MeetingRoom
    {
        public static void InsertMeetingRoom(string roomName,string building,string capacity,string unitID,string isSpecial,string picURL,string status,DataGridViewX dgv)
        {
            #region 資料整理
            string roomData = string.Format(@"
SELECT
    '{0}'::TEXT AS name
    ,'{1}'::TEXT AS building
    ,{2} ::INTEGER AS capacity
    ,{3} ::BIGINT AS ref_unit_id 
    ,'{4}'::BOOLEAN AS is_special
    ,'{5}'::TIMESTAMP AS create_time
    ,'{6}'::TEXT AS picture
    ,'{7}'::TEXT AS created_by
    ,'{8}'::TEXT AS status
                ", roomName, building, capacity, unitID, isSpecial, DateTime.Now.ToString("yyyy/MM/dd"), picURL, Actor.Account, status);


            List<string> equipmentDataList = new List<string>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string equipmentData = string.Format(@"
SELECT
    '{0}'::TEXT AS name
    ,{1} ::INTEGER AS count
    ,'{2}' ::TEXT AS status
                    ", row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value);

                    equipmentDataList.Add(equipmentData);
                }
            }
            #endregion

            #region SQL
            string sql = "";
            string equipDataRow = string.Join("UNION ALL", equipmentDataList);

            if (!string.IsNullOrEmpty(equipDataRow))
            {
                sql = string.Format(@"
WITH meetingroom_data AS(
    {0}
) ,equipment_data AS(
    {1}
) ,insert_meetingroom AS(
    INSERT INTO $ischool.booking.meetingroom(
        name
        , building
        , capacity
        , ref_unit_id
        , is_special
        , create_time
        , picture
        , created_by
        , status
    )
    SELECT
        *
    FROM
        meetingroom_data
    RETURNING $ischool.booking.meetingroom.*
) 
INSERT INTO $ischool.booking.meetingroom_equipment(
    name
    , count
    , status
    , ref_meetingroom_id
)
SELECT
    equipment_data.name
    ,equipment_data.count
    ,equipment_data.status
    , (SELECT uid FROM insert_meetingroom)
FROM
    equipment_data

                ", roomData, equipDataRow);
            }

            else
            {
                sql = string.Format(@"
WITH meetingroom_data AS(
    {0}
)
INSERT INTO $ischool.booking.meetingroom(
    name
    , building
    , capacity
    , ref_unit_id
    , is_special
    , create_time
    , picture
    , created_by
    , status
)
SELECT
    *
FROM
    meetingroom_data
                ", roomData);
            }
            #endregion

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }

        public static void UpdateMeetingRoom(string roomID,string roomName,string building,string capacity,string unitID,string isSpecial,string picURL,string status,DataGridViewX dgv)
        {
            #region 資料整理

            string roomData = string.Format(@"
SELECT
    {0}::BIGINT AS uid
    ,'{1}'::TEXT AS name
    ,'{2}'::TEXT AS building
    ,{3} ::INTEGER AS capacity
    ,{4} ::BIGINT AS ref_unit_id 
    ,'{5}'::BOOLEAN AS is_special
    ,'{6}'::TIMESTAMP AS create_time
    ,'{7}'::TEXT AS picture
    ,'{8}'::TEXT AS created_by
    ,'{9}'::TEXT AS status
                ", roomID, roomName, building, capacity, unitID, isSpecial, DateTime.Now.ToString("yyyy/MM/dd"), picURL, Actor.Account, status);

            List<string> equipmentDataList = new List<string>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string equipmentData = string.Format(@"
SELECT
    {0}::BIGINT AS uid
    ,'{1}'::TEXT AS name
    ,{2} ::INTEGER AS count
    ,'{3}' ::TEXT AS status
    ,{4}::BIGINT AS ref_meetingroom_id
                    ", ("" + row.Tag) == "" ? "null" : row.Tag, row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, roomID);

                    equipmentDataList.Add(equipmentData);
                }
            }

            #endregion

            #region SQL
            // 有設備資料
            string sql = "";
            if (equipmentDataList.Count > 0)
            {
                string equipmentDataRow = string.Join(" UNION ALL ", equipmentDataList);

                sql = string.Format(@"
WITH meetingroom_data AS(
    {0}
) ,equipment_data AS(
    {1}
) ,update_meetingroom AS(
    UPDATE
        $ischool.booking.meetingroom
    SET
       name = meetingroom_data.name
        , building = meetingroom_data.building
        , capacity = meetingroom_data.capacity
        , ref_unit_id = meetingroom_data.ref_unit_id
        , is_special = meetingroom_data.is_special
        , create_time = meetingroom_data.create_time
        , picture = meetingroom_data.picture
        , created_by = meetingroom_data.created_by
        , status = meetingroom_data.status
    FROM
        meetingroom_data
    WHERE
         $ischool.booking.meetingroom.uid = meetingroom_data.uid
) ,update_equipment_data AS(
    UPDATE 
        $ischool.booking.meetingroom_equipment
    SET
        name = equipment_data.name
        ,count = equipment_data.count
        ,status = equipment_data.status
    FROM
        equipment_data
    WHERE
        $ischool.booking.meetingroom_equipment.uid = equipment_data.uid
) ,insert_equipment_data AS(
    INSERT INTO $ischool.booking.meetingroom_equipment(
        name
        , count
        , status
        , ref_meetingroom_id
    )
    SELECT
        name
        , count
        , status
        , ref_meetingroom_id
    FROM
        equipment_data
    WHERE
        equipment_data.uid IS NULL
) ,delete_equipment_data AS(
    SELECT
        equipment.uid
    FROM(
        SELECT
            equipment.*
        FROM
            meetingroom_data
            LEFT OUTER JOIN $ischool.booking.meetingroom_equipment AS equipment
                ON meetingroom_data.uid = equipment.ref_meetingroom_id
    ) equipment        
        LEFT OUTER JOIN equipment_data
            ON equipment.uid = equipment_data.uid
    WHERE
        equipment_data.uid IS NULL
)   
    DELETE FROM $ischool.booking.meetingroom_equipment WHERE uid IN(SELECT * FROM delete_equipment_data)
                    ", roomData, equipmentDataRow);
            }
            // 沒有設備資料
            else
            {
                sql = string.Format(@"
WITH meetingroom_data AS(
    {0}
) , update_meetingroom_data AS(
    UPDATE
        $ischool.booking.meetingroom
    SET
        name = meetingroom_data.name
        , building = meetingroom_data.building
        , capacity = meetingroom_data.capacity
        , ref_unit_id = meetingroom_data.ref_unit_id
        , is_special = meetingroom_data.is_special
        , create_time = meetingroom_data.create_time
        , picture = meetingroom_data.picture
        , created_by = meetingroom_data.created_by
        , status = meetingroom_data.status
    FROM
        meetingroom_data
    WHERE
        $ischool.booking.meetingroom.uid = meetingroom_data.uid 
) , delete_equipment_data AS(
    SELECT
        equipment.uid
    FROM
        meetingroom_data
        LEFT OUTER JOIN $ischool.booking.meetingroom_equipment AS equipment
            ON meetingroom_data.uid = equipment.ref_meetingroom_id
)
DELETE 
FROM 
    $ischool.booking.meetingroom_equipment 
WHERE 
    uid IN(
        SELECT * FROM delete_equipment_data
    )
                    ", roomData);
            }
            #endregion

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }
    }
}
