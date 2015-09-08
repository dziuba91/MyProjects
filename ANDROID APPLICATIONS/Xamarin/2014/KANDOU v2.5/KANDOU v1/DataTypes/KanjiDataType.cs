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

namespace KANDOU_v1
{
    [Serializable]
    class KanjiDataType
    {
        public string sign;
        public string meaning;
        public string reading;

        public SubmissionOfKanji[] submissions;

        public KanjiDataType()  { }

        public KanjiDataType(string sign, string meaning, string reading, SubmissionOfKanji[] submissions)
        {
            this.sign = sign;
            this.meaning = meaning;
            this.reading = reading;
            this.submissions = submissions;
        }
    }
}