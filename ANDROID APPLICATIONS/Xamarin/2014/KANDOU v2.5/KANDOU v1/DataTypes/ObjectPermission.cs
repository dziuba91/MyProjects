using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KANDOU_v1.DataTypes
{
    [Serializable]
    class ObjectPermission
    {
        public int id;
        public bool permission;

        public ObjectPermission() { }

        public ObjectPermission(int id, bool permission)
        {
            this.id = id;
            this.permission = permission;
        }
    }
}