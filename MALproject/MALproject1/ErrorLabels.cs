using System;
using System.Collections.Generic;
using System.Text;

namespace MALproject1
{
    class ErrorLabels
    {
        public string labelDescription;
        public string labelTitle;

        public ErrorLabels(string description, string title)
        {
            labelDescription = description;
            labelTitle = title;
        }

        public string ReturnLabelTitle()
        {
            return labelTitle;
        }

        public string ReturnLabelDescription()
        {
            return labelDescription;
        }
    }
}
