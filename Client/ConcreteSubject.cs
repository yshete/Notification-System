using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    /// <summary>
    /// The 'ConcreteSubject' class
    /// </summary>
    class ConcreteSubject : Subject
    {
        private string _subjectState;
        private static ConcreteSubject Instance;

        public static ConcreteSubject GetInstance()
        {
            if (Instance == null)
                Instance = new ConcreteSubject();
            return Instance;
        }
        private ConcreteSubject()
        {
        }
        public string SubjectState
        {
            get { return _subjectState; }
            set { _subjectState = value; }
        }
    }
}
