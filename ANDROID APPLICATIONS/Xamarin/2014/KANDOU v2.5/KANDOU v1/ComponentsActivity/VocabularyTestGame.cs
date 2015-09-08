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
using Android.Graphics;

using KANDOU_v1.DataTypes;

namespace KANDOU_v1.ComponentsActivity
{
    class VocabularyTestGame : MainVocabularyGame
    {
        public int [] vocabularyToTestIndex = null;

        public VocabularyTestGame(Activity mainActivity, SubmissionOfKanji[] vocabulary, ObjectPermission[] vocabularySelectedExtendedObjectList, bool[] vocabularySelectedExtendedList, int[] vocabularyToTestIndex)
           : base (mainActivity, vocabulary, vocabularySelectedExtendedObjectList, vocabularySelectedExtendedList)
        {
            this.vocabularyToTestIndex = vocabularyToTestIndex;
        }
        
        public override void setVocabularyGameRound()
        {
            int max_tab = vocabulary.Length;
            bool repeat = false;

            for (int i = 0; i < 6; i++)
            {
                int number = random.Next(0, max_tab);

              check:
                for (int j = 0; j < i; j++)
                {
                    if (vocabularyIndex[j] == number || number == CorectVocabulary)
                    {
                        number++;
                        if (number == max_tab) number = 0;
                        repeat = true;
                        break;
                    }
                }

                if (repeat)
                {
                    repeat = false;
                    goto check;
                }

                vocabularyIndex[i] = number;
            }

            CorectVocabularyIndex = random.Next(0, 6);
            int index = random.Next(0, vocabularyToTestIndex.Length);
            int tmp = vocabularyIndex[CorectVocabularyIndex];
            vocabularyIndex[CorectVocabularyIndex] = vocabularyToTestIndex[index];   // change existed index to one of tested index
            if (vocabularyIndex[CorectVocabularyIndex] == CorectVocabulary)
            {
                index++;
                if (index >= vocabularyToTestIndex.Length)
                    index = 0;

                vocabularyIndex[CorectVocabularyIndex] = vocabularyToTestIndex[index];
            }
            CorectVocabulary = vocabularyIndex[CorectVocabularyIndex];

            for (int i = 0; i < 6; i++)
            {
                if (i == CorectVocabularyIndex) continue;

                if (vocabularyIndex[i] == CorectVocabulary) vocabularyIndex[i] = tmp;
            }
            
            setVocabularyMainData();
        }

        public void actualizeTestInedexes(int[] t)
        {
            this.vocabularyToTestIndex = t;
        }
    }
}