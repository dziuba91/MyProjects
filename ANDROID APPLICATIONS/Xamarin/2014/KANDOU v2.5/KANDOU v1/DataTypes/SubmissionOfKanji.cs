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
    class SubmissionOfKanji
    {
        public string signs;
        public string meaning;
        public string reading;
        public int priority;

        public SubmissionOfKanji() { }

        public SubmissionOfKanji(string signs, string meaning, string reading, int priority)
        {
            this.signs = signs;
            this.meaning = meaning;
            this.priority = priority;
            this.reading = reading;
        }
    }
}