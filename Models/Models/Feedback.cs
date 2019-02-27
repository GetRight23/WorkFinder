using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int GradeValue { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int IdWorker { get; set; }

        public virtual Worker IdWorkerNavigation { get; set; }
    }
}
