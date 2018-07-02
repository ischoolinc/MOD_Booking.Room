﻿using DevComponents.DotNetBar;
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

            if (Actor.Instance.isUnitBoss())
            {
                this.cboRoles.Items.Add("單位主管");
            }
            if (Actor.Instance.isUnitAdmin())
            {
                this.cboRoles.Items.Add("單位管理員");
            }
            if (this.cboRoles.Items.Count > 0)
            {
                this.cboRoles.SelectedIndex = 0;
            }
                
            this.cboUnits.DisplayMember = "Name";
            this.cboUnits.ValueMember = "ID";
            
            this.cboRoles.SelectedIndexChanged += delegate {
                fillUnitItems();
            };
            
            fillUnitItems();

            if (this.cboUnits.Items.Count > 0)
                cboUnits.SelectedIndex = 0;
        }

        private void fillUnitItems()
        {

            List<UnitRoleInfo> items = new List<UnitRoleInfo>() ;

            if (Actor.Instance.isSysAdmin())
            {
                items = Actor.Instance.getSysAdminUnits();
            }
            else  if (Actor.Instance.isUnitBoss() && this.cboRoles.Text == "單位主管")
            {
                items = Actor.Instance.getBossUnits();
            }
            else if(Actor.Instance.isUnitAdmin() && this.cboRoles.Text == "單位管理員" )
            {
                items = Actor.Instance.getUnitAdminUnits();
            }

            this.cboUnits.Items.Clear();

            foreach (UnitRoleInfo urInfo in items)
            {
                this.cboUnits.Items.Add(urInfo);
            }

            if (this.needUnAssignedItem && this.cboUnits.Items.Count > 0)
            {
                this.cboUnits.Items.Add(new UnitRoleInfo("", "--未指定--", false,""));
            }

            if (this.cboUnits.Items.Count > 0)
                this.cboUnits.SelectedIndex = 0;
        }
    }
}