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
    class ObjectIndexRelation
    {
        public int id;
        public SubmissionOfKanji obj;

        public ObjectIndexRelation() { }

        public ObjectIndexRelation(int id, SubmissionOfKanji obj)
        {
            this.id = id;
            this.obj = obj;
        }
    }
}