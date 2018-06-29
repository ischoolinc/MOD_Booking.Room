using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Ischool.Booking.Room.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ischool.Booking.Room
{
    class RoleUnitDecorator
    {
        private LabelX lbl;
        private ComboBoxEx cboRoles;
        private ComboBoxEx cboUnits;
        private bool needUnAssignedItem = false;

        public RoleUnitDecorator( LabelX lbl , ComboBoxEx cboRoles , ComboBoxEx cboUnits, bool needUnAssignedItem)
        {
            this.lbl = lbl;
            this.cboRoles = cboRoles;
            this.cboUnits = cboUnits;
            this.needUnAssignedItem = needUnAssignedItem;

            initialize();
        }

        public RoleUnitDecorator(LabelX lbl, ComboBoxEx cboRoles, ComboBoxEx cboUnits): this(lbl,cboRoles,cboUnits, false)
        {
            
        }

        private void initialize()
        {
            this.lbl.Visible = (Actor.Instance.isSysAdmin());
            this.cboRoles.Visible = !this.lbl.Visible;
            cboRoles.Items.Add("單位主管");
            cboRoles.Items.Add("單位管理員");

            cboRoles.SelectedIndexChanged += delegate {
                fillUnitItems();
            };
            
            this.cboUnits.DisplayMember = "Name";
            this.cboUnits.ValueMember = "ID";
            
            fillUnitItems();

            if (this.cboUnits.Items.Count > 0)
                cboUnits.SelectedIndex = 0;
        }

        private void fillUnitItems()
        {
            List<UnitRoleInfo> items = new List<UnitRoleInfo>() ;

            if (Actor.Instance.isSysAdmin())
            {
                items = Actor.Instance.getUnits();
            }
            else  if (Actor.Instance.isUnitBoss())
            {
                items = Actor.Instance.getBossUnits();
            }
            else
            {
                items = Actor.Instance.getUnitAdminUnits();
            }

            this.cboUnits.Items.Clear();

            foreach (UnitRoleInfo urInfo in items)
            {
                this.cboUnits.Items.Add(urInfo);
            }

            if (this.needUnAssignedItem)
            {
                this.cboUnits.Items.Add(new UnitRoleInfo("", "--未指定--", false, ""));
            }
        }
    }
}
